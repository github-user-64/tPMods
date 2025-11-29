using System.Collections.Generic;
using Terraria;

namespace PlayerGroup
{
    public static partial class GroupHelp
    {
        /// <summary>
        /// 获取该名称的每个分组中标签<paramref name="tag"/>的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<string> GetTagVals(string name, string tag)
        {
            if (tag == null) return new List<string>();

            List<GroupData> gs = GetNameGroup(name);
            List<string> vals = new List<string>();

            foreach (GroupData g in gs)
            {
                if (g.Tag.ContainsKey(tag) == false) continue;

                vals.Add(g.Tag[tag]);
            }

            return vals;
        }

        /// <summary>
        /// 该名称的每个分组中是否有标签<paramref name="tag"/>的值等于<paramref name="val"/>
        /// <para><paramref name="tag"/>不在任何一个分组中则返回<see langword="false"/></para>
        /// <para>没有<paramref name="tag"/>的值和<paramref name="val"/>相同返回<see langword="false"/></para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TagEqualsVal(string name, string tag, string val)
        {
            List<string> vals = GetTagVals(name, tag);

            return vals.Contains(val);
        }

        /// <summary>
        /// 玩家名的每个分组中是否有标签<paramref name="tag"/>的值等于<paramref name="val"/>
        /// <para><paramref name="tag"/>不在任何一个分组中则返回<see langword="false"/></para>
        /// <para>没有<paramref name="tag"/>的值和<paramref name="val"/>相同返回<see langword="false"/></para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TagEqualsVal(this Player player, string tag, string val)
        {
            if (player  == null) return false;
            return TagEqualsVal(player.name, tag, val);
        }
    }
}
