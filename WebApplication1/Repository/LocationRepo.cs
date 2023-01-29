using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Interface;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Repository
{
    public class LocationRepo : ILocation
    {
        private readonly AppDbContext _context;

        public LocationRepo(AppDbContext context)
        {
            _context = context;
        }
        public (Location location, bool successful, string message) AddLocation(CreateLocationViewModel model)
        {
            Location locExists = _context.Locations.Where(x => x.Name == model.Name).FirstOrDefault();
            if (locExists != null)
            {
                return (null, false, "Already exists");
            }
            Location loc = new Location()
            {
                Name = model.Name,
            };
            _context.Locations.Add(loc);
            _context.SaveChanges();
            return (loc, true, "Location Created");
        }

        public (bool successful, string message) DeleteLocationById(int id)
        {
            Location locExists = _context.Locations.Where(x => x.Id == id).FirstOrDefault();
            if (locExists == null)
            {
                return (false, $"Location with id : {id} does not exist");
            }
            _context.Locations.Remove(locExists);
            _context.SaveChanges();

            return (true, "Location successfully deleted");
        }

        public (List<Location> locations, bool successful, string message) GetAllLocations()
        {
            List<Location> list = _context.Locations.ToList();
            return (list, true, "Locations returned successfully");
        }

        public (List<Location> locations, bool successful, string message) GetAllLocationsInDateRange(DateTime StartDate, DateTime EndDate)
        {
            var locations = _context.Locations.Where(x => x.DateCreated >= StartDate && x.DateCreated <= EndDate.AddHours(24)).ToList();

            return (locations, true, "Locations returned successfully");
        }

        public (LocationViewModel location, bool successful, string message) GetLocationById(int id)
        {
            Location locExists = _context.Locations.Where(x => x.Id == id).FirstOrDefault();
            if (locExists == null)
            {
                return (null, false, $"Location with id : {id} does not exist");
            }
            LocationViewModel loc = new LocationViewModel()
            {
                Id = locExists.Id,
                Name = locExists.Name,
            };

            return (loc, true, "Location Found");
        }

        public (Location location, bool successful, string message) UpdateLocation(CreateLocationViewModel model, int id)
        {
            Location locExists = _context.Locations.Where(x => x.Id == id).FirstOrDefault();
            if (locExists == null)
            {
                return (null, false, $"Location with id : {id} does not exist");
            }
            locExists.Name = model.Name;
            _context.Locations.Update(locExists);
            _context.SaveChanges();

            return (locExists, true, "Location Updated");
        }
    }
}
