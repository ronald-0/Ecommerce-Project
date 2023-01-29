using WebApplication1.Entities;
using WebApplication1.ViewModels.Create;
using WebApplication1.ViewModels.View;

namespace WebApplication1.Interface
{
    public interface ILocation
    {
        (LocationViewModel location, bool successful, string message) GetLocationById(int id);
        (Location location, bool successful, string message) AddLocation(CreateLocationViewModel model);
        (Location location, bool successful, string message) UpdateLocation(CreateLocationViewModel model, int id);
        (bool successful, string message) DeleteLocationById(int id);
        (List<Location> locations, bool successful, string message) GetAllLocations();
        (List<Location> locations, bool successful, string message) GetAllLocationsInDateRange(DateTime StartDate, DateTime EndDate);
    }
}
