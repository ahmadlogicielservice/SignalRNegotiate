using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SignalRNegotiate
{
    [Authorize(AuthenticationSchemes = "ChatJwt")]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var sender = GetSenderName();

            Console.WriteLine($"Connected user: {sender}");

            await Clients.All.SendAsync(
                "ReceiveMessage",
                "System",
                $"{sender} connected."
            );

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var sender = GetSenderName();

            Console.WriteLine($"Disconnected user: {sender}");

            await Clients.All.SendAsync(
                "ReceiveMessage",
                "System",
                $"{sender} disconnected."
            );

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            var sender = GetSenderName();

            await Clients.All.SendAsync(
                "ReceiveMessage",
                sender,
                message
            );
        }

        private string GetSenderName()
        {
            // Preferred: Name claim
            var name = Context.UserIdentifier;

            if (!string.IsNullOrWhiteSpace(name))
                return name;

            // Fallback: NameIdentifier or "unknown"
            return Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? "unknown";
        }
    }
}
