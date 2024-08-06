using LibraHub.Dtos.BookDto;

namespace LibraHub.Services.Interface
{
    public interface IBookService
    {
        Task CreateAsync(CreateBookDto createBookDto);
        Task UpdateAsync(UpdateBookDto updateBookDto);
        Task DeleteAsync(int id);
        Task DecreaseQauantityAsync(int id);
        Task IncreaseQauantityAsync(int id);
    }
}
