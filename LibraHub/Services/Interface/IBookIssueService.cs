using LibraHub.Dtos.BookIssueDto;

namespace LibraHub.Services.Interface
{
    public interface IBookIssueService
    {
        Task CreateAsync(CreateBookIssueDto createBookIssueDto);
        Task UpdateAsync(UpdateBookIssueDto updateBookIssueDto);
        Task DeleteAsync(int id);
        Task ReturnBookAsync(int id, DateTime returnDate);
    }
}
