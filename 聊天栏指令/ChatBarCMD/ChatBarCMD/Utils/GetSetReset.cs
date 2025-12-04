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
        private T reset;
        private Func<T, T> func;

        /// <summary/>
        public GetSetReset(T val = default, T reset = default, Func<T, T> func = null)
        {
            this.val = val;
            this.reset = reset;
            this.func = func;

            if (this.func != null) this.val = this.func(this.val);
        }

        /// <summary/>
        public void Reset() => val = reset;
    }

    /// <summary/>
    public class GetSetReset
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
