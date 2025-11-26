using CommandHelp;

namespace PlayerGroup.CMDUtils
{
    /// <summary>
    /// 可变<see langword="int"/>
    /// </summary>
    public class CommandIntVariable : CommandValue<int>
    {
        /// <inheritdoc/>
        public override string Text => "<int>";

        /// <summary>
        /// 可变<see langword="int"/>
        /// </summary>
        public CommandIntVariable() : base(true) { }

        /// <inheritdoc/>
        protected override int ArgConvertThrow(string arg) => int.Parse(arg);

        /// <inheritdoc/>
        protected override int GetDefault() => 0;
    }
}
