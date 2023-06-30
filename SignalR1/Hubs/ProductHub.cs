using Microsoft.AspNetCore.SignalR;
using SignalR1.Models;

namespace SignalR1.Hubs
{
    public class ProductHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int totalUsers { get; set; } = 0;

        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("UpdateTotalViews", TotalViews);

            //await Clients.Users("tuanlbs78@gmail.com").SendAsync("UpdateTotalViews", TotalViews);
        }

        public override Task OnConnectedAsync()
        {
            totalUsers++;
            Clients.All.SendAsync("UpdateTotalUsers", totalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            totalUsers--;
            Clients.All.SendAsync("UpdateTotalUsers", totalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateProduct(int totalOrders)
        {
    
            await Clients.All.SendAsync("UpdatedProduct", totalOrders);
        }

        public async Task DeleteProduct(string pid)
        {
          
            await Clients.All.SendAsync("DeletedProduct", pid);
        }
    }
}
