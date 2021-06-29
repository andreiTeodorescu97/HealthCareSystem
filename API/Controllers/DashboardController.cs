using System;
using System.Collections.Generic;
using System.Linq;
using API.Constants;
using API.Data;
using API.DTOs.Dashboard;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class DashboardController : BaseApiController
    {
        private readonly DataContext _context;
        public DashboardController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("pacient-statistics")]
        public PacientStatisticsDto GetPublisherStatistics()
        {
            var userId = User.GetUserId();

            var pacientId = _context.Pacients.FirstOrDefault(c => c.UserId == userId).Id;

            var result = new PacientStatisticsDto();

            var lastAppoinment = _context.Appoinments.OrderByDescending(c => c.DateId).FirstOrDefault(c => c.PacientId == pacientId);

            if (lastAppoinment != null)
            {
                result.LastAppoinmentDate = lastAppoinment.AppoinmentDate;

                var lastAppoinmentDateId = (Int32.Parse(result.LastAppoinmentDate.Substring(6, 4)) * 100 +
                    Int32.Parse(result.LastAppoinmentDate.Substring(3, 2))) * 100 +
                    Int32.Parse(result.LastAppoinmentDate.Substring(0, 2));

                result.NumberOfDaysFromLastAppoinment = 25;
                result.NumberOfRecipes = _context.Recipes.Count(c => c.PacientId == pacientId);
                result.NumberOfAppoinments = _context.Appoinments.Count(c => c.PacientId == pacientId);
            }
            else
            {
                result.LastAppoinmentDate = null;
                result.NumberOfDaysFromLastAppoinment = 0;
                result.NumberOfRecipes = 0;
                result.NumberOfAppoinments = 0;
                result.LastHeight = 0;
                result.LastWeight = 0;
                result.LastTemperature = 0;
                result.LastHeartRate = 0;

                return result;
            }

            var lastConsultation = _context.Consultations.OrderByDescending(c => c.DateAdded).FirstOrDefault(c => c.PacientId == pacientId);

            if (lastConsultation != null)
            {
                result.LastHeight = lastConsultation.Height;
                result.LastWeight = lastConsultation.Weight;
                result.LastTemperature = lastConsultation.Temperature;
                result.LastHeartRate = lastConsultation.HeartRate;
            }
            else
            {
                result.LastHeight = 0;
                result.LastWeight = 0;
                result.LastTemperature = 0;
                result.LastHeartRate = 0;
            }


            return result;
        }

        [HttpGet("doctor-statistics")]
        public ActionResult<DoctorStatisticsDto> GetDoctorStatistics()
        {
            var userId = User.GetUserId();

            var doctorId = _context.Doctors.FirstOrDefault(c => c.UserId == userId).Id;

            var result = new DoctorStatisticsDto();

            var dateToday = DateTime.UtcNow.ToString("dd/MM/yyyy");

            result.NumberOfAppoinmentsToday =
                _context.Appoinments.Count(c => c.DoctorId == doctorId && c.AppoinmentDate == dateToday);
            result.NumberOfFinalizedAppoinments =
                _context.Appoinments.Count(c => c.DoctorId == doctorId && c.StatusId == (int)AppoinmentStatuses.Finalized);
            result.NumberOfReviews = _context.Reviews.Count(c => c.DoctorId == doctorId);
            result.NumberOfPacients = _context.PacientHistories.Where(c => c.DoctorId == doctorId).Count();

            return Ok(result);
        }

        [HttpGet("doctor-appoinments-statistics")]
        public ActionResult<List<AppoinmentReasonGraph>> GetAppoinmentsStatistics()
        {
            if (User.IsInRole("Pacient"))
            {
                return BadRequest("Nu aveti acces!");
            }
            var result = new List<AppoinmentReasonGraph>();

            var reasonsList = _context.Appoinments
                    .Select(c => c.Reason)
                    .Distinct()
                    .ToList();

            foreach (var item in reasonsList)
            {
                var nr = _context.Appoinments.Count(c => c.Reason == item);
                result.Add(new AppoinmentReasonGraph { ReasonName = item, Number = nr });
            }

            return Ok(result);
        }

        [HttpGet("doctor-vaccines-statistics")]
        public ActionResult<List<VaccineGraph>> GetVaccinesStatistics()
        {
            if (User.IsInRole("Pacient"))
            {
                return BadRequest("Nu aveti acces!");
            }
            var result = new List<VaccineGraph>();

            var vaccinesList = _context.Vaccines
                    .Select(c => c.Name)
                    .ToList();

            foreach (var item in vaccinesList)
            {
                var nr = _context.VaccineXPacients.Count(c => c.Vaccine.Name == item);
                if (nr != 0)
                {
                    result.Add(new VaccineGraph { Name = item, Number = nr });
                }
            }

            return Ok(result);
        }
    }
}