using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace WebService.Identity
{
    internal class EmailService : IIdentityMessageService
    {
    
            public Task SendAsync(IdentityMessage message)
            {
                // Create the mail message
                var mailMessage = new MailMessage(
                    "myapp@myapp.com",
                    message.Destination,
                    message.Subject,
                    message.Body
                    );

                // Send the message
                SmtpClient client = new SmtpClient();
                client.SendAsync(mailMessage, null);

                return Task.FromResult(true);
            }
        }
    }
