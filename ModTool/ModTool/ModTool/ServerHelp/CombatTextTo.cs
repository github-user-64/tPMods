using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 浮动文本
    /// </summary>
    public static class CombatTextTo
    {
        /// <summary>
        /// 到玩家
        /// </summary>
        public static void ToPlay(int clientId, string text, float x, float y, Color color)
        {
            if (Main.dedServ == false) return;

            NetMessage.TrySendData(MessageID.CombatTextString, clientId, -1,
                NetworkText.FromLiteral(text),
                (int)color.PackedValue, x, y);
        }

        /// <summary>
        /// 到全部玩家
        /// </summary>
        public static void ToPlayAll(string text, float x, float y, Color color, int ignoreClient = -1)
        {
            if (Main.dedServ == false) return;

            NetMessage.TrySendData(MessageID.CombatTextString, -1, ignoreClient,
                NetworkText.FromLiteral(text),
                (int)color.PackedValue, x, y);
        }

        /// <summary>
        /// 到全部玩家, 到每个玩家各自的位置加上偏移
        /// </summary>
        public static void ToPlayAllOff(string text, float offX, float offY, Color color, int ignoreClient = -1)
        {
            if (Main.dedServ == false) return;
            if (Main.player == null) return;

            for (int i = 0; i < Main.player.Length; ++i)
            {
                if (i == ignoreClient) continue;
                if (Main.player[i]?.Center is Vector2 pos == false) continue;

                pos.X += offX;
                pos.Y += offY;

                NetMessage.TrySendData(MessageID.CombatTextString, i, -1,
                    NetworkText.FromLiteral(text),
                    (int)color.PackedValue, pos.X, pos.Y);
            }
        }
    }
}
