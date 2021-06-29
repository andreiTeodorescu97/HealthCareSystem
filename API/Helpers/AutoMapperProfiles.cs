using System;
using System.Linq;
using API.DTOs;
using API.DTOs.Messages;
using API.DTOs.Recipes;
using API.DTOs.Reviews;
using API.DTOs.Vaccines;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDoctorDto>();
            CreateMap<Doctor, DoctorDto>()
            .ForMember(
            destination => destination.Age,
            options => options.MapFrom(source => source.DateOfBirth.CalculateAge()))
            .ForMember(destination => destination.MainPhotoUrl,
            options => options.MapFrom(source => source.Photos.FirstOrDefault(c => c.IsMain == true).Url));

            CreateMap<Pacient, GetPacientDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<PacientContact, PacientContactDto>()
            .ForMember(destination => destination.CityId,
            options => options.MapFrom(source => source.City.Id))
            .ForMember(destination => destination.RegionId,
            options => options.MapFrom(source => source.City.Region.Id));

            CreateMap<City, CityDto>()
            .ForMember(destination => destination.RegionId,
            options => options.MapFrom(source => source.RegionId));

            CreateMap<Region, RegionDto>();

            CreateMap<DoctorDto, Doctor>()
            .ForMember(x => x.WorkDays, opt => opt.Ignore());

            CreateMap<StudiesAndExperience, StudiesAndExperienceDto>();

            CreateMap<StudiesAndExperienceDto, StudiesAndExperience>();

            CreateMap<WorkDay, WorkDayDto>();

            CreateMap<WorkDayDto, WorkDay>()
            .ForMember(dest => dest.StartTimeSpan, opt => opt.MapFrom(src => (int)DateTime.Parse(src.StartHour).TimeOfDay.TotalMinutes))
            .ForMember(dest => dest.EndTimeSpan, opt => opt.MapFrom(src => (int)DateTime.Parse(src.EndHour).TimeOfDay.TotalMinutes));

            CreateMap<Appoinment, GetAppoimnetsDto>()
            .ForMember(dest => dest.DoctorFirstName, options => options.MapFrom(source => source.Doctor.FirstName))
            .ForMember(dest => dest.DoctorSecondName, options => options.MapFrom(source => source.Doctor.SecondName))
            .ForMember(dest => dest.PacientFirstName, options => options.MapFrom(source => source.Pacient.FirstName))
            .ForMember(dest => dest.PacientSecondName, options => options.MapFrom(source => source.Pacient.SecondName))
            .ForMember(dest => dest.PacientUserName, options => options.MapFrom(source => source.Pacient.User.UserName))
            .ForMember(dest => dest.DoctorUserName, options => options.MapFrom(source => source.Doctor.User.UserName))
            .ForMember(dest => dest.Status, options => options.MapFrom(source => source.Status.Name))
            .ForMember(dest => dest.DoctorProfilePhotoUrl, options => options
                .MapFrom(source => source.Doctor.Photos.FirstOrDefault(c => c.IsMain == true).Url));

            CreateMap<ConsultationDto, Consultation>();
            CreateMap<Consultation, ConsultationDto>();

            CreateMap<PacientGeneralMedicalDataDto, PacientGeneralMedicalData>();
            CreateMap<PacientGeneralMedicalData, PacientGeneralMedicalDataDto>();

            CreateMap<Vaccine, VaccineDto>();
            CreateMap<Photo, PhotoDto>();

            CreateMap<Medicine, MedicineDto>();
            CreateMap<MedicineDto, Medicine>();

            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();

            CreateMap<Prescription, PrescriptionDto>();
            
            CreateMap<PrescriptionDto, Prescription>()
            .ForMember(dest => dest.Medicine, options => options.Ignore());

            CreateMap<ErrorDto, Error>();

            CreateMap<Doctor, DoctorRecipeDto>()
            .ForMember(destination => destination.MainPhotoUrl,
            options => options.MapFrom(source => source.Photos.FirstOrDefault(c => c.IsMain == true).Url));

            CreateMap<Pacient, PacientRecipeDto>();

            CreateMap<Message, MessageDto>()
            .ForMember(
                dest => dest.SenderPhotoUrl,
                options => options.MapFrom(source => source.Sender.Doctor.Photos
                .FirstOrDefault(x => x.IsMain == true).Url)
            )
            .ForMember(
                dest => dest.RecipientPhotoUrl,
                options => options.MapFrom(source => source.Recipient.Doctor.Photos
                .FirstOrDefault(x => x.IsMain == true).Url));

             CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));

             CreateMap<PacientHistory, PacientHistoryDto>();
             CreateMap<Review, ReviewDto>();
        } 
    }
}