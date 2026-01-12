using ModTool.ServerHelp;
using System.Diagnostics;
using tContentPatch;
using Terraria;
using Terraria.IO;

namespace BedWars.Run
{
    internal class ThisIsWhat
    {
        private static IGameControl control = null;

        private static Player GetPlay(int plr)
        {
            if (Main.player?.IndexInRange(plr) != true) return null;
            return Main.player[plr];
        }

        private class a1 : Mod
        {
            public override void Loaded()
            {
                if (Main.dedServ == false)
                {
                    Print("不是服务端,不加载游戏");
                    return;
                }

                control = GameRun.instance;

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

                PlayerJoinLeft.OnSyncConnectedPlayerPr += playerJoinState.OnSyncConnectedPlayerPrefix;
                PlayerJoinLeft.OnPlayerClientDisconnected += playerJoinState.OnPlayerLeft;

                PlayerAccount.Account.AccountHelp.OnLogined += playerJoinState.OnPlayLogined;
                PlayerAccount.Account.AccountHelp.OnJoinGamePo += playerJoinState.OnPlayJoinGameTryAutoLoginPo;

                WorldFile.OnWorldLoad += () =>
                {
                    Print("世界加载完成,开始加载");
                    LoadGame();
                    InitGame();
                    tContentPatch.Utils.Log.SaveTry();
                };

                tContentPatch.Utils.Log.Add("起床战争:注册完成");

                Print("加载");
                LoadGame();

                if (WorldGen.loadSuccess == false || WorldGen.loadFailed == true)
                {
                    return;
                }
                //世界文件已加载

                Print("世界已加载,开始初始化");
                InitGame();

                foreach (Player i in Main.player)//防止已经有玩家加入
                {
                    if (i?.active != true) continue;
                    if (i.whoAmI == Main.player.Length - 1) continue;//最后一个是服务器

                    control.OnPlayJoinGame(i);
                }
            }

            private static void LoadGame()
            {
                control?.LoadTry(Print);
            }

            private static void InitGame()
            {
                control?.InitTry(Print);
            }

            private static void Print(string s)
            {
                s = $"起床战争:{s}";
                tContentPatch.Utils.Log.Add(s);
                ContentPatch.PrintTry(s);
            }
        }

        private static class playerJoinState
        {
            public static void OnSyncConnectedPlayerPrefix(int plr)
            {
                if (GetPlay(plr) is Player player == false) return;

                control.OnPlayJoinGame(player);
            }

            public static void OnPlayLogined(Player player)
            {
                control.OnPlayJoinGameTryAutoLoginPo(player);
            }

            public static void OnPlayJoinGameTryAutoLoginPo(int plr)
            {
                if (GetPlay(plr) is Player player == false) return;

                control.OnPlayJoinGameTryAutoLoginPo(player);
            }

            public static void OnPlayerLeft(Player player)
            {
                control.OnPlayLeftGame(player);
            }
        }

        private class gameUpdate : PatchMain
        {
            public override void DoUpdateInWorldPrefix(Stopwatch sw)
            {
                if (control == null) return;
                control.Update(Main.GameUpdateCount);
            }
        }
    }
}
