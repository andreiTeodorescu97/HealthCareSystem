using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Repositories
{
    public interface IConsultationRepository
    {
        Task<bool> AddConsultationAsync(ConsultationDto consultationDto);
    }
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ConsultationRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddConsultationAsync(ConsultationDto consultationDto)
        {
            var consultation = new Consultation();
            _mapper.Map(consultationDto, consultation);
            _context.Consultations.Add(consultation);
            
            return await _context.SaveChangesAsync() > 0; 
        }
    }
}