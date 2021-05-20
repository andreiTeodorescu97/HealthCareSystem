using System;
using System.Linq;
using API.DTOs;
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
            .ForMember(dest => dest.Status, options => options.MapFrom(source => source.Status.Name));

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
            CreateMap<PrescriptionDto, Prescription>();

            CreateMap<ErrorDto, Error>();

        }
    }
}