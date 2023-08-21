using CityInfo.API.Entities;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/[controller]")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ICityInfoRepository _repository;
        private readonly IMailService mailService;
        private CitiesDataStore _dataStore;


        public PointsOfInterestController(CitiesDataStore dataStore, IMailService mailService, ICityInfoRepository repository)
        {
            this.mailService = mailService;
            _dataStore = dataStore;
            _repository = repository;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterests(int cityId)
        {
            if (!await _repository.IsCityExists(cityId))
            {
                return NotFound();
            }
            var lstDtos = new List<PointOfInterestDto>();

            var lst = await _repository.GetPointsOfInterestForCityAsync(cityId);
            foreach (var obj in lst)
            {
                lstDtos.Add(new PointOfInterestDto()
                {
                    Id = obj.Id,
                    Description = obj.Description,
                    Name = obj.Name
                });
            }

            return lstDtos;

        }

        [HttpGet("{pointOfInterestId}" ,Name = "GetPointOfInterest") , ]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterestOne(int cityId
            , int pointOfInterestId)
        {
            /*var city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
            */
            if (!await _repository.IsCityExists(cityId))
            {
                return NotFound();
            }

            var point = await _repository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            var pointDto = new PointOfInterestDto()
            {
                Id = point.Id,
                Description = point.Description,
                Name = point.Name
            };
            return Ok(pointDto);

        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
            

            /*ar city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            int maxID = _dataStore.Cities.SelectMany(c => c.PointOfInterests).Max(x => x.Id);

            var final = new PointOfInterestDto()
            {
                Id = ++maxID,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterests.Add(final);*/
            if (!await _repository.IsCityExists(cityId))
            {
                return NotFound();
            }


            var final = new PointOfInterest()
            {
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description,
            };

            await _repository.AddPointOfInterestAsyncTask(cityId, final);
            await _repository.SaveChangesAsync();

            var forRoute = new PointOfInterestDto()
            {
                Id = final.Id,
                Description = final.Description,
                Name = final.Name
            };



            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = final.Id
                },
                forRoute
            );

        }

        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestUpdateDto pointOfInterest)
        {

            var city = _dataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterests
                .FirstOrDefault(c => c.Id == pointOfInterestId);

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();

        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _repository.IsCityExists(cityId))
            {
                return NotFound();
            }

            PointOfInterest? pointOfInterestFromRep = await _repository.GetPointOfInterestAsync(cityId, pointOfInterestId);

            if (pointOfInterestFromRep == null)
            {
                return NotFound();
            }

            _repository.RemovePointOfInterest(pointOfInterestFromRep);
            await _repository.SaveChangesAsync();

            return NoContent();
            /*
            var city = _dataStore.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city==null)
            {
                return NoContent();
            }

            var pointOfInterest = city.PointOfInterests.FirstOrDefault(x => x.Id == pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            city.PointOfInterests.Remove(pointOfInterest);
            

            mailService.send("subject", "item was deleted successfully");
            return NoContent();
            */


        }



    }
}
