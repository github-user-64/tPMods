using CommandHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace ModTool.Command
{
    /// <summary>
    /// 将完整玩家名 或 匹配玩家名 或 索引 转为在线玩家, 玩家不存在则报错
    /// </summary>
    public class CommandGetPlayer : CommandValue<Player>
    {
        /// <inheritdoc/>
        public override string TipText { get; set; } = "\"完整玩家名\"或匹配玩家名或索引";
        /// <inheritdoc/>
        public override string TypeName => "\"完整玩家名\"或匹配玩家名或索引";
        /// <inheritdoc/>
        public override string Text => "<\"完整玩家名\"或匹配玩家名或索引>";

        /// <summary>
        /// 将完整玩家名 或 匹配玩家名 或 索引 转为在线玩家, 玩家不存在则报错
        /// </summary>
        public CommandGetPlayer(bool isVariable = false) : base(isVariable) { }

        /// <inheritdoc/>
        protected override Player ArgConvertThrow(string arg)
        {
            bool isString = false;

            try
            {
                arg = CommandString.StringToString(arg);
                isString = true;
            }
            catch { }

            Player player = null;

            if (isString)//是完整字符串
            {
                player = Main.player.FirstOrDefault(i => i.name == arg);
                if (player == null) throw new Exception($"玩家名不存在[{arg}]");
            }
            else
            if (int.TryParse(arg, out int index))//是数字
            {
                if (Main.player.IndexInRange(index) != true) throw new Exception($"[{index}]超出范围");
                player = Main.player[index];
                if (player == null) throw new Exception($"玩家数据异常");
            }
            else
            {
                //匹配名称
                List<Player> list = MatchPlayerName(arg);
                if (list.Count < 1) throw new Exception($"没有匹配的玩家名:{arg}");

                if (list.Count > 1)
                {
                    string exMsg = "有多个匹配玩家名:";
                    foreach (var i in list) exMsg += $"\n索引:{i.whoAmI},名称:{i.name}";
                    throw new Exception(exMsg);
                }

                player = list[0];
            }

            if (player.active == false) throw new Exception("玩家不在线");

            return player;
        }

        /// <inheritdoc/>
        protected override Player GetDefault() => null;

        /// <inheritdoc/>
        public override (string cmdParse, string cmd) ParseFormat(string command)
        {
            (string cmdParse, string cmd) = CommandString.ValParseFormat(false, command);

            if (cmdParse == null) return base.ParseFormat(command);//如果不是字符串格式就使用默认解析

            return (cmdParse, cmd);
        }

        /// <summary>
        /// 返回名字中包含<paramref name="match"/>的在线玩家列表
        /// </summary>
        public static List<Player> MatchPlayerName(string match)
        {
            List<Player> list = new List<Player>();

            if (match == null || match.Length < 1) return list;
            if (Main.player == null) return list;

            for (int i = 0; i < Main.player.Length; ++i)
            {
                Player player = Main.player[i];

                if (player == null) continue;
                if (player.active == false) continue;

                string name = Main.player[i].name;
                if (name == null)
                {
                    list.Add(Main.player[i]);
                    continue;
                }

                if (name.Contains(match)) list.Add(player);
            }

            return list;
        }
    }
}
