using Microsoft.AspNetCore.SignalR;
using SignalRExample.Hubs;

namespace SignalRExample.Services;

public class SignalRService
{
    private readonly IHubContext<MessageHub> _hubContext;
    public SignalRService(IHubContext<MessageHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("receiveMessage", message);
    }
}