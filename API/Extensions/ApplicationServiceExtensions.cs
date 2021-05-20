using API.Data;
using API.Email;
using API.Helpers;
using API.Interfaces;
using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPacientRepository, PacientsRepository>();
            services.AddScoped<IDoctorRepository, DoctorsRepository>();
            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IAppoinmentsRepository, AppoinmentsRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();

            return services;
        }
        
    }
}