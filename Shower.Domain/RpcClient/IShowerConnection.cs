namespace Shower.Domain.RpcClient;

public interface IShowerConnection
{
    ShowerConnection Write<T>(ShowerCode code, Action<T> callback);
}