namespace Shower.Domain.RpcClient;

public interface IShowerConnection
{
    ShowerConnection Write<T>(ShowerCodes code, Action<T> callback);
}