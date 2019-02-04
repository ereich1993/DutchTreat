using DutchTreat.Models;

namespace DutchTreat.Services
{
    public interface IMailService
    {
        void send(User user);
    }
}