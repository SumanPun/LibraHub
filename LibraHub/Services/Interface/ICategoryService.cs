using LibraHub.Dtos.CategoryDto;

namespace LibraHub.Services.Interface
{
    public interface ICategoryService
    {
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}
