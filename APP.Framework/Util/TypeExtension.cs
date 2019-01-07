using System;

namespace APP.Framework.Util
{
    public static class TypeExtension
    {
        public static string ToString(this Guid guid, int length)
        {
            var str = guid.ToString("n");
            return str.Substring(str.Length - length);
        }
    }
}
