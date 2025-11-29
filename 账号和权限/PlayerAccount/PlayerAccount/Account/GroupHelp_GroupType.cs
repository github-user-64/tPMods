using PlayerGroup;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 分组类型工具
    /// </summary>
    public static class GroupHelp_GroupType
    {
        /// <summary>
        /// 获取玩家名所在的分组的标签<see cref="GroupTag.KeyGroupTypeLevel"/>, 标签<see cref="GroupTag.KeyGroupType"/>等于<paramref name="groupType"/>
        /// <para>标签<see cref="GroupTag.KeyGroupType"/>不等于<paramref name="groupType"/>返回<see langword="null"/></para>
        /// <para>标签<see cref="GroupTag.KeyGroupTypeLevel"/>值无法转化为<see langword="int"/>返回<see langword="null"/></para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        public static int? GetPlayGroupTypeLevel(this Player player, string groupType)
        {
            List<GroupData> gs = player.GetPlayGroupTagEqualsVal(GroupTag.KeyGroupType, groupType);//获取分组类型

            foreach (GroupData g in gs)//获取分组类型等级
            {
                if (int.TryParse(g.GetTag(GroupTag.KeyGroupTypeLevel), out int v)) return v;//返回转化成功的
            }

            return null;
        }

        /// <summary>
        /// 获取玩家名的管理员等级
        /// <para>标签<see cref="GroupTag.KeyGroupType"/>不等于<see cref="GroupTag.GroupTypeAdministrator"/>返回<see langword="null"/></para>
        /// <para>标签<see cref="GroupTag.KeyGroupTypeLevel"/>值无法转化为<see langword="int"/>返回<see langword="null"/></para>
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static int? GetPlayAdministratorLevel(this Player player)
        {
            return player.GetPlayGroupTypeLevel(GroupTag.GroupTypeAdministrator);
        }
    }
}
