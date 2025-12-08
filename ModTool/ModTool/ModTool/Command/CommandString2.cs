using CommandHelp;

namespace ModTool.Command
{
    /// <summary>
    /// 字符串
    /// </summary>
    public class CommandString2 : CommandValue<string>
    {
        /// <inheritdoc/>
        public override string Text => "<strint>";

        /// <summary>
        /// 字符串
        /// </summary>
        public CommandString2(bool isVariable = false) : base(isVariable) { }

        /// <inheritdoc/>
        protected override string ArgConvertThrow(string arg) => arg;

        /// <inheritdoc/>
        protected override string GetDefault() => null;
    }
}
