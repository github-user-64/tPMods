using System;
using System.Collections.Generic;

namespace PlayerAccount.Utils
{
    internal static class Utils
    {
        public static bool A1<T>(List<Func<T, bool>> list, T v1)
        {
            try
            {
                if (list == null) return true;

                list.RemoveAll(i => i == null);

                bool rv = true;

                foreach (Func<T, bool> f in list)
                {
                    bool v = f.Invoke(v1);
                    if (v == false) rv = false;
                }

                return rv;
            }
            catch
            {
                return true;
            }
        }

        public static bool A2<T, T2>(List<Func<T, T2, bool>> list, T v1, T2 v2)
        {
            try
            {
                if (list == null) return true;

                list.RemoveAll(i => i == null);

                bool rv = true;

                foreach (Func<T, T2, bool> f in list)
                {
                    bool v = f.Invoke(v1, v2);
                    if (v == false) rv = false;
                }

                return rv;
            }
            catch
            {
                return true;
            }
        }
    }
}
