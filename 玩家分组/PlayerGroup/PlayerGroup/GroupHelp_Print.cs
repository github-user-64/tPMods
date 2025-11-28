using System;
using System.Collections.Generic;

namespace PlayerGroup
{
    public static partial class GroupHelp
    {
        /// <summary>
        /// 输出全部分组
        /// </summary>
        /// <param name="print"></param>
        public static void PrintGroup(Action<string> print = null)
        {
            string s = $"分组数量[{GroupSetting.datas.Count}]";

            for (int i = 0; i < GroupSetting.datas.Count; ++i)
            {
                GroupData gd = GroupSetting.datas[i];

                s += $"\n{i}:[{gd.Name}],标签:";
                foreach (KeyValuePair<string, string> tag in gd.Tag) s += $"[{tag.Key}={tag.Value}]";
            }

            Utils.Utils.PrintTry(s, print);
        }

        /// <summary>
        /// 输出分组数据
        /// </summary>
        /// <param name="groupData"></param>
        /// <param name="print"></param>
        public static void PrintGroup(GroupData groupData, Action<string> print = null)
        {
            if (groupData == null) return;

            string s = $"名称:[{groupData.Name}],索引:[{GroupSetting.datas.IndexOf(groupData)}],标签:";
            foreach (KeyValuePair<string, string> tag in groupData.Tag) s += $"[{tag.Key}={tag.Value}]";

            s += $"\n玩家名数量:[{groupData.PlaysName.Count}]";

            for (int i = 0; i < groupData.PlaysName.Count; ++i)
            {
                s += $"\n{i}:[{groupData.PlaysName[i]}]";
            }

            Utils.Utils.PrintTry(s, print);
        }

        /// <summary>
        /// 输出分组数据
        /// </summary>
        /// <param name="group"></param>
        /// <param name="print"></param>
        public static void PrintGroup(string group, Action<string> print = null)
        {
            if (GetGroup(group) is GroupData gd) PrintGroup(gd, print);
            else Utils.Utils.PrintTry($"[{group}]不存在", print);
        }

        /// <summary>
        /// 输出分组数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="print"></param>
        public static void PrintGroup(int index, Action<string> print = null)
        {
            if (GetGroup(index) is GroupData gd) PrintGroup(gd, print);
            else Utils.Utils.PrintTry($"[{index}]不在索引范围内", print);
        }

        /// <summary>
        /// 输出名称的分组
        /// </summary>
        /// <param name="name"></param>
        /// <param name="print"></param>
        public static void PrintNameHasGroup(string name, Action<string> print = null)
        {
            List<GroupData> gds = GetNameGroup(name);
            if (gds.Count < 1)
            {
                print($"[{name}]不在任何分组中");
                return;
            }

            string s = $"[{name}]在[{gds.Count}]个分组中:";
            for (int i = 0; i < gds.Count; ++i) s = $"{s}\n{i}:[{gds[i].Name}]";

            Utils.Utils.PrintTry(s, print);
        }
    }
}
