using CommandHelp;
using System;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 将数字或字符串转为分组数据, 分组不存在则报错
    /// </summary>
    public class CommandGetGroup : CommandValue<GroupData>
    {
        /// <inheritdoc/>
        public override string TypeName => "分组名或索引";
        /// <inheritdoc/>
        public override string Text => "<分组名或索引>";
        /// <summary>
        /// 输出
        /// </summary>
        public Action<string> print = null;
        private bool isString = false;

        /// <summary>
        /// 将数字或字符串转为分组数据, 分组不存在则报错
        /// </summary>
        /// <param name="isVariable"></param>
        /// <param name="print"></param>
        public CommandGetGroup(bool isVariable = false, Action<string> print = null) : base(isVariable)
        {
            this.print = print;
        }

        /// <inheritdoc/>
        protected override GroupData ArgConvertThrow(string arg)
        {
            try
            {
                if (isString)
                {
                    GroupData gd = GroupHelp.GetGroup(CommandString.StringToString(arg));
                    return gd ?? throw new Exception($"分组不存在[{arg}]");
                }
                else
                {
                    if (int.TryParse(arg, out int index) == false) throw new Exception($"[{arg}]不是数字");

                    GroupData gd = GroupHelp.GetGroup(index);
                    return gd ?? throw new Exception($"[{index}]不在列表范围内");
                }
            }
            catch (Exception ex)
            {
                Utils.Utils.PrintTry(ex.Message, print);
                throw ex;
            }
        }

        /// <inheritdoc/>
        protected override GroupData GetDefault() => null;

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
