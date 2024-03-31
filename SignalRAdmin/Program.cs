using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRAdmin;

class Program
{
    static HubConnection connection;
    static async Task Main(string[] args)
    {
        connection = new HubConnectionBuilder().WithUrl("http://localhost:5143/myhub").Build();
        await connection.StartAsync();
        Console.WriteLine(connection.State);
        connection.On<string>("receiveMessage", message =>
        {
            Console.WriteLine($"--->{message}");
        });
        connection.On<string>("userJoined", message =>
        {
            Console.WriteLine($"{message} katıldı.");
        });
        connection.On<string>("userLeaved", message =>
        {
            Console.WriteLine($"{message} ayrıldı.");
            Console.WriteLine("*****************************");
        });


        while (true)
        {
            Console.WriteLine("Gönderilecek mesajı yazınız.");
            await connection.InvokeAsync("SendMessageAsync", Console.ReadLine());
        }
    }
}
