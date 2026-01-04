using ModTool.ServerHelp;
using System.Diagnostics;
using tContentPatch;
using Terraria;

namespace BedWars.Run
{
    internal class ThisIsWhat
    {
        private static readonly IGameControl control = GameRun.instance;

        private static Player GetPlay(int plr)
        {
            if (Main.player?.IndexInRange(plr) != true) return null;
            if (Main.player[plr]?.active != true) return null;
            return Main.player[plr];
        }

        private class a1 : Mod
        {
            public override void Loaded()
            {
                tContentPatch.Utils.Log.Add("起床战争:注册事件");

                PlayerCanAction.RegisterClassOnTile(control.PlayCanActionTile);

                PlayerCanAction.OnCanNewProjectile += control.PlayCanAction;
                PlayerCanAction.OnCanNewItem += control.PlayCanAction;
                PlayerCanAction.OnCanTogglePVP += control.PlayCanAction;
                PlayerCanAction.OnCanToggleTeam += control.PlayCanAction;
                PlayerCanAction.OnCanControls += control.PlayCanAction;
                PlayerCanAction.OnCanRequestChestOpen += control.PlayCanAction;
                PlayerCanAction.OnCanQuickStackChests += control.PlayCanAction;
                PlayerCanAction.OnCanPlayerBuffs += control.PlayCanAction;
                PlayerCanAction.OnCanBugCatching += control.PlayCanAction;
                PlayerCanAction.OnCanBugReleasing += control.PlayCanAction;
                PlayerCanAction.OnCanSpawnBossUseLicenseStartEvent += control.PlayCanAction;
                PlayerCanAction.OnCanRequestTeleportationByServer += control.PlayCanAction;
                PlayerCanAction.OnCanTeleportEntity += control.PlayCanAction;
                PlayerCanAction.OnCanDamageNPC += control.PlayCanAction;
                PlayerCanAction.OnCanPlayerHurtV2 += control.PlayCanAction;

                PlayerAccount.Account.AccountHelp.OnLogined += control.OnPlayLogin;

                tContentPatch.Utils.Log.Add("起床战争:注册完成");

                control.LoadTry(s =>
                {
                    tContentPatch.Utils.Log.Add(s);
                    ContentPatch.PrintTry(s);
                });
            }
        }

        private class a2 : PatchMain
        {
            public override void DoUpdateInWorldPrefix(Stopwatch sw)
            {
                control.Update();
            }
        }

        private class a3 : PatchNetMessage
        {
            public override void SyncConnectedPlayerPrefix(int plr)
            {
                if (GetPlay(plr) is Player player == false) return;

                control.OnPlayJoinGame(player);
            }

            public override void SyncOnePlayerPostfix(int plr, int toWho, int fromWho)
            {
                control.OnPlayLeftGame(plr);
            }
        }
    }
}
