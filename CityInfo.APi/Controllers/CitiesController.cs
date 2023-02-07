using AutoMapper;
using CityInfo.APi.Models;
using CityInfo.APi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.APi.Controllers
{
    [ApiController]
    [Route("api/cities")]

    public class CitiesController: ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

       // public CitiesDataStore CitiesDataStore { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();

            //without AutoMapper
            //var results = new List<CityWithoutPointsOfInterestDto>();
            //foreach(var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = cityEntity.Id,
            //        Description = cityEntity.Description,
            //        Name = cityEntity.Name
            //    });
            //}
            //return Ok(results);

            //with AutoMapper
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetCity(int id,
            bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInterestDto>(city));

        }
    }
}


