using System.Linq;
using Terraria;

namespace PlayerGroup
{
    public static partial class GroupHelp
    {
        /// <summary>
        /// 分组是否存在
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static bool HasGroup(string group)
        {
            return GroupSetting.datas.Exists(i => i.Name == group);
        }

        /// <summary>
        /// <paramref name="index"/>是否在<see cref="GroupSetting.datas"/>范围内
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool HasGroup(int index)
        {
            return GroupSetting.datas.IndexInRange(index);
        }

        /// <summary>
        /// 添加分组, 如果已存在则返回已有分组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static GroupData AddGroup(string group)
        {
            GroupData g = GroupSetting.datas.FirstOrDefault(i => i.Name == group);
            if (g == null)
            {
                g = GroupData.NewGroup(group);
                GroupSetting.datas.Add(g);
            }

            return g;
        }

        /// <summary>
        /// 获取分组, 不存在则返回<see langword="null"/>
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static GroupData GetGroup(string group)
        {
            return GroupSetting.datas.FirstOrDefault(i => i.Name == group);
        }

        /// <summary>
        /// 删除分组, 没有分组被删除则为<see langword="false"/>
        /// </summary>
        /// <param name="gd"></param>
        /// <returns></returns>
        public static bool DelGroup(GroupData gd)
        {
            int count = GroupSetting.datas.Count;
            _ = GroupSetting.datas.Remove(gd);
            return GroupSetting.datas.Count < count;
        }

        /// <summary>
        /// 删除分组, 没有分组被删除则为<see langword="false"/>
        /// </summary>
        /// <param name="group"></param>
        public static bool DelGroup(string group)
        {
            return GroupSetting.datas.RemoveAll(i => i.Name == group) > 0;
        }

        /// <summary>
        /// 删除分组, <paramref name="index"/>不在范围内则返回<see langword="false"/>
        /// </summary>
        /// <param name="index"></param>
        public static bool DelGroup(int index)
        {
            if (HasGroup(index) == false) return false;
            GroupSetting.datas.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// 获取第<paramref name="index"/>个分组, 不在范围内返回<see langword="null"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static GroupData GetGroup(int index)
        {
            if (GroupSetting.datas.IndexInRange(index) == false) return null;
            return GroupSetting.datas[index];
        }
    }
}
