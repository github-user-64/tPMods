using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal class SGameEnd : IStateAction
    {
        private int time = 0;
        private GameTeamData team = null;

        public SGameEnd(GameRun game) : base(game) { }

        //-进入时:禁用pvp,在在队伍中的所有玩家位置生成烟花
        //-一段时间后进入初始化地图
        public override void OnStart(object arg)
        {
            time = 60 * 10;

            game.ForActivePlayer(i => game.SetTeamPvP(i, 0, false));//禁用pvp

            team = arg as GameTeamData;
            if (team == null) return;

            team.ForPlay(i =>
            {
                //NetMessage.TrySendData(MessageID.AddPlayerBuffPvP, number: i.whoAmI, number2: BuffID.WitchBroom, number3: 1);
                i.mount.SetMount(MountID.WitchBroom, i);
                NetMessage.TrySendData(MessageID.PlayerControls, number: i.whoAmI);
            });

            ToPlayerPlayNetSound.ToPlayAll(SoundID.DD2_WinScene);
            ToPlayerPrint.PrintToPlayAll($"{team.team.name}队胜利", Color.GreenYellow);

            ToPlayerPrint.PrintToPlayAll("游戏结束,即将重新开始游戏", Color.GreenYellow);
        }

        public override void OnEnd()
        {
            team.ForPlay(i =>
            {
                //for (int j = 0; j < i.buffType.Length; j++)
                //{
                //    i.buffType[j] = 0;
                //}

                //NetMessage.TrySendData(MessageID.PlayerBuffs, -1, -1, null, i.whoAmI);

                i.mount.Dismount(i);
                NetMessage.TrySendData(MessageID.PlayerControls, number: i.whoAmI);
            });
        }

        public override void Update(uint gametime)
        {
            if (time < 1)
            {
                game.SetStateUpdate(GameRun.StateMapInit);
                return;
            }

            --time;

            if (team != null && time > 30 && gametime % 60 == 0)
            {
                team.ForPlay(i =>
                {
                    game.NewFireworks(i);
                });
            }
        }

        public override void OnPlayLeftGame(Player player)
        {
            if (Netplay.HasClients == false) game.SetStateUpdate(GameRun.StateMapInit);
        }
    }
}
