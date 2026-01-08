using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using tContentPatch;
using Terraria;

namespace PlayerAccount.Common
{
    internal class OnEnterServering : Mod
    {
        public override void Load()
        {
            ModTool.PatchGame.PNetMessage.OnSyncConnectedPlayerPo += ply =>
            {
                OnEnter(ply, true);
            };
        }

        public override void Loaded()
        {
            for (int i = 0; i < Main.player.Length; ++i)
            {
                if (Main.player[i]?.active != true) continue;

                OnEnter(i, false);
            }
        }

        private void OnEnter(int ply, bool autoLogin)
        {
            bool logined = false;

            if (ServerConfig.data.AutoLogin == true && autoLogin)
            {
                logined = AutoLogin.Login(ply);
            }
            
            if (ServerConfig.data.EnterServerMsg is string msg)
            {
                PrintTo.PrintToPlay(ply, msg, Color.White);
            }

            if (logined == false)
            {
                if (ServerConfig.data.NoLoginNoAction) PrintTo.PrintToPlayAll("登录前不能操作", Color.Red);
                PrintTo.PrintToPlay(ply, "注册账号输入/register [c/aaffaa:密码]", Color.Yellow);
                PrintTo.PrintToPlay(ply, "登录输入/login [c/aaffaa:密码]", Color.Yellow);
            }
        }
    }
}
