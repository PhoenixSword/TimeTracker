using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TimeTracker
{
    [Authorize]
    public class ChatHub : Hub
    {

        public async Task Send(string message, string userName)
        {

            await Clients.All.SendAsync("Receive", message, userName);
        }

        public override async Task OnConnectedAsync()
        {
            var user = "";
            var claims = Context.User.Claims.ToDictionary(t => t.Type);
            if (claims.ContainsKey("name"))
            {
                user = claims["name"].Value;
            }
            else if (claims.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"))
            {
                user = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"].Value;
            }
            await Clients.All.SendAsync("Notify", $"{user} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = "";
            var claims = Context.User.Claims.ToDictionary(t => t.Type);
            if (claims.ContainsKey("name"))
            {
                user = claims["name"].Value;
            }
            else if (claims.ContainsKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"))
            {
                user = claims["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"].Value;
            }
            await Clients.All.SendAsync("Notify", $"{user} покинул чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}