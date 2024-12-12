using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Services
{
    public interface IPayPalService
    {
        string CreatePaymentUrl(PaymentInformationModel1 model, HttpContext context);
        

        public PaymentResponseModel1 PaymentExecute(IQueryCollection collections)
        {
            
            return new PaymentResponseModel1();
        }
    }
}
