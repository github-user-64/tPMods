using BedWars.Common.UI;

namespace BedWars.Edit.UI.OtherItem
{
    internal class NoSpawnNPC : UIItemSwitchUpdate
    {
        public NoSpawnNPC(string text) : base(null, null, text)
        {
            MouseText = "让那些烦人的怪物远离你";

            gss = new GetSetStringBool(GetV, SetV);

            SetV(true);
        }

        private static bool GetV()
        {
            return !Common.GameAction.CanSpawnNPC;
        }

        private static void SetV(bool v)
        {
            Common.GameAction.CanSpawnNPC = !v;
        }
    }
}
