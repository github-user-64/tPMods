using System.Collections.Generic;
using System.Linq;

namespace ModTool.Utils
{
    /// <summary>
    /// <see cref="Dictionary{TKey, TValue}"/>工具
    /// </summary>
    public static class DictionaryUtils
    {
        /// <summary>
        /// key是否存在
        /// </summary>
        public static bool HasKey<T, TV>(this Dictionary<T, TV> d, T key)
        {
            if (d == null) return false;
            if (key == null) return false;
            return d.ContainsKey(key);
        }

        /// <summary>
        /// 设置val, key不存在则添加, 成功返回<see langword="true"/>
        /// </summary>
        public static bool SetVal<T, TV>(this Dictionary<T, TV> d, T key, TV val)
        {
            if (d == null) return false;
            if (key == null) return false;

            if (d.ContainsKey(key)) d[key] = val;
            else d.Add(key, val);

            return true;
        }

        /// <summary>
        /// 获取val, key不存在则返回<paramref name="def"/>
        /// </summary>
        public static TV GetVal<T, TV>(this Dictionary<T, TV> d, T key, TV def = default)
        {
            if (d.HasKey(key)) return def;

            return d[key];
        }

        /// <summary>
        /// 删除key, 有key被删除则为<see langword="true"/>
        /// </summary>
        public static bool DelKey<T, TV>(this Dictionary<T, TV> d, T key)
        {
            if (d == null) return false;
            if (key == null) return false;

            return d.Remove(key);
        }

        /// <summary>
        /// 获取key列表, 获取失败返回<see langword="null"/>
        /// </summary>
        public static List<T> GetKeys<T, TV>(this Dictionary<T, TV> d)
        {
            if (d == null) return null;

            return d.Keys.ToList();
        }

        /// <summary>
        /// 获取键值对, 不存在则返回<paramref name="def"/>
        /// </summary>
        public static KeyValuePair<T, TV>? GetKeyVal<T, TV>(this Dictionary<T, TV> d,
            T key, KeyValuePair<T, TV>? def = null)
        {
            if (d.HasKey(key) == false) return def;

            return new KeyValuePair<T, TV>(key, d[key]);
        }

        /// <summary>
        /// 获取键值对文本, 不存在则返回<paramref name="def"/>
        /// </summary>
        public static string GetKeyValString<T, TV>(this Dictionary<T, TV> d, T key, string def = "{null}")
        {
            if (d.HasKey(key) == false) return def;

            return $"{{{key.ToString()},{d[key]?.ToString()}}}";
        }
    }
}
