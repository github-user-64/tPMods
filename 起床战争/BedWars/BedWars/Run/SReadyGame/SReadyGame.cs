using Microsoft.Xna.Framework;
using ModTool.ServerHelp;

namespace BedWars.Run
{
    internal class SReadyGame : IStateAction
    {
        public SReadyGame(GameRun game) : base(game) { }

        public override void OnStart()
        {
            PrintTo.PrintToPlayAll("准备游戏", Color.YellowGreen);
            CombatTextTo.ToPlayAllOff("准备游戏", 0, -100, Color.White);
        }
    }
}
