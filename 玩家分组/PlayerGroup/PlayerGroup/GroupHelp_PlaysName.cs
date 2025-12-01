using System.Collections.Generic;
using Terraria;

namespace PlayerGroup
{
    public static partial class GroupHelp
    {
        /// <summary>
        /// 分组中是否存在该名称
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasName(this GroupData gd, string name)
        {
            return gd.PlaysName.Contains(name);
        }

        /// <summary>
        /// 添加名称, 已存在不会添加, 添加成功返回<see langword="true"/>
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="name"></param>
        public static bool AddName(this GroupData gd, string name)
        {
            if (gd.HasName(name)) return false;

            gd.PlaysName.Add(name);
            return true;
        }

        /// <summary>
        /// 添加第<paramref name="index"/>个玩家的名称, 已存在不会添加, 添加成功返回<see langword="true"/>
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool AddPlay(this GroupData gd, int index)
        {
            if (Main.player?.IndexInRange(index) != true) return false;

            return gd.AddName(Main.player[index].name);
        }

        /// <summary>
        /// 删除名称, 没有名称被删除则为<see langword="false"/>
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool DelName(this GroupData gd, string name)
        {
            return gd.PlaysName.RemoveAll(i => i == name) > 0;
        }

        /// <summary>
        /// 删除第<paramref name="index"/>个玩家的名称, 没有名称被删除则为<see langword="false"/>
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool DelPlay(this GroupData gd, int index)
        {
            if (Main.player?.IndexInRange(index) != true) return false;

            return gd.DelName(Main.player[index].name);
        }

        /// <summary>
        /// 获取该名字所在的分组, 名字不在任何分组中返回空列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<GroupData> GetNameGroup(string name)
        {
            List<GroupData> list = new List<GroupData>();

            foreach (GroupData group in GroupSetting.datas)
            {
                if (group.PlaysName.Contains(name)) list.Add(group);
            }

            return list;
        }

        /// <summary>
        /// 获取玩家名所在的分组, 名字不在任何分组中返回空列表
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static List<GroupData> GetPlayGroup(this Player player)
        {
            if (player == null) return new List<GroupData>();

            return GetNameGroup(player.name);
        }

        /// <summary>
        /// 该名称是否在指定分组
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public static bool NameHasGroup(string name, string group)
        {
            List<GroupData> gs = GetNameGroup(name);

            return gs.Exists(i => i.Name == group);
        }

        /// <summary>
        /// 玩家名是否在指定分组
        /// </summary>
        /// <param name="player"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public static bool PlayHasGroup(this Player player, string group)
        {
            if (player == null) return false;

            return NameHasGroup(player.name, group);
        }
    }
}
