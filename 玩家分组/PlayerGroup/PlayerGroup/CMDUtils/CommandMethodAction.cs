using CommandHelp;
using System;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 单纯用来执行指令的
    /// </summary>
    public class CommandMethodAction : CommandMethod
    {
        /// <summary>
        /// 单纯用来执行指令的
        /// </summary>
        /// <param name="text"></param>
        /// <param name="action"></param>
        public CommandMethodAction(string text, Action action) : base(text)
        {
            if (action != null) Runing += _ => action.Invoke();
        }
    }
}
