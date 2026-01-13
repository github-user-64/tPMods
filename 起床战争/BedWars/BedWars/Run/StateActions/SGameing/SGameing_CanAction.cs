using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using ModTool.Utils.GetDataEventArgs;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal partial class SGameing
    {
        public override bool PlayCanAction(GetDataEventArgs e)
        {
            if (base.PlayCanAction(e) == true) return true;

            GameTeamData team = game.GetPlayerTeam(e.player);
            if (team != null) return true;

            return false;
        }

        //放置图格只能在:游戏中 && 地图范围内 && (没图格 || 可交互图格)
        //破坏图格只能在:游戏中 && 玩家放置图格 || 可交互图格
        public override bool PlayCanActionTile(ClassTileEventArgs e)
        {
            GameTeamData team = game.GetPlayerTeam(e.player);
            if (team == null) return false;//没队伍

            Point mapPos = new Point(e.x, e.y);
            mapPos.X -= game.DataInfo.pos.X;
            mapPos.Y -= game.DataInfo.pos.Y;

            if (game.Data.InMapRelative(mapPos) == false) return false;//不在地图里

            TileData data = game.DataTile[mapPos.Y][mapPos.X];
            if (data.canAction == true) return true;//是可交互图格

            if (e is TileManipulationEventArgs tm)
            {
                bool isKill = false;
                bool isPlace = false;

                switch (tm.manipulationType)
                {
                    case 0: isKill = true; break;
                    case 2: isKill = true; break;
                    case 4: isKill = true; break;
                    case 6: isKill = true; break;
                    case 9: isKill = true; break;
                    case 11: isKill = true; break;
                    case 13: isKill = true; break;
                    case 17: isKill = true; break;
                    case 20: isKill = true; break;
                    case 1: isPlace = true; break;
                    default: break;
                }

                if (isKill) return OnCanKillTile(tm);
            }

            return false;
        }

        private bool OnCanKillTile(TileManipulationEventArgs e)
        {
            return false;
        }

        public void asd()
        {
            byte b9;
            if (b9 == 1)
            {
                bool forced = true;
                if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(num187, num188))
                {
                    flag13 = true;
                    forced = false;
                }
                WorldGen.PlaceTile(num187, num188, num189, mute: false, forced, -1, num190);
            }
            if (b9 == 2)
            {
                WorldGen.KillWall(num187, num188, flag12);
            }
            if (b9 == 3)
            {
                WorldGen.PlaceWall(num187, num188, num189);
            }
            if (b9 == 4)
            {
                WorldGen.KillTile(num187, num188, flag12, effectOnly: false, noItem: true);
            }
            if (b9 == 5)
            {
                WorldGen.PlaceWire(num187, num188);
            }
            if (b9 == 6)
            {
                WorldGen.KillWire(num187, num188);
            }
            if (b9 == 7)
            {
                WorldGen.PoundTile(num187, num188);
            }
            if (b9 == 8)
            {
                WorldGen.PlaceActuator(num187, num188);
            }
            if (b9 == 9)
            {
                WorldGen.KillActuator(num187, num188);
            }
            if (b9 == 10)
            {
                WorldGen.PlaceWire2(num187, num188);
            }
            if (b9 == 11)
            {
                WorldGen.KillWire2(num187, num188);
            }
            if (b9 == 12)
            {
                WorldGen.PlaceWire3(num187, num188);
            }
            if (b9 == 13)
            {
                WorldGen.KillWire3(num187, num188);
            }
            if (b9 == 14)
            {
                WorldGen.SlopeTile(num187, num188, num189);
            }
            if (b9 == 15)
            {
                Minecart.FrameTrack(num187, num188, pound: true);
            }
            if (b9 == 16)
            {
                WorldGen.PlaceWire4(num187, num188);
            }
            if (b9 == 17)
            {
                WorldGen.KillWire4(num187, num188);
            }
            switch (b9)
            {
                case 18:
                    Wiring.SetCurrentUser(this.whoAmI);
                    Wiring.PokeLogicGate(num187, num188);
                    Wiring.SetCurrentUser();
                    return;
                case 19:
                    Wiring.SetCurrentUser(this.whoAmI);
                    Wiring.Actuate(num187, num188);
                    Wiring.SetCurrentUser();
                    return;
                case 20:
                    if (WorldGen.InWorld(num187, num188, 2))
                    {
                        int type16 = Main.tile[num187, num188].type;
                        WorldGen.KillTile(num187, num188, flag12);
                        num189 = (short)((Main.tile[num187, num188].active() && Main.tile[num187, num188].type == type16) ? 1 : 0);
                        if (Main.netMode == 2)
                        {
                            NetMessage.TrySendData(17, -1, -1, null, b9, num187, num188, num189, num190);
                        }
                    }
                    return;
                case 21:
                    WorldGen.ReplaceTile(num187, num188, (ushort)num189, num190);
                    break;
            }
            if (b9 == 22)
            {
                WorldGen.ReplaceWall(num187, num188, (ushort)num189);
            }
            if (b9 == 23)
            {
                WorldGen.SlopeTile(num187, num188, num189);
                WorldGen.PoundTile(num187, num188);
            }
            if (Main.netMode == 2)
            {
                if (flag13)
                {
                    NetMessage.SendTileSquare(-1, num187, num188, 5);
                }
                else if ((b9 != 1 && b9 != 21) || !TileID.Sets.Falling[num189] || Main.tile[num187, num188].active())
                {
                    NetMessage.TrySendData(17, -1, this.whoAmI, null, b9, num187, num188, num189, num190);
                }
            }
        }
    }
}
