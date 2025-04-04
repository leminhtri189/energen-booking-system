using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class BookingHub : Hub
    {
        public async Task NotifyUpdate()
        {
            await Clients.Others.SendAsync("ReceiveBookingUpdate");
        }
    }
}
