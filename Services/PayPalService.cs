using System.Net;
using ThanhThoaiRestaurant.Models;
using PayPal.Api;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Payments;
using ThanhThoaiRestaurant.Services;
using PayPalCheckoutSdk.Orders;

namespace ThanhThoaiRestaurant.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly IConfiguration _configuration;
        private const double ExchangeRate = 22_863.0;

        public PayPalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static double ConvertVndToDollar(double vnd)
        {
            var total = Math.Round(vnd / ExchangeRate, 2);

            return total;
        }

        public string CreatePaymentUrl(PaymentInformationModel1 model, HttpContext context)
        {
            var envSandbox = new SandboxEnvironment(_configuration["Paypal:ClientId"], _configuration["Paypal:SecretKey"]);
            var client = new PayPalHttpClient(envSandbox);

            var paypalOrderId = DateTime.Now.Ticks;
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            var amount = new PayPalCheckoutSdk.Orders.Money() // Sử dụng kiểu dữ liệu 'Money' từ 'PayPalCheckoutSdk.Orders'
            {
                Value = ConvertVndToDollar(model.Amount).ToString(),
                CurrencyCode = "USD"
            };

            var purchaseUnitRequest = new PayPalCheckoutSdk.Orders.Item() // Chỉ rõ kiểu 'Item' từ namespace 'PayPalCheckoutSdk.Orders'
            {
                Name = $"Order: {model.OrderDescription}",
                UnitAmount = amount,
                Quantity = "1",
                Sku = "sku",
                Category = "DIGITAL_GOODS"
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
        {
            new PurchaseUnitRequest()
            {
                AmountWithBreakdown = new PayPalCheckoutSdk.Orders.AmountWithBreakdown()
                {
                    CurrencyCode = "USD",
                    Value = ConvertVndToDollar(model.Amount).ToString(),
                },
                Description = $"Invoice #{model.OrderDescription}",
                InvoiceId = paypalOrderId.ToString(),
                Items = new List<PayPalCheckoutSdk.Orders.Item>() { purchaseUnitRequest }, // Chỉ rõ kiểu 'Item' từ namespace 'PayPalCheckoutSdk.Orders'
            }
        },
                ApplicationContext = new ApplicationContext()
                {
                    BrandName = "ThanhThoaiRestaurant",
                    ReturnUrl = $"{urlCallBack}?payment_method=PayPal&success=1&order_id={paypalOrderId}",
                    CancelUrl = $"{urlCallBack}?payment_method=PayPal&success=0&order_id={paypalOrderId}",
                    UserAction = "CONTINUE"
                }
            });

            var response = client.Execute(request).Result; // Đợi kết quả từ phương thức Execute

            var paymentUrl = "";
            if (response.StatusCode is HttpStatusCode.Accepted or HttpStatusCode.OK or HttpStatusCode.Created)
            {
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>(); // Sử dụng kiểu dữ liệu 'Order' từ 'PayPalCheckoutSdk.Orders'

                foreach (var link in result.Links)
                {
                    if (link.Rel.ToLower().Trim().Equals("approve"))
                    {
                        paymentUrl = link.Href;
                        break;
                    }
                }
            }

            return paymentUrl;
        }



        public PaymentResponseModel1 PaymentExecute(IQueryCollection collections)
        {
            var response = new PaymentResponseModel1();

            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("order_description"))
                {
                    response.OrderDescription = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("transaction_id"))
                {
                    response.TransactionId = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("order_id"))
                {
                    response.OrderId = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payment_method"))
                {
                    response.PaymentMethod = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("success"))
                {
                    response.Success = Convert.ToInt32(value) > 0;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("paymentid"))
                {
                    response.PaymentId = value;
                }

                if (!string.IsNullOrEmpty(key) && key.ToLower().Equals("payerid"))
                {
                    response.PayerId = value;
                }
            }

            return response;
        }
    }
}