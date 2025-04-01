using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class BookingHub : Hub
    {
        public async Task SendBookingUpdate(Guid bookingId)
        {
            await Clients.All.SendAsync("ReceiveBookingUpdate", bookingId);
        }
    }
}
