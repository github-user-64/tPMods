using CommandHelp;

namespace ModTool.test
{
    /// <summary/>
    public class CommandString : CommandValue<string>
    {
        /// <inheritdoc/>
        public override string Text => "<strint>";

        /// <summary/>
        public CommandString(bool isVariable = false) : base(isVariable) { }

        /// <inheritdoc/>
        protected override string ArgConvertThrow(string arg)
        {
            return arg;
        }

        /// <inheritdoc/>
        protected override string GetDefault() => null;
    }
}
