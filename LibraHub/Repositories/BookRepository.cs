using LibraHub.Data;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraHub.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Book>> GetAllBooksWithUserAndCategory()
        {
            return await _context.Books!.Include(x => x.Category).Include(u => u.CreatedUser).ToListAsync();
        }

        public async Task<Book> GetBookWithUserAndCategory(int id)
        {
            return await _context.Books!.Include(x => x.Category).Include(u => u.CreatedUser).FirstOrDefaultAsync(x=> x.Id == id) ?? throw new Exception("Book not found");
        }
        public async Task<int> GetTotalAvailableBookCount()
        {
            var books = await _context.Books!.ToListAsync();
            var total = books.Select(z => z.AvailableCopies).Sum();
            return total;
        }

        public async Task<int> GetTotalBookCount()
        {
            var books = await _context.Books!.ToListAsync();
            var total = books.Select(z => z.NumberOfCopies).Sum();
            return total;
        }
    }
}
