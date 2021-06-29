using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.DTOs.Reviews;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class ReviewController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository, DataContext context)
        {
            _reviewRepository = reviewRepository;
            _context = context;
        }

        [HttpPost("add")]
        public ActionResult Add(AddReviewDto addReviewDto)
        {
            if (User.IsInRole("Doctor"))
            {
                return BadRequest("Nu aveti dreptul de a adauga review-uri!");
            }

            var userPacientId = User.GetUserId();

            var pacientId = _context.Pacients.FirstOrDefault(c => c.UserId == userPacientId).Id;

            addReviewDto.PacientId = pacientId;

            if (_reviewRepository.AddReview(addReviewDto))
            {
                return Ok();
            }

            return BadRequest("Nu am putut adauga review-ul!");
        }

        [HttpDelete("delete")]
        public ActionResult Delete(int reviewId)
        {
            if (User.IsInRole("Doctor"))
            {
                return BadRequest("Nu aveti dreptul de a sterge review-uri!");
            }

            var userPacientId = User.GetUserId();

            var pacientId = _context.Pacients.FirstOrDefault(c => c.UserId == userPacientId).Id;

            if (_reviewRepository.DeleteReview(reviewId, pacientId))
            {
                return Ok();
            }

            return BadRequest("Nu am putut sterge review-ul!");
        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<ReviewDto>> Get()
        {
            if (User.IsInRole("Pacient"))
            {
                var userPacientId = User.GetUserId();

                var pacientId = _context.Pacients.FirstOrDefault(c => c.UserId == userPacientId).Id;

                return Ok(_reviewRepository.GetLoggedInPacientReviews(pacientId));
            }

            var userDoctorId = User.GetUserId();

            var doctorId = _context.Doctors.FirstOrDefault(c => c.UserId == userDoctorId).Id;

            return Ok(_reviewRepository.GetDoctorReviews(doctorId));
        }

        [HttpGet("get-doctor-reviews")]
        public ActionResult<IEnumerable<ReviewDto>> GetDoctorReview(int doctorId)
        {
            if (User.IsInRole("Pacient"))
            {
                return Ok(_reviewRepository.GetDoctorReviews(doctorId));
            }

            return BadRequest("Upss.eroare!");
        }
    }
}