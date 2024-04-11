using System.Diagnostics.CodeAnalysis;

namespace ShowerUI.Common;

public static class NullableHelper
{
    [return: NotNullIfNotNull("value")]
    public static T? SetNull<T>(ref T? value) where T : class
    {
        var copy = value;
        value = null;
        return copy;
    }
}
