using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public interface IConsultationRepository
    {
        Task<bool> AddConsultationAsync(ConsultationDto consultationDto);
        Task<IEnumerable<ConsultationDto>> GetPacientConsultations(int pacientId);
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
            var linkedAppoinment = await _context.Appoinments.FindAsync(consultationDto.AppoinmentId);

            if (consultationDto.PacientId == null)
            {
                consultationDto.PacientId = linkedAppoinment.PacientId;
            }

            var consultation = new Consultation();
            _mapper.Map(consultationDto, consultation);
            consultation.DateAdded = DateTime.Now;
            _context.Consultations.Add(consultation);

            linkedAppoinment.IsConsultationAdded = true;
            _context.Entry(linkedAppoinment).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ConsultationDto>> GetPacientConsultations(int pacientId)
        {
            var consultations = await _context.Consultations
            .Where(C => C.PacientId == pacientId)
            .ProjectTo<ConsultationDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return consultations;
        }
    }
}