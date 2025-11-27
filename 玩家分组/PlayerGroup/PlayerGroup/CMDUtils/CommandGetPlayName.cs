using CommandHelp;
using System;
using System.Linq;
using Terraria;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 将数字或字符串转为玩家名称, 玩家不存在则报错
    /// </summary>
    public class CommandGetPlayName : CommandValue<string>
    {
        /// <inheritdoc/>
        public override string TypeName => "玩家名或索引";
        /// <inheritdoc/>
        public override string Text => "<玩家名或索引>";
        /// <summary>
        /// 输出
        /// </summary>
        public Action<string> print = null;
        private bool isString = false;

        /// <summary>
        /// 将数字或字符串转为玩家名称, 玩家不存在则报错
        /// </summary>
        /// <param name="isVariable"></param>
        /// <param name="print"></param>
        public CommandGetPlayName(bool isVariable = false, Action<string> print = null) : base(isVariable)
        {
            this.print = print;
        }

        /// <inheritdoc/>
        protected override string ArgConvertThrow(string arg)
        {
            try
            {
                if (isString)
                {
                    arg = CommandString.StringToString(arg);
                    //Player player = Main.player.FirstOrDefault(i => i.name == arg);
                    //if (player == null) throw new Exception($"玩家名不存在[{arg}]");
                    //return player.name;
                    return arg;
                }
                else
                {
                    if (int.TryParse(arg, out int index) == false) throw new Exception($"[{arg}]不是数字");

                    return Main.player[index].name;
                }
            }
            catch (Exception ex)
            {
                Utils.Utils.PrintTry(ex.Message, print);
                throw ex;
            }
        }

        /// <inheritdoc/>
        protected override string GetDefault() => null;

        /// <inheritdoc/>
        public override (string cmdParse, string cmd) ParseFormat(string command)
        {
            (string cmdParse, string cmd) = CommandString.ValParseFormat(false, command);

            if (cmdParse == null) return base.ParseFormat(command);//如果不是字符串格式就使用默认解析

            isString = true;

            return (cmdParse, cmd);
        }
    }
}
