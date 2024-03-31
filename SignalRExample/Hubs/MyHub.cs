using Microsoft.AspNetCore.SignalR;
using SignalRExample.Models;

namespace SignalRExample.Hubs;

public class MyHub: Hub
{
    static List<string> clients = new List<string>();
    static List<GroupClient> groupClients = new List<GroupClient>();

    public async Task SendMessageAsync(string message)
    {
        await Clients.All.SendAsync("receiveMessage", message);
    }

    public async Task ClientSendMessage(string connectionId, string message)
    {
        await Clients.Client(connectionId).SendAsync("receiveMessage", message);
    }

    public async Task ClientsSendMessage(string[] clients, string message)
    {
        await Clients.Clients(clients).SendAsync("receiveMessage", message);
    }

    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
    
    public async Task GroupSendMessage(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("receiveMessage", message);
    }

    public async Task GroupExceptSendMessage(string groupName, string[] excludedClients, string message)
    {
        await Clients.GroupExcept(groupName, excludedClients).SendAsync("receiveMessage", message);
    }

    public async Task GetGroupsClients(string[] groupsName)
    {
        List<string> datas = new List<string>();
        foreach (var groupName in groupsName)
        {
            datas.AddRange(groupClients.Where(g => g.GroupName == groupName).Select(c => c.ConnectionId).ToList());
        }
        await Clients.Caller.SendAsync("getGroupClients", datas);
    }


    public override async Task OnConnectedAsync()
    {
        clients.Add(Context.ConnectionId);
        await Clients.All.SendAsync("clients", clients);
        await Clients.All.SendAsync("userJoined", $"{Context.ConnectionId}");
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        clients.Remove(Context.ConnectionId);
        await Clients.All.SendAsync("clients", clients);
        await Clients.All.SendAsync("userLeaved", $"{Context.ConnectionId}");
    }
}