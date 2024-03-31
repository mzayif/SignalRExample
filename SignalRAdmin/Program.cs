using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRAdmin;

class Program
{
    static HubConnection connection;
    static async Task Main(string[] args)
    {
        connection = new HubConnectionBuilder().WithUrl("http://localhost:5143/message",
            options => options.AccessTokenProvider = () => Task.FromResult("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIm5iZiI6MTcxMTg4OTkxMiwiZXhwIjoxNzExODkwMjEyLCJpc3MiOiJIZXJoYW5naSBiaXIgbWV0aW4gZWtsZW5lYmlsaXIuIiwiYXVkIjoibG9jYWxob3N0In0.FyVbxCDd6MOqXbBT7U4hRNo9Em4VKHiWVPK0XlkSe6s")).Build();
        await connection.StartAsync();
        Console.WriteLine(connection.State);
        connection.On<string>("ReceiveMessage", message =>
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
            await connection.InvokeAsync("SendMessage", Console.ReadLine());
        }
    }
}
