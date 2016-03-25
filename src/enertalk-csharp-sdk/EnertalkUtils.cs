using System;

namespace Enertalk
{
    internal static class EnertalkUtils
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            var result = (dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return (long)result;
        }
    }
}
