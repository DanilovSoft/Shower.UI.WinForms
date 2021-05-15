using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ShowerUI.Common;

namespace ShowerTcpClient
{
    public static class ConnectionHelper
    {
        private const int PortNumber = 333;
        private const string Address = "10.2.2.11";

        /// <exception cref="OperationCanceledException"/>
        public static async Task<ShowerConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var tcp = new TcpClient();
            try
            {
                using (cancellationToken.Register(static s => ((IDisposable)s).Dispose(), tcp, useSynchronizationContext: false))
                {
                    await tcp.ConnectAsync(Address, PortNumber).ConfigureAwait(false);
                }
                var connection = new ShowerConnection(tcp);
                tcp = null;
                return connection;
            }
            catch when (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw;
            }
            finally
            {
                tcp?.Dispose();
            }
        }
    }
}
