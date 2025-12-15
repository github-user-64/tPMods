//using Microsoft.Xna.Framework;
//using System;
//using tContentPatch;
//using Terraria;

//namespace ModTool.ServerHelp
//{
//    internal class a1 : PatchPlayer
//    {
//        public override void Initialize()
//        {
//            Utils.ServerSideCharacter(true);

//            PlayerCanAction.OnCanControls.Add((p, e) =>
//            {
//                //p.controlUp = e.controlUp;
//                //p.controlDown = e.controlDown;
//                //p.controlLeft = e.controlLeft;
//                //p.controlRight = e.controlRight;
//                //p.controlJump = e.controlJump;
//                p.position = e.position;

//                Vector2 pos = new Vector2(Main.spawnTileX, Main.spawnTileY) * 16;
//                float d = p.Distance(pos);
//                if (d < 16 * 20) return false;

//                p.Center = pos;
//                p.velocity = Vector2.Zero;
//                NetMessage.SendData(96, -1, -1, null, p.whoAmI, p.Center.X, p.Center.Y, 0);

//                return false;
//            });
//        }

//        //public override void UpdatePrefix(Player This, int playerI)
//        //{
//        //    Vector2 pos = new Vector2(Main.spawnTileX, Main.spawnTileY) * 16;
//        //    float d = This.Distance(pos);
//        //    if (d < 16 * 20) return;

//        //    This.Center = pos;
//        //    This.velocity = Vector2.Zero;
//        //    NetMessage.SendData(96, -1, -1, null, This.whoAmI, This.Center.X, This.Center.Y, 0);
//        //}
//    }
//}
