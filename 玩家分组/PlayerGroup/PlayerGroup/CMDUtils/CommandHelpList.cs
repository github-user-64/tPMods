using CommandHelp;
using System;
using System.Collections.Generic;
using tContentPatch;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 指令帮助列表
    /// </summary>
    public class CommandHelpList : CommandMethod
    {
        /// <inheritdoc/>
        public override string Text => "?";

        /// <summary>
        /// 显示指令列表
        /// </summary>
        /// <param name="cos"></param>
        /// <param name="tip"></param>
        /// <param name="print"></param>
        public CommandHelpList(List<CommandObject> cos, string tip = null, Action<string> print = null)
        {
            Runing += args =>
            {
                string s = null;
                foreach (CommandObject i in cos)
                {
                    if (i == this) continue;

                    if (s == null)
                    {
                        s = $"{i.Text}";
                    }
                    else
                    {
                        s += $", {i.Text}";
                    }
                }

                if (s == null) s = "no cmd";
                if (tip != null) s = $"{s}//{tip}";
                Utils.Utils.PrintTry(s, print);
            };
        }

        /// <inheritdoc/>
        public override object OnRuning(ref int index, List<CommandObject> commandList, object[] args)
        {
            base.OnRuning(ref index, commandList, args);
            return this;
        }
    }
}
