using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebService.Identity
{
    internal class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}