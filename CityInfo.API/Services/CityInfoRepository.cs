using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        CityInfoContext _context;
        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

      

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery, int pageSize, int pageNumber)
        {

            var collection = _context.Cities as IQueryable<City>;

            if (!String.IsNullOrEmpty(name))
            {
                name = name.Trim();
                collection = collection.Where(x => x.Name == name);
            }

            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(x => x.Name.Contains(searchQuery) || x.Description.Contains(searchQuery));
            }




            return await collection.Skip((pageSize * (pageNumber - 1))).Take(pageSize).ToListAsync();
        }


        public async Task<City?> GetCityAsync(int cityId, bool include )
        {
            if (include)
            {
                return await _context.Cities.Include(x => x.PointOfInterests)
                    .Where(x => x.Id == cityId).FirstOrDefaultAsync();
            }
            else
            {
                return await _context.Cities.Where(x => x.Id == cityId).FirstOrDefaultAsync();
            }
        }

        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterest.Where(x => x.CityId == cityId && x.Id == pointOfInterestId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointOfInterest.Where(x => x.CityId == cityId).ToListAsync();
        }

        public async Task<bool> IsCityExists(int cityId)
        {
            return await _context.Cities.AnyAsync(x => x.Id == cityId);
        }


        public async Task AddPointOfInterestAsyncTask(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);

            if (city != null)
            {
                city.PointOfInterests.Add(pointOfInterest);
            }
        }



        public void RemovePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterest.Remove(pointOfInterest);
        }



        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
