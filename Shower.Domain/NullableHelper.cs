using System.Diagnostics.CodeAnalysis;

namespace Shower.Domain;

public static class NullableHelper
{
    [return: NotNullIfNotNull(nameof(value))]
    public static T? SetNull<T>(ref T? value) where T : class
    {
        var copy = value;
        value = null;
        return copy;
    }
}
