using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Recipes;
using API.Interfaces;
using API.RecipePDF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;
using System;
using API.Entities;

namespace API.Controllers
{
    [Authorize]
    public class RecipeController : BaseApiController
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IConverter _converter;
        private readonly DataContext _context;
        private readonly ILoggerService _loggerService;
        public RecipeController(IRecipeRepository recipeRepository, DataContext context, IConverter converter, ILoggerService loggerService)
        {
            _loggerService = loggerService;
            _context = context;
            _converter = converter;
            _recipeRepository = recipeRepository;

        }

        [HttpGet("get-medicines")]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetDoctorAppoinments()
        {
            return Ok(await _recipeRepository.GetMedicines());
        }

        [HttpGet("get-recipe")]
        public async Task<ActionResult<FullRecipeInfoDto>> GetRecipe(int consultationId)
        {
            return Ok(await _recipeRepository.GetRecipe(consultationId));
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

        [HttpGet("generateRecipePdf")]
        public async Task<IActionResult> CreateRecipePDFAsync(int consultationId)
        {
            try
            {
                var fullRecipeInfoDto = await _recipeRepository.GetRecipe(consultationId);

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Reteta"
                };
                var styleSheet = _context.EmailTemplates.FirstOrDefault(c => c.Id == 5).Template;
                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = TemplateGenerator.GetRecipeHTMLString(fullRecipeInfoDto),
                    /* WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = styleSheet }, */
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wassets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                };
                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };
                var file = _converter.Convert(pdf);
                return File(file, "application/pdf", "Reteta");
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    InnerMessage = ex.InnerException.Message,
                    StackTrace = ex.InnerException.StackTrace,
                };
                _context.Errors.Add(error);
                await _context.SaveChangesAsync();
                return BadRequest("Upps..ceva nu a mers!");
            }
        }


        [HttpGet("generateTestPdf")]
        public IActionResult CreatePDF(int consultationId)
        {

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wassets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }
    }
}