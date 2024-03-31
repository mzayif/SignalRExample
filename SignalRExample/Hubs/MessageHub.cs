using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRExample.MessageTypes;
using SignalRExample.Models;

namespace SignalRExample.Hubs;

[Authorize(Roles = "Admin")]
public class MessageHub : Hub<IMessageHub>
{
    static List<string> clients = new List<string>();
    static List<GroupClient> groupClients = new List<GroupClient>();

    public async Task SendMessage(string message)
    {
        await Clients.Others.ReceiveMessage(message);
    }

    public async Task ClientSendMessage(string connectionId, string message)
    {
        await Clients.Client(connectionId).ReceiveMessage(message);
    }

    public async Task ClientsSendMessage(string[] clients, string message)
    {
        await Clients.Clients(clients).ReceiveMessage(message);
    }

    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task GroupSendMessage(string groupName, string message)
    {
        await Clients.Group(groupName).ReceiveMessage(message);
    }

    public async Task GroupExceptSendMessage(string groupName, string[] excludedClients, string message)
    {
        await Clients.GroupExcept(groupName, excludedClients).ReceiveMessage(message);
    }

    public async Task GetGroupsClients(string[] groupsName)
    {
        List<string> datas = new List<string>();
        foreach (var groupName in groupsName)
        {
            datas.AddRange(groupClients.Where(g => g.GroupName == groupName).Select(c => c.ConnectionId).ToList());
        }
        await Clients.Caller.GetGroupClients(datas);
    }

    public override async Task OnConnectedAsync()
    {
        clients.Add(Context.ConnectionId);
        await Clients.All.Clients(clients);
        await Clients.All.UserJoined($"{Context.ConnectionId}");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        clients.Remove(Context.ConnectionId);
        await Clients.All.Clients(clients);
        await Clients.All.UserLeaved($"{Context.ConnectionId}");
    }
}