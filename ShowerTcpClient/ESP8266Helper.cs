using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    public static class ESP8266Helper
    {
        /// <summary>
        /// Экранирует запятые, кавычки и слэши.
        /// </summary>
        public static string EscapeString(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return input
                .Replace("/", "//")
                .Replace(",", "/,")
                .Replace(@"""", @"/""");
        }

        /// <summary>
        /// Формирует часть команды вида <c>AT+CWJAP_CUR="abc","0123456789","ca:d7:19:d8:a6:44"</c>.
        /// </summary>
        /// <param name="ssid">Имя сети.</param>
        /// <param name="password">Пароль сети.</param>
        /// <param name="bsid">MAC сети.</param>
        /// <returns>Строка вида <c>"abc","0123456789","ca:d7:19:d8:a6:44"</c>.</returns>
        public static string CreateSetCurrentAPCommand(string ssid, string password, PhysicalAddress? bsid = null)
        {
            if (ssid == null)
                throw new ArgumentNullException(nameof(ssid));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            if (ssid.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(ssid));

            if (password.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(password));

            string escSsis = EscapeString(ssid);
            string escPass = EscapeString(password);

            if (bsid == null)
            {
                return $@"""{escSsis}"",""{escPass}""";
            }
            else
            {
                string sBsid = BitConverter.ToString(bsid.GetAddressBytes()).Replace('-', ':').ToLowerInvariant();
                return $@"""{escSsis}"",""{escPass}"",""{sBsid}""";
            }
        }
    }
}
