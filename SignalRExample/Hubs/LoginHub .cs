using Microsoft.AspNetCore.SignalR;
using SignalRExample.MessageTypes;
using SignalRExample.Models;
using SignalRExample.Utilities;

namespace SignalRExample.Hubs;

public class LoginHub : Hub<ILoginHub>
{
    private readonly IConfiguration _configuration;
    static List<User> _context = new List<User>();

    public LoginHub(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task Create(string userName, string password)
    {
        _context.Add(new User
        {
            Password = password,
            Username = userName
        });

        await Clients.Caller.Create(true);
    }

    public async Task Login(string userName, string password)
    {
        var user = _context.FirstOrDefault(u => u.Username == userName && u.Password == password);
        Token token = null;
        if (user != null)
        {
            var tokenHandler = new TokenHandler(_configuration);
            token = tokenHandler.CreateAccessToken(5);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
        }
        await Clients.Caller.Login(user != null ? token : null);
    }

    public async Task RefreshTokenLogin(string refreshToken)
    {
        var user = _context.FirstOrDefault(x => x.RefreshToken == refreshToken);
        Token token = null;
        if (user != null && user?.RefreshTokenEndDate > DateTime.Now)
        {
            TokenHandler tokenHandler = new TokenHandler(_configuration);
            token = tokenHandler.CreateAccessToken(1);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenEndDate = token.Expiration.AddMinutes(1);
        }
        await Clients.Caller.Login(user != null ? token : null);
    }
}