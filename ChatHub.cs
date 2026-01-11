using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRNegotiate
{

    [Authorize(AuthenticationSchemes = "ChatJwt")]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            Console.WriteLine($"Connected user: {userId}");
            return base.OnConnectedAsync();
        }

        public Task SendMessage(string message)
        {
            return Clients.All.SendAsync("message", message);
        }
    }
}
