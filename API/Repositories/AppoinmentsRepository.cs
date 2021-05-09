using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Constants;
using API.Data;
using API.DTOs;
using API.Email;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AppoinmentsRepository : IAppoinmentsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public AppoinmentsRepository(DataContext context, IMapper mapper, IMailService mailService)
        {
            _mailService = mailService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<FreeHourDto>> GetAvailableHours(int doctorId, DateTime date)
        {
            var workHoursForDay = await _context.WorkDays.Where(d => d.DoctorId == doctorId & d.Day == GetEnglishDayName(date)).SingleOrDefaultAsync();

            if (workHoursForDay == null)
            {
                return null;
            }

            var dateString = date.ToString("dd/MM/yyyy");

            var appoimentsForDoctor = await _context.Appoinments.Where(d => d.DoctorId == doctorId & d.AppoinmentDate.Contains(dateString)).ToListAsync();

            //An appoiment will have be default 30 minutes
            var numberOfSlots = (workHoursForDay.EndTimeSpan - workHoursForDay.StartTimeSpan) / 30;

            var from = workHoursForDay.StartTimeSpan;
            var to = workHoursForDay.StartTimeSpan + 30;
            var availableSpots = new List<FreeHourDto>();
            var id = 0;

            for (int i = 0; i < numberOfSlots; i++)
            {
                if (!appoimentsForDoctor.Any(d => d.AppoinmentStartSpan == from && d.AppoinmentEndSpan == to))
                {
                    availableSpots.Add(new FreeHourDto
                    {
                        Id = id,
                        FromHour = TimeSpan.FromMinutes((int)from).ToString().Remove(TimeSpan.FromMinutes((int)from).ToString().Length - 3),
                        ToHour = TimeSpan.FromMinutes((int)to).ToString().Remove(TimeSpan.FromMinutes((int)to).ToString().Length - 3),
                        FromTimeSpan = (int)from,
                        ToTimeSpan = (int)to,
                    });
                    id++;
                }

                from = from + 30;
                to = from + 30;
            }

            return availableSpots;
        }

        public async Task<bool> AddAppoinmentAsync(MakeAnAppoinmentDto makeAnAppoinmentDto, int pacientId)
        {
            var time = TimeSpan.FromMinutes((int)makeAnAppoinmentDto.FromTimeSpan).ToString().
                Substring(0, TimeSpan.FromMinutes((int)makeAnAppoinmentDto.FromTimeSpan).ToString().Length - 3) + "-" +
                        TimeSpan.FromMinutes((int)makeAnAppoinmentDto.ToTimeSpan).ToString().
                Substring(0, TimeSpan.FromMinutes((int)makeAnAppoinmentDto.ToTimeSpan).ToString().Length - 3);

            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //Add 3 hours in seconds to complete the diference between utc0 and utc3 (Romania time)
            dtDateTime = dtDateTime.AddSeconds(makeAnAppoinmentDto.DayUnixTime + 10800);

            var dateId = (dtDateTime.Year * 100 + dtDateTime.Month) * 100 + dtDateTime.Day;

            var newAppoinment = new Appoinment
            {
                AppoinmentDate = dtDateTime.ToString("dd/MM/yyyy"),
                AppoinmentHour = time,
                AppoinmentStartSpan = makeAnAppoinmentDto.FromTimeSpan,
                AppoinmentEndSpan = makeAnAppoinmentDto.ToTimeSpan,
                Reason = makeAnAppoinmentDto.Reason,
                DateId = dateId,
                PacientId = pacientId,
                DoctorId = makeAnAppoinmentDto.DoctorId,
                IsConsultationAdded = false,
                StatusId = (int)AppoinmentStatuses.Pending,
            };

            _context.Appoinments.Add(newAppoinment);

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateAppoinmentStatus(UpdateAppoinmentStatusDto updateAppoinmentStatusDto)
        {
            var apppoinment = await _context.Appoinments.Include(c => c.Doctor)
            .Include(c => c.Pacient)
            .SingleOrDefaultAsync(c => c.Id == updateAppoinmentStatusDto.AppoinmentId);

            apppoinment.StatusId = updateAppoinmentStatusDto.NewStatusId;

            _context.Entry(apppoinment).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0)
            {
                if (updateAppoinmentStatusDto.NewStatusId == (int)AppoinmentStatuses.Approved)
                {
                    var appoimentApprovalMail = new AppoinmentApprovalMail
                    {
                        DoctorFirstName = apppoinment.Doctor.FirstName,
                        DoctorSecondName = apppoinment.Doctor.SecondName,
                        AppoinmentDate = apppoinment.AppoinmentDate,
                        AppoinmentId = apppoinment.Id,
                        ToEmail = apppoinment.Pacient.Email
                    };

                    await _mailService.SendAppoinmentApproval(appoimentApprovalMail);
                }

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<GetAppoimnetsDto>> GetPacientAppoinments(int pacientId)
        {
            return await _context.Appoinments.Where(c => c.PacientId == pacientId)
                    .ProjectTo<GetAppoimnetsDto>(_mapper.ConfigurationProvider)
                    .OrderBy(c => c.DateId)
                    .ToListAsync();
        }

        public async Task<IEnumerable<GetAppoimnetsDto>> GetDoctorAppoinments(int doctorId)
        {
            return await _context.Appoinments.Where(c => c.DoctorId == doctorId)
                    .ProjectTo<GetAppoimnetsDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(c => c.DateId)
                    .ToListAsync();
        }
        private string GetEnglishDayName(DateTime date)
        {
            string nameOfDay = date.ToString("dddd");
            switch (nameOfDay)
            {
                case "Monday":
                    nameOfDay = "Luni";
                    break;
                case "Tuesday":
                    nameOfDay = "Marti";
                    break;
                case "Wednesday":
                    nameOfDay = "Miercuri";
                    break;
                case "Thursday":
                    nameOfDay = "Joi";
                    break;
                case "Friday":
                    nameOfDay = "Vineri";
                    break;
                case "Saturday":
                    nameOfDay = "Sambata";
                    break;
                case "Sunday":
                    nameOfDay = "Duminica";
                    break;
            }
            return nameOfDay;
        }
    }
}