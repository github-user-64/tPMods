using PlayerTag.Utils;
using System.Collections.Generic;

namespace PlayerTag
{
    /// <summary>
    /// 标签数据
    /// </summary>
    public class TagData
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name = null;
        /// <summary>
        /// 标签
        /// </summary>
        public Dictionary<string, string> Tag = null;
        /// <summary>
        /// 临时标签, 不会被保存
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Dictionary<string, string> TagTemp = null;
        /// <summary>
        /// 为<see langword="false"/>意味着不在列表里, 如果在列表里那么会被删除
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public bool active = false;

        /// <summary>
        /// 标签是否存在
        /// </summary>
        public bool HasTag(string tag) => Tag.DictionaryHasKey(tag);
        /// <summary>
        /// 标签是否存在
        /// </summary>
        public bool HasTagTemp(string tag) => TagTemp.DictionaryHasKey(tag);

        /// <summary>
        /// 设置标签, 标签不存在则创建, 成功返回<see langword="true"/>
        /// </summary>
        public bool SetTag(string key, string val) => Tag.DictionarySetVal(key, val);
        /// <summary>
        /// 设置标签, 标签不存在则创建, 成功返回<see langword="true"/>
        /// </summary>
        public bool SetTagTemp(string key, string val) => TagTemp.DictionarySetVal(key, val);

        /// <summary>
        /// 获取标签值, 标签不存在则返回<paramref name="def"/>
        /// </summary>
        public string GetTag(string key, string def = null) => Tag.DictionaryGetVal(key, def);
        /// <summary>
        /// 获取标签值, 标签不存在则返回<paramref name="def"/>
        /// </summary>
        public string GetTagTemp(string key, string def = null) => TagTemp.DictionaryGetVal(key, def);

        /// <summary>
        /// 删除标签, 有标签被删除则为<see langword="true"/>
        /// </summary>
        public bool DelTag(string tag) => Tag.DictionaryDel(tag);
        /// <summary>
        /// 删除标签, 有标签被删除则为<see langword="true"/>
        /// </summary>
        public bool DelTagTemp(string tag) => TagTemp.DictionaryDel(tag);

        /// <summary>
        /// 创建一个<see cref="TagData"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TagData NewGroup(string name)
        {
            return new TagData()
            {
                Name = name,
                Tag = new Dictionary<string, string>(),
                TagTemp = new Dictionary<string, string>(),
                active = false,
            };
        }
    }
}
