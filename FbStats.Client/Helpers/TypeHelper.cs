using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FbStats.Client.Helpers
{
    public static class TypeHelper
    {
        public static int DefaultInteger(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out int converted);
                return converted;
            }
            return 0;

        }

        public static double DefaultDouble(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                double.TryParse(value, out double converted);
                return converted;
            }
            return 0;
        }

        public static long DefaultLong(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                long.TryParse(value, out long converted);
                return converted;
            }
            return 0;
        }
    }
}
