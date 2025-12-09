using tContentPatch;

namespace PlayerAccount.Common
{
    internal class LoginMaxKick : Mod
    {
        private class ErrorCount : ModTool.AdditionalData.PlayerAdditionalData<int>
        {
            public override int ConverterThrow(int index) => 0;
        }

        public override void Load()
        {
            ErrorCount ec = new ErrorCount();

            Account.AccountHelp.OnLoginPasswdError += ply =>
            {
                int count = ec.GetData(ply.whoAmI, 0) + 1;

                ec.SetData(ply.whoAmI, count);

                if (count < 5) return;

                ModTool.ServerHelp.KickPlay.Kick(ply.whoAmI, "密码多次错误");
            };
        }
    }
}
