using ModTool.Utils.GetDataEventArgs;

namespace BedWars.Run
{
    internal class SMapInit : IStateAction
    {
        public SMapInit(GameRun game) : base(game)
        {
        }

        public override bool PlayCanActionTile(ClassTileEventArgs e)
        {
            return base.PlayCanActionTile(e);
        }
    }
}
