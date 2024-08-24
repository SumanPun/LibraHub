using AspNetCoreHero.ToastNotification.Abstractions;
using LibraHub.Dtos.BookIssueDto;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using LibraHub.Services.Interface;
using LibraHub.ViewModels.BookIssueViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace LibraHub.Controllers
{
    public class BookIssueController : Controller
    {
        private readonly IBookIssueRepsoitory _bookIssueRepository;
        private readonly IBookIssueService _bookIssueService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IBookService _bookService;
        private readonly IBookIssueHistoryRepository _bookIssueHistoryRepository;

        public BookIssueController(IBookIssueRepsoitory bookIssueRepository, IBookIssueService bookIssueService, UserManager<ApplicationUser> userManager, INotyfService notyfService, IBookRepository bookRepository, IStudentRepository studentRepository, IBookService bookService, IBookIssueHistoryRepository bookIssueHistoryRepository)
        {
            _bookIssueRepository = bookIssueRepository;
            _bookIssueService = bookIssueService;
            _userManager = userManager;
            _notyfService = notyfService;
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
            _bookService = bookService;
            _bookIssueHistoryRepository = bookIssueHistoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var bookIssues = await _bookIssueRepository.GetAllBookIssuesWithPropertiesNotReturned();
            var vm = bookIssues.Select(x => new BookIssueViewModel()
            {
                Id = x.Id,
                IssueDate = x.IssueDate,
                StudentName = x.Student!.FullName,
                Book = x.Book!.Name,
                Publication = x.Book.Publication,
                ReturnDate = x.ReturnDate
            }).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var books = await _bookRepository.GetAllAsync();
            var students = await _studentRepository.GetAllAsync();
            var vm = new CreateBookIssueViewModel();
            vm.BookSelectList = books.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            vm.StudentSelectList = students.Select(x => new SelectListItem()
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();
            vm.IssueDate = DateTime.Now;
            vm.ReturnDate = DateTime.Now.AddDays(7);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookIssueViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                if (vm.IssueDate > vm.ReturnDate)
                {
                    _notyfService.Error("Issue Date can not be greater than Return Date");
                    return View(vm);
                }
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var currentUser = await _userManager.GetUserAsync(User);
                var dto = new CreateBookIssueDto()
                {
                    BookId = vm.BookId,
                    IssueDate = vm.IssueDate,
                    ReturnDate = vm.ReturnDate,
                    StudentId = vm.StudentId,
                    Note = vm.Note,
                    CreatedUserId = currentUser!.Id
                };
                await _bookIssueService.CreateAsync(dto);
                await _bookService.DecreaseQauantityAsync(vm.BookId);
                _notyfService.Success("Book Issue Created Successfully");
                tx.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var bookIssue = await _bookIssueRepository.GetBookIssueWithProperties(id);
            var vm = new BookIssueDetailsViewModel()
            {
                Id = bookIssue.Id,
                Book = bookIssue.Book!.Name,
                Grade = bookIssue.Student!.Grade,
                IssueDate = bookIssue.IssueDate,
                ReturnDate = bookIssue.ReturnDate,
                Student = bookIssue.Student.FullName,
                Status = bookIssue.IsReturn,
                Note = bookIssue.Note,
                CreatedBy = bookIssue.CreatedUser!.FullName,
                CreatedDate = bookIssue.CreatedDate,
                ModifiedBy = bookIssue.ModifiedUser?.FullName,
                ModifiedDate = bookIssue.ModifiedDate
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(int id, DateTime returnDate)
        {
            try
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var issueBook = await _bookIssueRepository.GetByAsync(x => x.Id == id);
                await _bookIssueService.ReturnBookAsync(id, returnDate);
                await _bookService.IncreaseQauantityAsync(issueBook.BookId);
                _notyfService.Success("Book Returned Successfully");
                tx.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> IndexReturnBook()
        {
            var bookIssues = await _bookIssueRepository.GetAllBookIssuesWithPropertiesReturned();
            var vm = bookIssues.Select(x => new BookIssueViewModel()
            {
                Id = x.Id,
                IssueDate = x.IssueDate,
                StudentName = x.Student!.FullName,
                Book = x.Book!.Name,
                Publication = x.Book.Publication,
                ReturnDate = x.ReturnDate
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var bookIssue = await _bookIssueRepository.GetByAsync(x => x.Id == id);
                await _bookIssueService.DeleteAsync(id);
                await _bookService.IncreaseQauantityAsync(bookIssue.BookId);
                _notyfService.Success("Book Issue Deleted Successfully");
                tx.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bookIssue = await _bookIssueRepository.GetBookIssueWithProperties(id);
            if (bookIssue.IsReturn)
            {
                _notyfService.Error("Book Already Returned");
                return RedirectToAction(nameof(Index));
            }
            var vm = new EditBookIssueViewModel()
            {
                Id = bookIssue.Id,
                BookId = bookIssue.BookId,
                StudentId = bookIssue.StudentId,
                IssueDate = bookIssue.IssueDate,
                ReturnDate = bookIssue.ReturnDate,
                Note = bookIssue.Note
            };
            var books = await _bookRepository.GetAllAsync();
            var students = await _studentRepository.GetAllAsync();
            vm.BookSelectList = books.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == bookIssue.BookId
            }).ToList();
            vm.StudentSelectList = students.Select(x => new SelectListItem()
            {
                Text = x.FullName,
                Value = x.Id.ToString(),
                Selected = x.Id == bookIssue.StudentId
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookIssueViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var dto = new UpdateBookIssueDto()
                {
                    Id = vm.Id,
                    BookId = vm.BookId,
                    IssueDate = vm.IssueDate,
                    ReturnDate = vm.ReturnDate,
                    StudentId = vm.StudentId,
                    Note = vm.Note,
                    ModifiedUserId = currentUser!.Id
                };
                await _bookIssueService.UpdateAsync(dto);
                _notyfService.Success("Book Issue Updated Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var bookIssue = await _bookIssueRepository.GetBookIssueWithProperties(id);
            var vm = new BookIssueDetailsViewModel()
            {
                Id = bookIssue.Id,
                Book = bookIssue.Book!.Name,
                Grade = bookIssue.Student!.Grade,
                IssueDate = bookIssue.IssueDate,
                ReturnDate = bookIssue.ReturnDate,
                Student = bookIssue.Student.FullName,
                Status = bookIssue.IsReturn,
                Note = bookIssue.Note,
                CreatedBy = bookIssue.CreatedUser!.FullName,
                CreatedDate = bookIssue.CreatedDate,
                ModifiedBy = bookIssue.ModifiedUser?.FullName,
                ModifiedDate = bookIssue.ModifiedDate
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> IndexBookIssueHistory()
        {
            var bookIssueHistories = await _bookIssueHistoryRepository.GetAllAsync();
            var vm = bookIssueHistories.Select(x => new BookIssueHistoryViewModel()
            {
                Id = x.Id,
                BookIssueId = x.BookIssueId,
                Message = x.Message,
                CreatedDate = x.CreatedDate,
            }).ToList();
            return View(vm);
        }
    }
}
