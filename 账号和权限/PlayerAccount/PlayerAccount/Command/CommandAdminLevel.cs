using CommandHelp;

namespace PlayerAccount.Command
{
    /// <summary>
    /// 返回管理等级, 默认1
    /// </summary>
    public class CommandAdminLevel : CommandValue<int>
    {
        /// <inheritdoc/>
        public override string Text => "<int>";

        /// <inheritdoc/>
        protected override int ArgConvertThrow(string arg) => int.Parse(arg);

        /// <inheritdoc/>
        protected override int GetDefault() => 1;

        /// <summary/>
        public CommandAdminLevel(bool isVariable = false) : base(isVariable) { }
    }
}
