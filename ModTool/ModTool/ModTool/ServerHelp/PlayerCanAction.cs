using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using tContentPatch;
using Terraria;
using Terraria.ID;

namespace ModTool.ServerHelp
{
    /// <summary>
    /// 玩家能否交互
    /// </summary>
    public static partial class PlayerCanAction
    {
        internal static void Init()
        {
            PatchGame.PatchMessageBuffer.OnCanGetData.Add(CanNewProjectile);
            PatchGame.PatchMessageBuffer.OnCanGetData.Add(CanTogglePVP);
            PatchGame.PatchMessageBuffer.OnCanGetData.Add(CanToggleTeam);
            PatchGame.PatchMessageBuffer.OnCanGetData.Add(CanControls);

            //OnCanControls.Add((p, t) =>
            //{
            //    //if (p.HeldItem.type == 0) return true;
            //    PrintTo.PrintToPlay(p.whoAmI, "不可以哦", Color.AliceBlue);
            //    return false;
            //});
        }

        private static bool GetP(MessageBuffer This, out Player player)
        {
            player = null;

            if (Main.netMode != 2) return false;

            if (Main.player.IndexInRange(This.whoAmI) != true) return false;
            player = Main.player[This.whoAmI];

            return player != null;
        }

        private static bool Foo<T>(this List<T> list, Func<T, bool> action)
        {
            list.RemoveAll(i => i == null);

            foreach (T i in list)
            {
                if (action(i) == false) return false;
            }

            return true;
        }

        private static bool CanNewProjectile(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.SyncProjectile) return true;
            if (GetP(This, out Player player) == false) return true;

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

            return OnCanNewProjectile.Foo(i =>
            {
                if (i.Invoke(player, type, damage2)) return true;

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
            });
        }

        private static bool CanTogglePVP(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.TogglePVP) return true;
            if (GetP(This, out Player player) == false) return true;

            int _whoAmI = This.reader.ReadByte();
            bool hostile = This.reader.ReadBoolean();

            return OnCanTogglePVP.Foo(i =>
            {
                if (i.Invoke(player, hostile)) return true;

                NetMessage.TrySendData(MessageID.TogglePVP, This.whoAmI, -1, null, player.whoAmI);

                return false;
            });
        }

        private static bool CanToggleTeam(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.Unknown45) return true;
            if (GetP(This, out Player player) == false) return true;

            int _whoAmI = This.reader.ReadByte();
            int team = This.reader.ReadByte();

            return OnCanToggleTeam.Foo(i =>
            {
                if (i.Invoke(player, team)) return true;

                NetMessage.TrySendData(MessageID.Unknown45, This.whoAmI, -1, null, player.whoAmI);

                return false;
            });
        }

        private static bool CanControls(MessageBuffer This, int start, int length, int messageType)
        {
            if (messageType != MessageID.PlayerControls) return true;
            if (GetP(This, out Player player) == false) return true;

            int _whoAmI = This.reader.ReadByte();
            BitsByte bs0 = This.reader.ReadByte();
            BitsByte bs1 = This.reader.ReadByte();
            BitsByte bs2 = This.reader.ReadByte();
            BitsByte bs3 = This.reader.ReadByte();
            int selectedItem = This.reader.ReadByte();
            Vector2 position = This.reader.ReadVector2();

            ControlsEventArgs e = new ControlsEventArgs();
            e.controlUp = bs0[0];
            e.controlDown = bs0[1];
            e.controlLeft = bs0[2];
            e.controlRight = bs0[3];
            e.controlJump = bs0[4];
            e.controlUseItem = bs0[5];
            e.position = position;

            return OnCanControls.Foo(i =>
            {
                if (i.Invoke(player, e)) return true;

                NetMessage.TrySendData(MessageID.PlayerControls, This.whoAmI, -1, null, player.whoAmI);

                return false;
            });
        }
    }
}
