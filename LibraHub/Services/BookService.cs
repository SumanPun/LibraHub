using LibraHub.Dtos.BookDto;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using LibraHub.Services.Interface;

namespace LibraHub.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateBookDto createBookDto)
        {
            var book = new Book()
            {
                Name = createBookDto.Name,
                Author = createBookDto.Author,
                NumberOfCopies = createBookDto.NumberOfCopies,
                AvailableCopies = createBookDto.NumberOfCopies,
                CategoryId = createBookDto.CategoryId,
                Publication = createBookDto.Publication,
                CreatedUserId = createBookDto.CreatedUserId,
                CreatedDate = DateTime.UtcNow,
                ModifiedUserId = createBookDto.CreatedUserId,
                ModifiedDate = DateTime.UtcNow
            };
            await _unitOfWork.CreateAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task DecreaseQauantityAsync(int id)
        {
            var book = await _bookRepository.GetByAsync(x => x.Id == id);
            book.AvailableCopies--;
            await _unitOfWork.UpdateAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByAsync(x => x.Id == id);
            await _unitOfWork.DeleteAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task IncreaseQauantityAsync(int id)
        {
            var book = await _bookRepository.GetByAsync(x => x.Id == id);
            book.AvailableCopies++;
            await _unitOfWork.UpdateAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByAsync(x => x.Id == updateBookDto.Id);
            book.Name = updateBookDto.Name;
            book.Author = updateBookDto.Author;
            book.Publication = updateBookDto.Publication;
            book.NumberOfCopies = updateBookDto.NumberOfCopies;
            book.AvailableCopies = updateBookDto.AvailableCopies;
            book.CategoryId = updateBookDto.CategoryId;
            book.ModifiedUserId = updateBookDto.ModifiedUserId;
            book.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.UpdateAsync(book);
            await _unitOfWork.SaveAsync();
        }
    }
}
