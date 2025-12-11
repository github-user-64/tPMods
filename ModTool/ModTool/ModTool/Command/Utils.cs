using CommandHelp;

namespace ModTool.Command
{
    /// <summary>
    /// 工具
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 向<paramref name="co"/>的子命令中添加<paramref name="add"/>后返回<paramref name="add"/>
        /// </summary>
        public static CommandObject AddRAdd(this CommandObject co, CommandObject add)
        {
            co.SubCommand.Add(add);
            return add;
        }
    }
}
