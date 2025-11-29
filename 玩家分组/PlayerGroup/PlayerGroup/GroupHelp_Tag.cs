using System.Collections.Generic;
using Terraria;

namespace PlayerGroup
{
    public static partial class GroupHelp
    {
        /// <summary>
        /// 获取该名称所在的分组, 分组存在标签<paramref name="tag"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<GroupData> GetNameGroupHasTag(string name, string tag)
        {
            if (tag == null) return new List<GroupData>();

            List<GroupData> gs = GetNameGroup(name);

            gs.RemoveAll(i => i.Tag.ContainsKey(tag) == false);

            return gs;
        }

        /// <summary>
        /// 获取玩家名所在的分组, 分组存在标签<paramref name="tag"/>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static List<GroupData> GetPlayGroupHasTag(this Player player, string tag)
        {
            if (player == null) return new List<GroupData>();

            return GetNameGroupHasTag(player.name, tag);
        }

        /// <summary>
        /// 获取该名称所在的分组, 分组标签<paramref name="tag"/>的值等于<paramref name="val"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static List<GroupData> GetNameGroupTagEqualsVal(string name, string tag, string val)
        {
            List<GroupData> gs = GetNameGroupHasTag(name, tag);

            gs.RemoveAll(i => i.GetTag(tag) != val);

            return gs;
        }

        /// <summary>
        /// 获取玩家名所在的分组, 分组标签<paramref name="tag"/>的值等于<paramref name="val"/>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static List<GroupData> GetPlayGroupTagEqualsVal(this Player player, string tag, string val)
        {
            if (player  == null) return new List<GroupData>();

            return GetNameGroupTagEqualsVal(player.name, tag, val);
        }
    }
}
