using System.ComponentModel.DataAnnotations;

namespace Shower.Domain.RpcClient;

/// <summary>
/// Адрес контроллера душа.
/// </summary>
public sealed class ShowerConnectionOptions
{
    public const string SectionName = "ShowerConnection";

    /// <summary>
    /// IP-адрес или имя хоста контроллера.
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "Укажите адрес контроллера в ShowerConnection:Host.")]
    public string Host { get; set; } = "";

    [Range(1, 65535, ErrorMessage = "Порт контроллера ShowerConnection:Port должен быть от 1 до 65535.")]
    public int Port { get; set; }
}
