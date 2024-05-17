using Fiorello_PB101.Models;
using Fiorello_PB101.ViewModels.Categories;

namespace Fiorello_PB101.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<CategoryProductVM>> GetAllWithProductAsync();
    }
}
