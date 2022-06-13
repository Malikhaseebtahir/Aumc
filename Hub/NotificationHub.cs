using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Aumc.Hub
{
    public class NotificationHub : DynamicHub
    {
        public async Task RoomsUpdated(bool flag)
            => await Clients.Others.SendAsync("RoomsUpdated", flag);
    }
}