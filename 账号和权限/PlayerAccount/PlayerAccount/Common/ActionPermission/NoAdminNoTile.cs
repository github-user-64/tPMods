using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using ModTool.Utils;
using PlayerAccount.Account;
using tContentPatch;

namespace PlayerAccount.Common.ActionPermission
{
    /// <summary>
    /// 非管理不能修改方块
    /// </summary>
    internal class NoAdminNoTile : Mod
    {
        public override void Load()
        {
            PlayerCanAction.RegisterClassOnTile(e =>
            {
                if (ServerConfig.data.NoAdminNoTile == false) return true;

                bool ok = e.player.GetAccount().HasKey(AccountTag.AdminLevel);
                if (ok == false) SendMsgToPlay.Send(e.player, "你没有权限修改方块", Color.Red);

                return ok;
            });
        }
    }
}
