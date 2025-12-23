using Microsoft.Xna.Framework;
using tContentPatch;

namespace PlayerAccount.Common
{
    internal class OnEnterServering : Mod
    {
        public override void Load()
        {
            ModTool.PatchGame.PNetMessage.OnSyncConnectedPlayerPo += PatchNetMessage_OnSyncConnectedPlayerPo;
        }

        private void PatchNetMessage_OnSyncConnectedPlayerPo(int ply)
        {
            bool logined = false;

            if (ServerConfig.data.AutoLogin == true)
            {
                logined = AutoLogin.Login(ply);
            }
            
            if (ServerConfig.data.EnterServerMsg is string msg)
            {
                ModTool.ServerHelp.PrintTo.PrintToPlay(ply, msg, Color.White);
            }

            if (logined == false)
            {
                ModTool.ServerHelp.PrintTo.PrintToPlay(ply, "注册账号输入/register [c/aaffaa:密码]", Color.Yellow);
                ModTool.ServerHelp.PrintTo.PrintToPlay(ply, "登录输入/login [c/aaffaa:密码]", Color.Yellow);
            }
        }
    }
}
