using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _repository;

        public CitiesController(ICityInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutInterestsDto>>> GetCities( [FromQuery] string? name, [FromQuery] string? searchQuery
        , [FromQuery] int pageSize = 10 , [FromQuery] int pageNumber = 1)
        {
            var cities = await _repository.GetCitiesAsync(name, searchQuery, pageSize,pageNumber);


            var listOfCities = new List<CityWithoutInterestsDto>();
            foreach (var city in cities)
            {
                listOfCities.Add(new CityWithoutInterestsDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                });
            }

            return listOfCities;

            /*var cities = await _repository.GetCitiesAsync();

            var listOfCities = new List<CityWithoutInterestsDto>();
            foreach (var city in cities)
            {
                listOfCities.Add(new CityWithoutInterestsDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                });
            }

                
            return Ok(listOfCities);*/
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool include)
        {
            var city = await _repository.GetCityAsync(id, include);
           
            if (include)
            {
                List<PointOfInterestDto> lstPointOfInterestDtos = new List<PointOfInterestDto>();

                foreach (var point in city.PointOfInterests)
                {
                    lstPointOfInterestDtos.Add(new PointOfInterestDto()
                    {
                        Id = point.Id,
                        Name = point.Name,
                        Description = point.Description,
                    });
                }


                var result = new CityDto()
                {
                    Name = city.Name,
                    Id = city.Id,
                    Description = city.Description,
                    PointOfInterests = lstPointOfInterestDtos
                };

                return Ok(result);
            }
            else
            {
                var result = new CityWithoutInterestsDto()
                {
                    Name = city.Name,
                    Description = city.Description,
                    Id = city.Id
                };
                return Ok(result);
            }

        }

    }
}
