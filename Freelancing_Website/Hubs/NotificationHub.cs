// Freelancing_Website/Hubs/NotificationHub.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Freelancing_Website.Hubs
{
    [Authorize] 
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(System.Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        
    }
}
