using ExtenContent.Extens;
using tContentPatch;
using Terraria;
using test2.Content.Items;

namespace test2.Common
{
    /// <summary>
    /// 领域
    /// </summary>
    internal class Neighborhood : PatchMain
    {
        protected static bool Enable = false;
        protected static float Size = 0;

        public static void Apply()
        {
            Enable = true;
        }

        public override void DoUpdateInWorldPostfix()
        {
            Player player = Main.LocalPlayer;

            if (player.active != true || player.dead == true ||
                (player.HeldItem.type == ExtenManag.ItemType<EItem1>() && Main.mouseRight) != true)
            {
                Enable = false;
                Size = 0;
                return;
            }

            for (int i = 0; i < Main.npc.Length; ++i)
            {
                NPC npc = Main.npc[i];
                if (npc.active != true) continue;
                if (npc.Center.DirectionTo(player.Center).Length() > Size) continue;


            }

            Size += 5;
        }
    }
}
