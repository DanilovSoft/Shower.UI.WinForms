using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerUI.Common
{
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
}
