using LibraHub.Dtos.StudentDto;

namespace LibraHub.Services.Interface
{
    public interface IStudentService
    {
        Task CreateAsync(CreateStudentDto createStudentDto);
        Task UpdateAsync(UpdateStudentDto updateStudentDto);
        Task DeleteAsync(int id);
    }
}
