using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CitiesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CitiesController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("cities")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cities =  await _context.Cities
            .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return Ok(cities);
        }

        [HttpGet("regions")]
        public async Task<ActionResult<IEnumerable<RegisterDto>>> GetRegions()
        {
            var regions = await _context.Regions
            .ProjectTo<RegionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return Ok(regions);
        }
    }
}