using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AppoinmentsRepository : IAppoinmentsRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AppoinmentsRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<FreeHourDto>> GetAvailableHours(int doctorId, DateTime date)
        {
            var workHoursForDay = await _context.WorkDays.Where(d => d.DoctorId == doctorId & d.Day == GetEnglishDayName(date)).SingleOrDefaultAsync();

            if(workHoursForDay == null)
            {
                return null;
            }

            var dateString = date.ToString("dd/MM/yyyy");

            var appoimentsForDoctor = await _context.Appoinments.Where(d => d.DoctorId == doctorId & d.AppoinmentDate.Contains(dateString)).ToListAsync();

            //An appoiment will have be default 30 minutes
            var numberOfSlots = (workHoursForDay.EndTimeSpan - workHoursForDay.StartTimeSpan)/30;

            var from = workHoursForDay.StartTimeSpan;
            var to = workHoursForDay.StartTimeSpan + 30;
            var availableSpots = new List<FreeHourDto>();
            var id = 0;

            for(int i=0; i<numberOfSlots; i++)
            {
                if(!appoimentsForDoctor.Any(d => d.AppoinmentStartSpan == from && d.AppoinmentEndSpan == to))
                {                    
                    availableSpots.Add(new FreeHourDto{
                        Id = id,
                        FromHour = TimeSpan.FromMinutes((int)from).ToString().Remove(TimeSpan.FromMinutes((int)from).ToString().Length-3),
                        ToHour = TimeSpan.FromMinutes((int)to).ToString().Remove(TimeSpan.FromMinutes((int)to).ToString().Length-3),
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
            var time = TimeSpan.FromMinutes((int)makeAnAppoinmentDto.FromTimeSpan).ToString() + "-" +
                        TimeSpan.FromMinutes((int)makeAnAppoinmentDto.ToTimeSpan).ToString();

            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(makeAnAppoinmentDto.DayUnixTime);

            var dateId = (dtDateTime.Year * 100 + dtDateTime.Month)*100 + dtDateTime.Day;

            var newAppoinment = new Appoinment{
                AppoinmentDate = dtDateTime.ToString("dd/MM/yyyy"),
                AppoinmentHour = time,
                AppoinmentStartSpan = makeAnAppoinmentDto.FromTimeSpan,
                AppoinmentEndSpan = makeAnAppoinmentDto.ToTimeSpan,
                Reason = makeAnAppoinmentDto.Reason,
                DateId = dateId,
                PacientId = pacientId,
                DoctorId = makeAnAppoinmentDto.DoctorId,
            };

             _context.Appoinments.Add(newAppoinment);

             return await _context.SaveChangesAsync() > 0;
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