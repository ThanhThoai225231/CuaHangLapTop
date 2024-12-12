using Newtonsoft.Json;
using System.Text;
using ThanhThoaiRestaurant.Models;
using ThanhThoaiRestaurant.Services;

public class ShippingService : IShippingService
{

    private readonly string _clientId;
    private readonly string _clientSecret;

    private readonly HttpClient _httpClient;

    public ShippingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public ShippingService(IConfiguration configuration)
    {
        _clientId = configuration["ShippingService:ClientId"];
        _clientSecret = configuration["ShippingService:ClientSecret"];
    }

    public async Task<bool> ShipOrder(HoaDonViewModel model)
    {
        try
        {
            // Tạo đối tượng chứa thông tin đơn hàng để gửi đi
            var shippingInfo = new ShippingInfo
            {
                OrderId = model.MaDonHang,
                CustomerName = model.NguoiNhan,
                CustomerPhone = model.SDTNN,
                CustomerAddress = model.DiaChiNhan,
                Note = model.GhiChu,
                // Các thông tin khác cần thiết
            };

            // Chuyển đối tượng shippingInfo sang chuỗi JSON
            var jsonContent = JsonConvert.SerializeObject(shippingInfo);

            // Tạo nội dung HTTP từ chuỗi JSON
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Gửi yêu cầu POST tới API của Goship để gửi đơn hàng
            var response = await _httpClient.PostAsync("https://www.goship.com/api/", httpContent);

            // Kiểm tra xem yêu cầu đã thành công (HTTP status code 200-299) hay không
            if (response.IsSuccessStatusCode)
            {
                // Trả về true nếu đơn hàng được gửi thành công
                return true;
            }
            else
            {
                // Nếu có lỗi, bạn có thể xử lý ở đây, ví dụ: log lỗi, thông báo người dùng, v.v.
                // Trả về false nếu có lỗi xảy ra
                return false;
            }
        }
        catch (Exception ex)
        {
            // Xử lý nếu có lỗi khi gửi đơn hàng
            // Log lỗi, thông báo cho người dùng, hoặc thực hiện các hành động khác tùy theo yêu cầu
            // Trả về false nếu có lỗi
            return false;
        }
    }
}

// Định nghĩa một lớp để lưu trữ thông tin đơn hàng sẽ được gửi đi
public class ShippingInfo
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerAddress { get; set; }
    public string Note { get; set; }
    // Các thuộc tính khác cần thiết
}

