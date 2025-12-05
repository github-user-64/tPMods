using System;

namespace ChatBarCMD.Utils
{
    /// <summary/>
    public class GetSetReset<T>
    {
        /// <summary/>
        public Action<T> OnValUpdate = null;
        private T _val;
        /// <summary/>
        public T val
        {
            get => _val;
            set
            {
                if (_val == null && value == null) return;
                if (_val?.Equals(value) == true) return;
                _val = func == null ? value : func(value);
                OnValUpdate?.Invoke(_val);
            }
        }
        private readonly T reset = default;
        private readonly Func<T, T> func = null;

        /// <summary/>
        public GetSetReset(T val = default, T reset = default, Func<T, T> func = null)
        {
            this.func = func;//放在上面这样设置初始值的时候也能用
            this.val = val;
            this.reset = reset;
        }

        /// <summary/>
        public void Reset() => val = reset;
    }

    /// <summary/>
    public static class GetSetReset
    {
        /// <summary/>
        public static Func<int, int> GetIntFunc(int min = int.MinValue, int max = int.MaxValue)
        {
            return v =>
            {
                if (v < min) return min;
                if (v > max) return max;
                return v;
            };
        }
    }
}
