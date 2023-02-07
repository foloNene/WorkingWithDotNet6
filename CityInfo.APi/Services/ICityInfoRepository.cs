using CityInfo.APi.Entities;

namespace CityInfo.APi.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityid);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId);
        Task<bool> CityExistAsync(int cityId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
