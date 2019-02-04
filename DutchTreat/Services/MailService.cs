using DutchTreat.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Services
{
    public class MailService : IMailService
    {
        private ILogger<MailService> _logger;

        public MailService(ILogger<MailService>logger)
        {
            _logger = logger;
        }
        public void send(User user)
        {
            _logger.LogInformation($"username is :{user.UserName} password:{user.Password}");
        }
    }
}
