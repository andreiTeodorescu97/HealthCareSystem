using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RecipeController : BaseApiController
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository, DataContext context)
        {
            _recipeRepository = recipeRepository;

        }

        [HttpGet("get-medicines")]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetDoctorAppoinments()
        {
            return Ok(await _recipeRepository.GetMedicines());
        }

        [HttpGet("get-recipe")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int recipeId)
        {
            return Ok(await _recipeRepository.GetRecipe(recipeId));
        }

        [HttpPost("add-recipe")]
        public async Task<ActionResult> AddRecipe(RecipeDto recipeDto)
        {
            if (recipeDto.ConsultationId <= 0 || recipeDto.PacientId <= 0)
            {
                return BadRequest("Invalid data!");
            }

            if (await _recipeRepository.AddRecipe(recipeDto))
            {
                return Ok();
            }

            return BadRequest("Upps..ceva nu a mers!");
        }

        [HttpDelete("delete-recipe")]
        public async Task<ActionResult> DeleteRecipeAsync(int recipeId)
        {
            if (recipeId <= 0)
            {
                return BadRequest("Invalid data!");
            }

            if (await _recipeRepository.DeleteRecipe(recipeId))
            {
                return Ok();
            }

            return BadRequest("Upps..ceva nu a mers!");
        }
    }
}