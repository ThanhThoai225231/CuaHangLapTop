using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Services
{
    public interface IShippingService
    {
        Task<bool> ShipOrder(HoaDonViewModel model);
    }
}
