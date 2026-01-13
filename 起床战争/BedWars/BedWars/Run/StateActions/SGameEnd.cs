using Microsoft.Xna.Framework;
using ModTool.ServerHelp;

namespace BedWars.Run.StateActions
{
    internal class SGameEnd : IStateAction
    {
        public SGameEnd(GameRun game) : base(game) { }

        public override void OnStart(object arg)
        {
            GameTeamData team = arg as GameTeamData;

            string text = "";
            team.ForPlay(i => text += i.name + ",");

            ToPlayerPrint.PrintToPlayAll(text, Color.AliceBlue);
        }
    }
}
