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
            CreateMap<Doctor, DoctorDto>();
            
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

            CreateMap<DoctorDto, Doctor>();

            CreateMap<StudiesAndExperience, StudiesAndExperienceDto>();
            
            CreateMap<StudiesAndExperienceDto, StudiesAndExperience>();

        }
    }
}