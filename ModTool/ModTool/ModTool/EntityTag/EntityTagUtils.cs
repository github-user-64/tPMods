using ModTool.AdditionalData;
using ModTool.Utils;
using System.Collections.Generic;
using Terraria;

namespace ModTool.EntityTag
{
    /// <summary>
    /// 实体标签工具
    /// </summary>
    public static class EntityTagUtils
    {
        /// <summary>
        /// key是否存在
        /// </summary>
        public static bool HasKey<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string key) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).HasKey(key);
        }

        /// <summary>
        /// 设置val, key不存在则添加, 成功返回<see langword="true"/>
        /// </summary>
        public static bool SetVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string key, string val) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).SetVal(key, val);
        }

        /// <summary>
        /// 获取val, key不存在则返回<paramref name="def"/>
        /// </summary>
        public static string GetVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string key, string def = null) where T : Entity
        {
            if (obj == null) return def;
            if (entity == null) return def;

            return obj.GetData(entity.whoAmI).GetVal(key, def);
        }

        /// <summary>
        /// 删除key, 有key被删除则为<see langword="true"/>
        /// </summary>
        public static bool DelKey<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string key) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).DelKey(key);
        }

        /// <summary>
        /// 获取key列表, 获取失败返回<see langword="null"/>
        /// </summary>
        public static List<string> GetKeys<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity) where T : Entity
        {
            if (obj == null) return null;
            if (entity == null) return null;

            return obj.GetData(entity.whoAmI).GetKeys();
        }

        /// <summary>
        /// 获取字典, 获取失败返回<see langword="null"/>
        /// </summary>
        public static Dictionary<string, string>
            GetKeyVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity) where T : Entity
        {
            if (obj == null) return null;
            if (entity == null) return null;

            return obj.GetData(entity.whoAmI);
        }
    }
}
