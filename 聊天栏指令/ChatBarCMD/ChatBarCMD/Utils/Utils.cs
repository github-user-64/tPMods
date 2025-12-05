using System;
using System.Collections.Generic;

namespace ChatBarCMD.Utils
{
    internal static class Utils
    {
        public static bool A1<T>(this List<Func<T, bool>> list, T v1)
        {
            bool rv = true;

            if (list == null) return true;

            list.RemoveAll(i => i == null);

            foreach (var f in list)
            {
                try
                {
                    bool v = f.Invoke(v1);
                    if (v == false) rv = false;
                }
                catch { }
            }

            return rv;
        }

        public static bool A2<T, T2>(this List<Func<T, T2, bool>> list, T v1, T2 v2)
        {
            bool rv = true;

            if (list == null) return true;

            list.RemoveAll(i => i == null);

            foreach (var f in list)
            {
                try
                {
                    bool v = f.Invoke(v1, v2);
                    if (v == false) rv = false;
                }
                catch { }
            }

            return rv;
        }
    }
}
