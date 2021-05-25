using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.DTOs.Recipes;
using API.Email;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly ILoggerService _loggerService;
        public RecipeRepository(DataContext context, IMapper mapper, ILoggerService loggerService,
            IMailService mailService)
        {
            _loggerService = loggerService;
            _mailService = mailService;
            _mapper = mapper;
            _context = context;

        }

        public async Task<IEnumerable<MedicineDto>> GetMedicines()
        {
            var result = await _context.Medicines
                .OrderBy(c => c.Id)
                .ProjectTo<MedicineDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task<FullRecipeInfoDto> GetRecipe(int consultationId)
        {
            var recipe = await _context.Recipes.Where(c => c.ConsultationId == consultationId)
            .ProjectTo<RecipeDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

            var pacient = await _context.Pacients.Where(c => c.Id == recipe.PacientId)
            .ProjectTo<PacientRecipeDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

            var doctor = await _context.Consultations
            .Include(c => c.Appoinment.Doctor)
            .Where(c => c.Id == consultationId)
            .Select(c => c.Appoinment.Doctor)
            .ProjectTo<DoctorRecipeDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

            var result = new FullRecipeInfoDto{
                Doctor = doctor,
                Pacient = pacient,
                Recipe = recipe
            };

            return result;
        }

        public async Task<bool> AddRecipe(RecipeDto recipeDto)
        {
            var consultation = await _context.Consultations.FindAsync(recipeDto.ConsultationId);
            consultation.HasRecipe = true;

            recipeDto.UniqueId = Guid.NewGuid();
            try
            {
                var recipe = new Recipe();
                _mapper.Map(recipeDto, recipe);
                recipe.DateAdded = DateTime.Now;

                _context.Recipes.Add(recipe);
                _context.Entry(consultation).State = EntityState.Modified;

                return await _context.SaveChangesAsync() > 0;
            }
            catch (System.Exception ex)
            {
                await _loggerService.LogError(ex);

            }
            return false;
        }

        public async Task<bool> DeleteRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            _context.Recipes.Remove(recipe);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}