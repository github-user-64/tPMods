using PlayerGroup;
using System.Collections.Generic;
using Terraria;

namespace PlayerAccount.Account
{
    /// <summary>
    /// 玩家账号
    /// </summary>
    public static class PlayAccount
    {
        /// <summary>
        /// 玩家的分组标签<see cref="GroupTag.KeyGroupType"/>是否等于<paramref name="groupType"/>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        public static bool GroupTypeEquals(this Player player, string groupType)
        {
            return player.TagEqualsVal(GroupTag.KeyGroupType, groupType);
        }

        /// <summary>
        /// 玩家的分组标签<see cref="GroupTag.KeyGroupType"/>是否有管理员
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool HasAdministrator(this Player player)
        {
            return player.GroupTypeEquals(GroupTag.GroupTypeAdministrator);
        }

        public static int GetGroupTypeLevel(this Player player, int def = -1)
        {

        }
    }
}
