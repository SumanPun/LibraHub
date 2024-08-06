using AspNetCoreHero.ToastNotification.Abstractions;
using LibraHub.Dtos.BookDto;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using LibraHub.Services.Interface;
using LibraHub.ViewModels.BookViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraHub.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotyfService _notyfService;
        private readonly ICategoryRepository _categoryRepository;

        public BookController(IBookService bookService, IBookRepository bookRepository, UserManager<ApplicationUser> userManager, INotyfService notyfService, ICategoryRepository categoryRepository)
        {
            _bookService = bookService;
            _bookRepository = bookRepository;
            _userManager = userManager;
            _notyfService = notyfService;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllBooksWithUserAndCategory();
            var vm = books.Select(x => new BookViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                NumberOfCopies = x.NumberOfCopies,
                AvailableCopies = x.AvailableCopies,
                CategoryName = x.Category!.Name,
            }).ToList();
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var vm = new CreateBookViewModel()
            {
                Categories = categories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var dto = new CreateBookDto()
                {
                    Name = vm.Name,
                    Author = vm.Author,
                    Publication = vm.Publication,
                    NumberOfCopies = vm.NumberOfCopies,
                    CategoryId = vm.CategoryId,
                    CreatedUserId = _userManager.GetUserId(User)
                };
                await _bookService.CreateAsync(dto);
                _notyfService.Success("Book created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bookService.DeleteAsync(id);
                _notyfService.Success("Book deleted successfully");
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
            try
            {
                var book = await _bookRepository.GetByAsync(x => x.Id == id);
                var categories = await _categoryRepository.GetAllAsync();
                var vm = new EditBookViewModel()
                {
                    Id = book.Id,
                    Name = book.Name,
                    Author = book.Author,
                    Publication = book.Publication,
                    NumberOfCopies = book.NumberOfCopies,
                    AvailableCopies = book.AvailableCopies,
                    CategoryId = book.CategoryId,
                    Categories = categories.Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList()
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var currentUser = await _userManager.GetUserAsync(User);
            try
            {
                if (vm.AvailableCopies > vm.NumberOfCopies)
                {
                    _notyfService.Error("Available copies can't be greater than number of copies");
                    return View(vm);
                }
                var dto = new UpdateBookDto()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Author = vm.Author,
                    Publication = vm.Publication,
                    NumberOfCopies = vm.NumberOfCopies,
                    AvailableCopies = vm.AvailableCopies,
                    CategoryId = vm.CategoryId,
                    ModifiedUserId = _userManager.GetUserId(User)
                };
                await _bookService.UpdateAsync(dto);
                _notyfService.Success("Book updated successfully");
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
            try
            {
                var book = await _bookRepository.GetBookWithUserAndCategory(id);
                var vm = new BookDetailViewModel()
                {
                    Id = book.Id,
                    Name = book.Name,
                    Author = book.Author,
                    Publication = book.Publication,
                    NumberOfCopies = book.NumberOfCopies,
                    AvailableCopies = book.AvailableCopies,
                    Category = book.Category!.Name,
                    CreatedDate = book.CreatedDate,
                    CreatedBy = book.CreatedUser!.FullName,
                    ModifiedDate = book.ModifiedDate,
                    ModifiedBy = book.ModifiedUser?.FullName
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
