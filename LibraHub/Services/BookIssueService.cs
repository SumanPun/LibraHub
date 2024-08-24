using LibraHub.Dtos.BookIssueDto;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using LibraHub.Services.Interface;
using System.Transactions;

namespace LibraHub.Services
{
    public class BookIssueService : IBookIssueService
    {
        private readonly IBookIssueRepsoitory _bookIssueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookIssueService(IBookIssueRepsoitory bookIssueRepository, IUnitOfWork unitOfWork)
        {
            _bookIssueRepository = bookIssueRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateBookIssueDto createBookIssueDto)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var bookIssue = new BookIssue
            {
                BookId = createBookIssueDto.BookId,
                IssueDate = createBookIssueDto.IssueDate,
                ReturnDate = createBookIssueDto.ReturnDate,
                StudentId = createBookIssueDto.StudentId,
                Note = createBookIssueDto.Note,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = createBookIssueDto.CreatedUserId,
                ModifiedDate = DateTime.UtcNow,
                ModifiedUserId = createBookIssueDto.CreatedUserId,
                IsReturn = false
            };
            await _unitOfWork.CreateAsync(bookIssue);
            await _unitOfWork.SaveAsync();

            var bookIssueHistory = new BookIssueHistory
            {
                BookIssueId = bookIssue.Id,
                Message = $"Issue Book successfully. Id: {bookIssue.Id}",
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(bookIssueHistory);
            await _unitOfWork.SaveAsync();
            tx.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            var bookIssue = await _bookIssueRepository.GetByAsync(x => x.Id == id);
            await _unitOfWork.DeleteAsync(bookIssue);
            await _unitOfWork.SaveAsync();
        }

        public async Task ReturnBookAsync(int id, DateTime returnDate)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var bookIssue = await _bookIssueRepository.GetByAsync(x => x.Id == id);
            bookIssue.IsReturn = true;
            bookIssue.ReturnDate = returnDate;
            await _unitOfWork.UpdateAsync(bookIssue);
            await _unitOfWork.SaveAsync();

            var bookIssueHistory = new BookIssueHistory
            {
                BookIssueId = bookIssue.Id,
                Message = $"Return Book successfully. Id: {bookIssue.Id}",
                CreatedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(bookIssueHistory);
            await _unitOfWork.SaveAsync();
            tx.Complete();
        }

        public async Task UpdateAsync(UpdateBookIssueDto updateBookIssueDto)
        {
            var bookIssue = await _bookIssueRepository.GetByAsync(x => x.Id == updateBookIssueDto.Id);
            bookIssue.BookId = updateBookIssueDto.BookId;
            bookIssue.IssueDate = updateBookIssueDto.IssueDate;
            bookIssue.ReturnDate = updateBookIssueDto.ReturnDate;
            bookIssue.StudentId = updateBookIssueDto.StudentId;
            bookIssue.Note = updateBookIssueDto.Note;
            bookIssue.ModifiedDate = DateTime.UtcNow;
            bookIssue.ModifiedUserId = updateBookIssueDto.ModifiedUserId;
            await _unitOfWork.UpdateAsync(bookIssue);
            await _unitOfWork.SaveAsync();
        }
    }
}
