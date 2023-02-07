using CityInfo.APi.DbContexts;
using CityInfo.APi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.APi.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            //return all the cities
            //return await _context.Cities.ToListAsync();
            //return all the cities in an ordered manner.
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        //For filetering
        public async Task<IEnumerable<City>> GetCitiesAsync(string? name, string? searchQuery)
        {
            if (string.IsNullOrEmpty(name)
                && string.IsNullOrWhiteSpace(searchQuery))
            {
                return await GetCitiesAsync();
            }

            //collection due to deffered excursion
            var collection = _context.Cities as IQueryable<City>;

            //implement filtering
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            //implement flitering
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            return await collection.OrderBy(c => c.Name).ToListAsync();

     
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointsOfInterestId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId && p.Id == pointsOfInterestId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityid)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityid).ToListAsync();
        }

        //check if city exist
        public async Task<bool> CityExistAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        //Add point of interest to city

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }
    }
}
