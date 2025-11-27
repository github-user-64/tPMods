using System.Collections.Generic;

namespace PlayerGroup
{
    /// <summary>
    /// 分组数据
    /// </summary>
    public class GroupData
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name = null;
        /// <summary>
        /// 标签
        /// </summary>
        public Dictionary<string, string> Tag = null;
        /// <summary>
        /// 玩家名称列表
        /// </summary>
        public List<string> PlaysName = null;

        /// <summary>
        /// 获取标签, 标签不存在则返回<paramref name="def"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public string GetTag(string key, string def = null)
        {
            if (key == null) return def;
            if (Tag.ContainsKey(key) == false) return def;

            return Tag[key];
        }

        /// <summary>
        /// 设置标签, 标签不存在则创建
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void SetTag(string key, string val)
        {
            if (key == null) return;

            if (Tag.ContainsKey(key)) Tag[key] = val;
            else Tag.Add(key, val);
        }

        /// <summary>
        /// 标签是否存在
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool HasTag(string tag)
        {
            if (tag == null) return false;
            return Tag.ContainsKey(tag);
        }

        /// <summary>
        /// 删除标签, 没有标签被删除则为<see langword="false"/>
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool DelTag(string tag)
        {
            return Tag.Remove(tag);
        }

        /// <summary>
        /// 创建一个分组数据, 不会添加到分组中
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GroupData NewGroup(string name)
        {
            return new GroupData()
            {
                Name = name,
                Tag = new Dictionary<string, string>(),
                PlaysName = new List<string>(),
            };
        }
    }
}
