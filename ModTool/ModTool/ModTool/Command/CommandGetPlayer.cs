using CommandHelp;
using System;
using System.Linq;
using Terraria;

namespace ModTool.Command
{
    /// <summary>
    /// 将数字或字符串转为在线玩家, 玩家不存在则报错
    /// </summary>
    public class CommandGetPlayer : CommandValue<Player>
    {
        /// <inheritdoc/>
        public override string TipText { get; set; } = "\"玩家名\"或索引";
        /// <inheritdoc/>
        public override string TypeName => "\"玩家名\"或索引";
        /// <inheritdoc/>
        public override string Text => "<\"玩家名\"或索引>";

        /// <summary/>
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

            if (isString)
            {
                player = Main.player.FirstOrDefault(i => i.name == arg);
                if (player == null) throw new Exception($"玩家名不存在[{arg}]");
            }
            else
            {
                if (int.TryParse(arg, out int index) == false) throw new Exception($"[{arg}]不是数字");
                if (Main.player.IndexInRange(index) != true) throw new Exception($"[{index}]超出范围");
                player = Main.player[index];
                if (player == null) throw new Exception($"玩家数据异常");
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
    }
}
