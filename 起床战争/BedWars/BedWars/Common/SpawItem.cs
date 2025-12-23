using Microsoft.Xna.Framework;
using ModTool.PatchGame;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Common
{
    public static class SpawItem
    {
        public class SpawData
        {
            public string Name = null;
            public Vector2 Pos = Vector2.Zero;
            public List<int> Types = new List<int>();
            /// <summary>
            /// 如果为<see langword="true"/>则从除了<see cref="Types"/>的所有中选择
            /// </summary>
            public bool Exclude = false;
            public int MaxStack = 1;

            public void Spaw()
            {
                if (MaxStack < 1) return;
                if (WorldGen.InWorld((int)Pos.X, (int)Pos.Y) == false) return;

                int type = 0;

                if (Exclude)
                {
                    type = Utils.Utils.GetRandItemID(Types);
                }
                else if (Types?.Count > 0)
                {
                    type = Types[ModTool.Utils.Utils.GetRand(0, Types.Count)];
                }

                if (type == 0) return;

                Item item = new Item();
                item.SetDefaults(type);
                if (item.maxStack < 1) return;

                int stack = ModTool.Utils.Utils.GetRand(1, MaxStack + 1);
                if (stack > item.maxStack) stack = item.maxStack;
                else if (stack < 1) stack = 1;

                Item.NewItem(null, Pos, -Vector2.UnitY * 2, type, stack);
            }
        }

        internal static void Init()
        {
            PMain.OnDoUpdateInWorldPr += DoUpdateInWorldPr;
        }

        public static readonly List<SpawData> SpawDatas = new List<SpawData>();

        private static void DoUpdateInWorldPr()
        {
            
        }
    }
}
