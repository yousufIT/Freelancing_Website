using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Freelancing_Website.Infrastructure
{
    
    public class EmailBasedUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var email = connection.User?.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(email)) return email;
            return connection.User?.Identity?.Name;
        }
    }
}
