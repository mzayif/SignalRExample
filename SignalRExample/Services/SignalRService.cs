using Microsoft.AspNetCore.SignalR;
using SignalRExample.Hubs;

namespace SignalRExample.Services;

public class SignalRService
{
    private readonly IHubContext<MyHub> _hubContext;
    public SignalRService(IHubContext<MyHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("receiveMessage", message);
    }
}