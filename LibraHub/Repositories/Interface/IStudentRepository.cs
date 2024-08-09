using LibraHub.Models;

namespace LibraHub.Repositories.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<Student>> GetStudentsWithUserAsync();
        Task<Student> GetStudentWithUserAsync(int id);
    }
}
