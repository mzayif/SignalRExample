// ReSharper disable CommentTypo
namespace SignalRExample.MessageTypes;


/// <summary>
/// Bu tip te bir interface sayesinde Client tarafındaki metotları tanımlayabiliyoruz.
/// Böylece yazım hatalarından kurtulmuş olunur.
/// </summary>
public interface IMessageHub
{
    Task ReceiveMessage(string message);
    Task UserJoined(string clientId);
    Task UserLeaved(string clientId);
    Task Clients(List<string> clientList);
    Task GetGroupClients(List<string> clientList);
}