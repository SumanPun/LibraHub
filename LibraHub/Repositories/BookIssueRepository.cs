using LibraHub.Data;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraHub.Repositories
{
    public class BookIssueRepository : Repository<BookIssue> , IBookIssueRepsoitory
    {
        public BookIssueRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<BookIssue>> GetAllBookIssuesWithProperties()
        {
            return await _context.BookIssues!.Include(b => b.Book).Include(u => u.CreatedUser).Include(u => u.ModifiedUser).Include(u => u.Student).ToListAsync();
        }

        public async Task<List<BookIssue>> GetAllBookIssuesWithPropertiesNotReturned()
        {
            return await _context.BookIssues!.Include(b => b.Book).Include(u => u.CreatedUser).Include(u => u.ModifiedUser).Include(u => u.Student).Where(b => b.IsReturn == false).ToListAsync();
        }

        public async Task<List<BookIssue>> GetAllBookIssuesWithPropertiesReturned()
        {
            return await _context.BookIssues!.Include(b => b.Book).Include(u => u.CreatedUser).Include(u => u.ModifiedUser).Include(u => u.Student).Where(b => b.IsReturn == true).ToListAsync();
        }

        public async Task<BookIssue> GetBookIssueWithProperties(int id)
        {
            return await _context.BookIssues!.Include(b => b.Book).Include(u => u.CreatedUser).Include(u => u.ModifiedUser).Include(u => u.Student).FirstOrDefaultAsync(b => b.Id == id) ?? throw new Exception("Book Issue not found");
        }

        public async Task<int> GetTotalBookIssueCount()
        {
            return await _context.BookIssues!.CountAsync();
        }

        public async Task<int> GetTotalBookReturnCount()
        {
            return await _context.BookIssues!.Where(x => x.IsReturn).CountAsync();
        }
    }
}
