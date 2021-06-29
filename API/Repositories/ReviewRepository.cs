using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.DTOs.Reviews;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public interface IReviewRepository
    {
        bool AddReview(AddReviewDto addReviewDto);
        bool DeleteReview(int reviewId, int pacientId);
        IEnumerable<ReviewDto> GetDoctorReviews(int doctorId);
        IEnumerable<ReviewDto> GetLoggedInPacientReviews(int pacientId);
    }

    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public bool AddReview(AddReviewDto addReviewDto)
        {
            var newReview = new Review
            {
                PacientId = (int)addReviewDto.PacientId,
                DoctorId = addReviewDto.DoctorId,
                Rating = addReviewDto.Rating,
                Content = addReviewDto.Content,
                DateAdded = DateTime.UtcNow
            };

            _context.Reviews.Add(newReview);

            if (_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteReview(int reviewId, int pacientId)
        {
            var reviewToDelete = _context.Reviews.FirstOrDefault(c=> c.Id == reviewId && c.PacientId == pacientId);

            if (reviewToDelete != null)
            {
                _context.Reviews.Remove(reviewToDelete);

                if (_context.SaveChanges() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<ReviewDto> GetDoctorReviews(int doctorId)
        {
            var doctorReviews = _context.Reviews
                .Where(c => c.DoctorId == doctorId)
                .Select(c => new ReviewDto {
                    Id = c.Id,
                    Rating = c.Rating,
                    Content = c.Content,
                    DateAdded = c.DateAdded,
                    PacientId = c.PacientId,
                    PacientFirstName = c.Pacient.FirstName,
                    PacientSecondName = c.Pacient.SecondName,
                    DoctorId = c.DoctorId,
                    DoctorFirstName = c.Doctor.FirstName,
                    DoctorSecondName = c.Doctor.SecondName,
                    DoctorProfilePicture = c.Doctor.Photos.FirstOrDefault(d => d.IsMain == true).Url
                })
                .OrderByDescending(c => c.DateAdded)
                .ToList();

            return doctorReviews;
        }

        public IEnumerable<ReviewDto> GetLoggedInPacientReviews(int pacientId)
        {
            var pacientReviews = _context.Reviews
                .Include(c => c.Pacient)
                .Include(c => c.Doctor.Photos)
                .Where(c => c.PacientId == pacientId)
                .Select(c => new ReviewDto {
                    Id = c.Id,
                    Rating = c.Rating,
                    Content = c.Content,
                    DateAdded = c.DateAdded,
                    PacientId = c.PacientId,
                    PacientFirstName = c.Pacient.FirstName,
                    PacientSecondName = c.Pacient.SecondName,
                    DoctorId = c.DoctorId,
                    DoctorFirstName = c.Doctor.FirstName,
                    DoctorSecondName = c.Doctor.SecondName,
                    DoctorProfilePicture = c.Doctor.Photos.FirstOrDefault(d => d.IsMain == true).Url
                })
                .OrderByDescending(c => c.DateAdded)
                .ToList();

            return pacientReviews;
        }
    }
}