using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using PlayerAccount.Account;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal class SReadyGame : IStateAction
    {
        private readonly List<Player> ReadyPlay = new List<Player>();
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
        public override void OnStart(object arg)
        {
            ToPlayerPrint.PrintToPlayAll("准备游戏", Color.YellowGreen);
            ToPlayerCombatText.ToPlayAllOff("准备游戏", 0, -100, Color.White);

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

            game.ForActivePlayer(i =>
            {
                game.ClearInventory(i);//清空背包

                TryAddPlayToReady(i);//添加登录玩家到列表
            });
        }

        public override void OnEnd()
        {
            ReadyPlay.Clear();
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
            if (ReadyPlay.Remove(player) == false) return;

            int v = game.DataInfo.startGameMinPlay - ReadyPlay.Count;
            if (v > 0)
            {
                ToPlayerPrint.PrintToPlayAll($"{player.name}离开游戏,还需{v}名玩家", Color.Green);
            }
        }

        private void TryAddPlayToReady(Player player)
        {
            if (player == null) return;
            if (player.active == false) return;
            if (ReadyPlay.Contains(player)) return;

            if (ReadyPlay.Count >= MaxPlayCount)
            {
                ToPlayerPrint.PrintToPlay(player.whoAmI, $"已达最大玩家数量{MaxPlayCount}名", Color.Red);
                return;
            }

            Dictionary<string, string> acc = player.GetAccount();
            if (acc == null)
            {
                ToPlayerPrint.PrintToPlay(player.whoAmI, "你还未登录,不能加入游戏", Color.Red);
                ToPlayerPrint.PrintToPlay(player.whoAmI, "注册:[c/aaffaa:/register]登录[c/aaffaa:/login]", Color.White);
                return;
            }

            ReadyPlay.Add(player);

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
                ToPlayerPrint.PrintToPlayAll($"{player.name}加入游戏,还需{v}名玩家", Color.YellowGreen);
            }
            else
            {
                ToPlayerPrint.PrintToPlayAll($"{player.name}加入游戏,准备开始游戏", Color.YellowGreen);
            }
        }

        public override void Update(uint gametime)
        {
            ReadyPlay.RemoveAll(i => Utils.PlayerHasServer(i) == false);

            if (gametime % 60 != 0) return;

            int v = game.DataInfo.startGameMinPlay - ReadyPlay.Count;
            if (v > 0 || ReadyPlay.Count < 1)//最小玩家数量不够时等待
            {
                StartTime = 30;
                return;
            }

            int time = 30;
            //float what = 6;
            float what = 2;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (ReadyPlay.Count >= what) time = 5;
            else if (ReadyPlay.Count > what / 2f) time = 10;
            else if (ReadyPlay.Count > what / 3f) time = 20;

            if (StartTime > time) StartTime = time;

            //进入倒计时

            if (StartTime < 1)//时间到进入游戏中
            {
                game.SetStateUpdate(GameRun.StateGameing, ReadyPlay.ToList());
                return;
            }

            if (StartTime < 6)
            {
                ToPlayerPrint.PrintToPlayAll($"开始游戏:{StartTime}", Color.Pink);
                ToPlayerCombatText.ToPlayAllOff(StartTime.ToString(), 0, -30, Color.DeepPink);

                ToPlayerPlayNetSound.ToPlayAll(SoundID.Item149);
            }
            else if (StartTime % 10 == 0)
            {
                ToPlayerPrint.PrintToPlayAll($"开始游戏:{StartTime}", Color.YellowGreen);

                ToPlayerPlayNetSound.ToPlayAll(SoundID.Item149);
            }

            --StartTime;
        }
    }
}
