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
        /// tag是否存在
        /// </summary>
        public static bool HasTag<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).HasKey(tag);
        }

        /// <summary>
        /// 设置val, tag不存在则添加, 成功返回<see langword="true"/>
        /// </summary>
        public static bool SetVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag, string val) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).SetVal(tag, val);
        }

        /// <summary>
        /// 获取val, tag不存在则返回<paramref name="def"/>
        /// </summary>
        public static string GetVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag, string def = null) where T : Entity
        {
            if (obj == null) return def;
            if (entity == null) return def;

            return obj.GetData(entity.whoAmI).GetVal(tag, def);
        }

        /// <summary>
        /// 删除tag, 有tag被删除则为<see langword="true"/>
        /// </summary>
        public static bool DelTag<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).DelKey(tag);
        }

        /// <summary>
        /// tag的val是否相等
        /// </summary>
        public static bool EqualsVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag, string val) where T : Entity
        {
            if (obj == null) return false;
            if (entity == null) return false;

            return obj.GetData(entity.whoAmI).EqualsVal(tag, val);
        }

        /// <summary>
        /// 获取tag列表, 获取失败返回<see langword="null"/>
        /// </summary>
        public static List<string> GetTags<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity) where T : Entity
        {
            if (obj == null) return null;
            if (entity == null) return null;

            return obj.GetData(entity.whoAmI).GetKeys();
        }

        /// <summary>
        /// 获取tag字典, 获取失败返回<see langword="null"/>
        /// </summary>
        public static Dictionary<string, string>
            GetTagDic<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity) where T : Entity
        {
            if (obj == null) return null;
            if (entity == null) return null;

            return obj.GetData(entity.whoAmI);
        }

        /// <summary>
        /// 获取键值对, 不存在则返回<paramref name="def"/>
        /// </summary>
        public static KeyValuePair<string, string>? GetTagVal<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag, KeyValuePair<string, string>? def = null) where T : Entity
        {
            if (obj == null) return def;
            if (entity == null) return def;

            return obj.GetData(entity.whoAmI).GetKeyVal(tag, def);
        }

        /// <summary>
        /// 获取键值对文本, 不存在则返回<paramref name="def"/>
        /// </summary>
        public static string GetTagValString<T>(this AdditionalData<Dictionary<string, string>, T> obj, T entity,
            string tag, string def = "{null}") where T : Entity
        {
            if (obj == null) return def;
            if (entity == null) return def;

            return obj.GetData(entity.whoAmI).GetKeyValString(tag, def);
        }
    }
}
