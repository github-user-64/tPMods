using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using ModTool.Utils.GetDataEventArgs;
using PlayerAccount.Account;
using tContentPatch;
using Terraria;
using Terraria.ID;

namespace PlayerAccount.Common.ActionPermission
{
    /// <summary>
    /// 没登录不能操作
    /// </summary>
    internal class NoLoginNoAction : PatchPlayer
    {
        public override void Initialize()
        {
            PlayerCanAction.RegisterClassOnTile(CanActionNoMsg);

            PlayerCanAction.OnCanNewProjectile += CanActionNoMsg;
            PlayerCanAction.OnCanNewItem += CanAction;
            PlayerCanAction.OnCanTogglePVP += CanAction;
            PlayerCanAction.OnCanToggleTeam += CanAction;
            PlayerCanAction.OnCanControls += OnCanControls;
            PlayerCanAction.OnCanRequestChestOpen += CanAction;
            PlayerCanAction.OnCanQuickStackChests += CanAction;
            PlayerCanAction.OnCanPlayerBuffs += CanActionNoMsg;
            PlayerCanAction.OnCanBugCatching += CanAction;
            PlayerCanAction.OnCanBugReleasing += CanAction;
            PlayerCanAction.OnCanSpawnBossUseLicenseStartEvent += CanAction;
            PlayerCanAction.OnCanRequestTeleportationByServer += CanAction;
            PlayerCanAction.OnCanTeleportEntity += CanAction;
            PlayerCanAction.OnCanDamageNPC += CanAction;
            PlayerCanAction.OnCanPlayerHurtV2 += CanAction;
        }

        public static bool CanAction(GetDataEventArgs e)
        {
            return CanAction(e.player, true);
        }
        public static bool CanActionNoMsg(GetDataEventArgs e)
        {
            return CanAction(e.player, false);
        }
        public static bool CanAction(Player player, bool msg = true)
        {
            if (ServerConfig.data.NoLoginNoAction == false) return true;

            bool ok = player.GetAccount() != null;
            if (ok == false && msg) SendMsgToPlay.Send(player, "你没有权限操作,请先登录", Color.Red);

            return ok;
        }

        //限制位置
        private static bool RestrictPos(Player player)
        {
            Vector2 pos = new Vector2(Main.spawnTileX, Main.spawnTileY) * 16;
            float d = player.Distance(pos);
            if (d < 16 * 20) return false;

            player.Center = pos;
            player.velocity = Vector2.Zero;

            SendMsgToPlay.Send(player, "你没有权限操作,请先登录", Color.Red);

            NetMessage.SendData(MessageID.TeleportPlayerThroughPortal, -1, -1, null,
                player.whoAmI, player.Center.X, player.Center.Y, 0);

            return true;
        }

        private static bool OnCanControls(ControlsEventArgs e)
        {
            if (CanActionNoMsg(e) == true) return true;

            RestrictPos(e.player);

            return false;
        }

        public override void UpdatePrefix(Player This, int playerI)
        {
            if (Main.dedServ == false) return;
            if (CanAction(This, false) == true) return;

            RestrictPos(This);
        }
    }
}
