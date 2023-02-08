using AutoMapper;
using CityInfo.APi.Models;
using CityInfo.APi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.APi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")] 
    [ApiVersion("2.0")]
    // [Route("api/cities")]
    [Route("api/v{version:apiVersion}/cites")]

    public class CitiesController: ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

       // public CitiesDataStore CitiesDataStore { get; }
       /// <summary>
       /// Get cities
       /// </summary>
       /// <param name="name"></param>
       /// <param name="searchQuery"></param>
       /// <param name="pageNumber"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(
           string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {

            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            var (cityEntities, paginationMetadata) = await _cityInfoRepository.
                GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

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
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

        /// <summary>
        /// Get city by Id
        /// </summary>
        /// <param name="id">The id of the city to get</param>
        /// <param name="includePointsOfInterest">whether or  not to include the points of interest</param>
        /// <returns>An IActionResult</returns>
        /// <response code= "200">Returns the requested City</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


