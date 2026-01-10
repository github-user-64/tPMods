using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using PlayerAccount.Account;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace BedWars.Run
{
    internal class SReadyGame : IStateAction
    {
        private List<int> ReadyPlay = new List<int>();
        private int MaxPlayCount = 0;
        private int StartTime = 0;

        public SReadyGame(GameRun game) : base(game) { }

        //准备游戏:
        //-进入时:清空掉落物,清空玩家背包,添加登录玩家到列表
        //-玩家登录时:添加登录玩家到列表
        //-添加登录玩家到列表:
        //--处于幽灵状态:正常状态,重生到进入游戏位置
        //-不能交互图格
        //-最小玩家数量不够时等待,够时进入计时
        //-时间到进入游戏中
        public override void OnStart()
        {
            PrintTo.PrintToPlayAll("准备游戏", Color.YellowGreen);
            CombatTextTo.ToPlayAllOff("准备游戏", 0, -100, Color.White);

            ReadyPlay.Clear();
            StartTime = 0;
            MaxPlayCount = 0;

            game.DataTeams.ForEach(i => MaxPlayCount += i.maxPlay);

            //清空掉落物
            for (int i = 0; i < Main.item.Length; i++)
            {
                Item item = Main.item[i];
                if (item?.active != true) continue;
                if (item.type == ItemID.None) continue;
                if (item.stack < 1) continue;

                item.SetDefaults(ItemID.None);
                item.active = false;

                NetMessage.TrySendData(MessageID.SyncItem, number: i);
            }

            game.ForPlay(i =>
            {
                game.ClearInventory(i);//清空背包

                TryAddPlayToReady(i);//添加登录玩家到列表
            });
        }

        public override void OnPlayLogin(Player player)
        {
            TryAddPlayToReady(player);//玩家登录后
        }

        public override void OnPlayJoinGameTryAutoLoginPo(Player player)
        {
            TryAddPlayToReady(player);//加入游戏尝试自动登录后
        }

        public override void OnPlayLeftGame(Player player)
        {
            ReadyPlay.Remove(player.whoAmI);

            int v = game.DataInfo.startGameMinPlay - ReadyPlay.Count;
            if (v > 0)
            {
                PrintTo.PrintToPlayAll($"{player.name}离开游戏,还需{v}名玩家", Color.Green);
            }
        }

        private void TryAddPlayToReady(Player player)
        {
            if (player == null) return;
            if (player.active == false) return;
            if (ReadyPlay.Contains(player.whoAmI)) return;

            if (ReadyPlay.Count >= MaxPlayCount)
            {
                PrintTo.PrintToPlay(player.whoAmI, $"已达最大玩家数量{MaxPlayCount}名", Color.Red);
                return;
            }

            Dictionary<string, string> acc = player.GetAccount();
            if (acc == null)
            {
                PrintTo.PrintToPlay(player.whoAmI, "你还未登录,不能加入游戏", Color.Red);
                PrintTo.PrintToPlay(player.whoAmI, "注册:[c/aaffaa:/register]登录[c/aaffaa:/login]", Color.White);
                return;
            }

            ReadyPlay.Add(player.whoAmI);

            //设为正常状态
            if (player.ghost)
            {
                player.ghost = false;
                NetMessage.TrySendData(MessageID.PlayerControls);
            }

            game.SpawnToMapSpaw(player);//重生到进入游戏位置

            int v = game.DataInfo.startGameMinPlay - ReadyPlay.Count;
            if (v > 0)
            {
                PrintTo.PrintToPlayAll($"{player.name}加入游戏,还需{v}名玩家", Color.YellowGreen);
            }
            else
            {
                PrintTo.PrintToPlayAll($"{player.name}加入游戏,准备开始游戏", Color.YellowGreen);
            }
        }

        public override void Update(uint gametime)
        {
            if (gametime % 60 != 0) return;

            int v = game.DataInfo.startGameMinPlay - ReadyPlay.Count;
            if (v > 0 || ReadyPlay.Count < 1)
            {
                StartTime = 30;
                return;
            }

            int time = 30;
            if (ReadyPlay.Count >= MaxPlayCount) time = 5;
            else if (ReadyPlay.Count > MaxPlayCount / 2f) time = 10;
            else if (ReadyPlay.Count > MaxPlayCount / 3f) time = 20;

            if (StartTime > time) StartTime = time;

            //

            if (StartTime < 1)
            {
                //
                return;
            }

            if (StartTime < 6)
            {
                PrintTo.PrintToPlayAll($"开始游戏:{StartTime}", Color.Pink);
                CombatTextTo.ToPlayAllOff(StartTime.ToString(), 0, -50, Color.Pink);
            }
            else
            {
                if (StartTime % 10 == 0) PrintTo.PrintToPlayAll($"开始游戏:{StartTime}", Color.YellowGreen);
            }

            --StartTime;
        }
    }
}
