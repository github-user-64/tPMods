using CommandHelp;
using System;

namespace ChatBarCMD.Utils
{
    /// <summary>
    /// Help, Reset, Add
    /// </summary>
    internal class CommandHRA<T> : CommandMethod
    {
        /// <summary>
        /// Help, Reset, Add
        /// </summary>
        public CommandHRA(string texe, GetSetReset<T> gsr, string tip = null, Action<string> print = null, params CommandObject[] add) : base(texe, 1)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, tip, print));
            SubCommand.Add(new CommandVariable("reset"));
            foreach (CommandObject co in add) SubCommand.Add(co);

            Runing += args =>
            {
                if (args[0] is CommandVariable cv)
                {
                    if (cv.TextEquals) gsr.Reset();
                    else print?.Invoke($"[{gsr.val}]");
                }
                else
                {
                    gsr.val = (T)args[0];
                }
            };
        }
    }
}
