using System;

namespace ShowerTcpClient
{
    public interface IShowerConnection
    {
        ShowerConnection Write<T>(ShowerCodes code, Action<T> callback);
    }
}