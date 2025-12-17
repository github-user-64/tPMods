using tContentPatch;

namespace ModTool
{
    internal class cs1 : Mod
    {
        public override void Load()
        {
            PatchGame.PatchMessageBuffer.OnGetDataPr.Add((a, b, v,c) =>
            {
                if (c == 13) return;//控制
                //if (c == 23) return;
                //if (c == 20) return;
                //if (c == 27) return;
                //if (c == 29) return;
                //if (c == 82) return;
                //if (c == 54) return;
                //if (c == 91) return;
                //if (c == 19) return;
                //if (c == 21) return;
                //if (c == 22) return;
                //if (c == 40) return;
                //if (c == 36) return;
                //if (c == 16) return;
                if (c == 5) return;//背包物品
                if (c == 138) return;//未知

                ContentPatch.PrintTry(c.ToString());
            });
        }
    }
}
