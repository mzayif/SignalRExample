using SignalRExample.Models;

namespace SignalRExample.MessageTypes;

public interface ILoginHub
{
    Task Login(Token? token);
    Task Create(bool result);
}
