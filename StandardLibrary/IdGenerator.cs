using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public static class IdGenerator
    {
        const int maxLength = 6;

        private static char[] _base62chars =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            .ToCharArray();

        private static List<string> _savedIds = new List<string>();

        private static Random _random = new Random();

        public static string GetBase62()
        {
            var sb = new StringBuilder(maxLength);

            for (int i = 0; i < maxLength; i++)
                sb.Append(_base62chars[_random.Next(62)]);

            if (_savedIds.Count(id => id == sb.ToString()) == 0)
            {
                _savedIds.Add(sb.ToString());
                return sb.ToString();
            }

            // recursive until we get a unique id
            return GetBase62();
        }
    }
}
