using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs;

public class MyHub: Hub
{
    public async Task SendMessageAsync(string message)
    {
        await Clients.All.SendAsync("receiveMessage", message);
    }
}