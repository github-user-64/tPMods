using Microsoft.Xna.Framework;
using ModTool.ServerHelp;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal class SGameEnd : IStateAction
    {
        private int time = 0;

        public SGameEnd(GameRun game) : base(game) { }

        //-进入时:禁用pvp,在在队伍中的所有玩家位置生成烟花
        //-有玩家死亡时:
        //-一段时间后进入初始化地图
        public override void OnStart(object arg)
        {
            game.ForActivePlayer(i => game.SetTeamPvP(i, 0, false));//禁用pvp

            if (arg is GameTeamData team == false) return;

            ToPlayerPrint.PrintToPlayAll($"{team.team.name}队胜利", Color.GreenYellow);

            a1(team);

            time = 60 * 6;

            ToPlayerPrint.PrintToPlayAll("游戏结束,即将重新开始游戏", Color.GreenYellow);

            //
            string text = "";
            team.ForPlay(i => text += i.name + ",");

            ToPlayerPrint.PrintToPlayAll(text, Color.AliceBlue);
        }

        public override void Update(uint gametime)
        {
            if (time > 0)
            {
                --time;
                return;
            }

            game.SetStateUpdate(GameRun.StateMapInit);
        }

        private void a1(GameTeamData team)
        {
            int[] ids = new int[]
            {
                ProjectileID.RocketFireworksBoxRed,
                ProjectileID.RocketFireworksBoxGreen,
                ProjectileID.RocketFireworksBoxBlue,
                ProjectileID.RocketFireworksBoxYellow,
            };

            team.ForPlay(i =>
            {
                Vector2 pos = i.Center;
                pos.Y -= 40;

                int id = ids[ModTool.Utils.Utils.GetRand(0, ids.Length)];

                Projectile.NewProjectile(null, pos, Vector2.UnitY * -5, id, 0, 0);
            });
        }
    }
}
