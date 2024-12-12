using ThanhThoaiRestaurant.Models;


namespace ThanhThoaiRestaurant.Services;
public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}