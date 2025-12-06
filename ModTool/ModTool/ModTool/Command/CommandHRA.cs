using CommandHelp;
using ModTool.Utils;
using System;

namespace ModTool.Command
{
    /// <summary>
    /// Help, Reset, AddType
    /// </summary>
    public class CommandHRA<T> : CommandMethod
    {
        /// <summary>
        /// Help, Reset, AddType
        /// </summary>
        public CommandHRA(string texe, GetSetReset<T> gsr, string tip = null, Action<string> print = null, params CommandObject[] add) : base(texe, 1)
        {
            SubCommand.Add(new CommandPrintList(SubCommand, tip, print));
            SubCommand.Add(new CommandVariable("reset"));
            SubCommand.AddRange(add);

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
