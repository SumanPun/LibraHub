using LibraHub.Data;
using LibraHub.Models;
using LibraHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraHub.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Student>> GetStudentsWithUserAsync()
        {
            return await _context.Students!.Include(c => c.CreatedUser).Include(m => m.ModifiedUser).ToListAsync();
        }

        public async Task<Student> GetStudentWithUserAsync(int id)
        {
            return await _context.Students!.Include(c => c.CreatedUser).Include(m => m.ModifiedUser).FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Student not found");
        }
    }
}
