using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery, int pageSize, int pageNumber);
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool include);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);

        Task<bool> IsCityExists(int cityId);

        Task AddPointOfInterestAsyncTask(int cityId,  PointOfInterest pointOfInterest);

        void RemovePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
