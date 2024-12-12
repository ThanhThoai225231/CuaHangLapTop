using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Models
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
