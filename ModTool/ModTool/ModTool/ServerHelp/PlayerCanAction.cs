using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 玩家能否交互
    /// </summary>
    public static class PlayerCanAction
    {
        internal static void Init()
        {
            PatchGame.PatchMessageBuffer.OnCanGetData.Add(CanNewProjectile);
        }

        /// <summary/>
        public delegate bool NewProjectileEvent(Player player, int type, int damage);
        /// <summary>
        /// 能否生成射弹, 当有一个返回<see langword="false"/>则<see cref="OnCanNewProjectile"/>剩下的不会再执行
        /// </summary>
        public static readonly List<NewProjectileEvent> OnCanNewProjectile = new List<NewProjectileEvent>();

        private static bool CanNewProjectile(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.SyncProjectile) return true;
            if (Main.netMode != 2) return true;

            int whoAmI = This.whoAmI;
            if (Main.player.IndexInRange(whoAmI) != true) return true;
            Player player = Main.player[whoAmI];
            if (player == null) return true;

            int identity = This.reader.ReadInt16();
            Vector2 position = This.reader.ReadVector2();
            Vector2 velocity = This.reader.ReadVector2();
            int owner = This.reader.ReadByte();
            int type = This.reader.ReadInt16();

            BitsByte bitsByte8 = This.reader.ReadByte();
            BitsByte bitsByte9 = (byte)(bitsByte8[2] ? This.reader.ReadByte() : 0);
            float[] array3 = new float[Projectile.maxAI];
            array3[0] = (bitsByte8[0] ? This.reader.ReadSingle() : 0f);
            array3[1] = (bitsByte8[1] ? This.reader.ReadSingle() : 0f);
            int bannerIdToRespondTo = (bitsByte8[3] ? This.reader.ReadUInt16() : 0);
            int damage2 = (bitsByte8[4] ? This.reader.ReadInt16() : 0);
            float knockBack2 = (bitsByte8[5] ? This.reader.ReadSingle() : 0f);
            int originalDamage = (bitsByte8[6] ? This.reader.ReadInt16() : 0);
            int num68 = (bitsByte8[7] ? This.reader.ReadInt16() : (-1));

            OnCanNewProjectile.RemoveAll(i => i == null);

            foreach (NewProjectileEvent i in OnCanNewProjectile)
            {
                if (i.Invoke(player, type, damage2) == false)
                {
                    for (int index = 0; index < Main.projectile.Length; ++index)
                    {
                        Projectile proj = Main.projectile[index];

                        if (proj.active) continue;

                        proj.identity = identity;
                        proj.type = ProjectileID.None;
                        proj.owner = owner;

                        NetMessage.TrySendData(MessageID.SyncProjectile, This.whoAmI, -1, null, index);
                        break;
                    }
                    
                    return false;
                }
            }
            
            return true;
        }
    }
}
