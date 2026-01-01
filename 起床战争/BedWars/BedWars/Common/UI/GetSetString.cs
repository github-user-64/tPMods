using System;

namespace BedWars.Common.UI
{
    internal class GetSetString<T>
    {
        public Func<T> Get = null;
        public Action<T> Set = null;
        public Func<string, T> func = null;

        public GetSetString(Func<T> Get, Action<T> Set)
        {
            this.Get = Get;
            this.Set = Set;
        }

        public string GetS()
        {
            return Get()?.ToString();
        }

        public void SetS(string v)
        {
            Set(func(v));
        }
    }

    internal class GetSetStringInt : GetSetString<int>
    {
        public GetSetStringInt(Func<int> Get, Action<int> Set) : base(Get, Set)
        {
            func = v =>
            {
                if (int.TryParse(v, out int rv)) return rv;
                return this.Get();
            };
        }
    }

    internal class GetSetStringDouble : GetSetString<double>
    {
        public GetSetStringDouble(Func<double> Get, Action<double> Set) : base(Get, Set)
        {
            func = v =>
            {
                if (double.TryParse(v, out double rv)) return rv;
                return this.Get();
            };
        }
    }

    internal class GetSetStringString : GetSetString<string>
    {
        public GetSetStringString(Func<string> Get, Action<string> Set) : base(Get, Set)
        {
            func = v => v;
        }
    }

    internal class GetSetStringBool : GetSetString<bool>
    {
        public GetSetStringBool(Func<bool> Get, Action<bool> Set) : base(Get, Set)
        {
            func = v =>
            {
                if (v == GetS()) return true;
                return false;
            };
        }
    }
}
