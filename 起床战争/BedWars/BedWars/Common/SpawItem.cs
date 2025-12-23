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
            public string name = null;
            public Vector2 pos = Vector2.Zero;
            public List<int> types = new List<int>();
            /// <summary>
            /// 如果为<see langword="true"/>则从除了<see cref="types"/>的所有中选择
            /// </summary>
            public bool exclude = false;
            public int maxStack = 1;
            public int cd = 30;

            public void Spaw()
            {
                if (maxStack < 1) return;
                if (WorldGen.InWorld((int)pos.X / 16, (int)pos.Y / 16) == false) return;

                int type = 0;

                if (exclude)
                {
                    type = Utils.Utils.GetRandItemID(types);
                }
                else if (types?.Count > 0)
                {
                    type = types[ModTool.Utils.Utils.GetRand(0, types.Count)];
                }

                if (type == 0) return;

                Item item = new Item();
                item.SetDefaults(type);
                if (item.maxStack < 1) return;

                int stack = ModTool.Utils.Utils.GetRand(1, maxStack + 1);
                if (stack > item.maxStack) stack = item.maxStack;
                else if (stack < 1) stack = 1;

                Item.NewItem(null, pos, Vector2.Zero, type, stack);
            }
        }

        internal static void Init()
        {
            PMain.OnDoUpdateInWorldPr += DoUpdateInWorldPr;
        }

        /// <summary>
        /// 不要往里塞<see langword="null"/>
        /// </summary>
        public static readonly List<SpawData> SpawDatas = new List<SpawData>();

        private static void DoUpdateInWorldPr()
        {
            try
            {
                foreach (SpawData data in SpawDatas)
                {
                    if (data.cd > 0 && Main.GameUpdateCount % data.cd != 0) continue;

                    data.Spaw();
                }
            }
            catch { }
        }
    }
}
