using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<MedicineDto>> GetMedicines();
        Task<FullRecipeInfoDto> GetRecipe(int consultationId);
        Task<bool> AddRecipe(RecipeDto recipeDto);
        Task<bool> DeleteRecipe(int recipeId);
    }
}