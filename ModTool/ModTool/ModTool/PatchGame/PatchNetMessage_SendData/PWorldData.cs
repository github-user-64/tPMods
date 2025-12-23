using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Social;

namespace ModTool.PatchGame.PatchNetMessage_SendData
{
    internal static class PWorldData
    {
        /// <summary>
        /// 启用服务端角色
        /// </summary>
        public static bool ServerSideCharacter = false;

        public static void Foo(BinaryWriter writer)
        {
            writer.Write((int)Main.time);
            BitsByte bb5 = (byte)0;
            bb5[0] = Main.dayTime;
            bb5[1] = Main.bloodMoon;
            bb5[2] = Main.eclipse;
            writer.Write(bb5);
            writer.Write((byte)Main.moonPhase);
            writer.Write((short)Main.maxTilesX);
            writer.Write((short)Main.maxTilesY);
            writer.Write((short)Main.spawnTileX);
            writer.Write((short)Main.spawnTileY);
            writer.Write((short)Main.worldSurface);
            writer.Write((short)Main.rockLayer);
            writer.Write(Main.worldID);
            writer.Write(Main.worldName);
            writer.Write((byte)Main.GameMode);
            writer.Write(Main.ActiveWorldFileData.UniqueId.ToByteArray());
            writer.Write(Main.ActiveWorldFileData.WorldGeneratorVersion);
            writer.Write((byte)Main.moonType);
            writer.Write((byte)WorldGen.treeBG1);
            writer.Write((byte)WorldGen.treeBG2);
            writer.Write((byte)WorldGen.treeBG3);
            writer.Write((byte)WorldGen.treeBG4);
            writer.Write((byte)WorldGen.corruptBG);
            writer.Write((byte)WorldGen.jungleBG);
            writer.Write((byte)WorldGen.snowBG);
            writer.Write((byte)WorldGen.hallowBG);
            writer.Write((byte)WorldGen.crimsonBG);
            writer.Write((byte)WorldGen.desertBG);
            writer.Write((byte)WorldGen.oceanBG);
            writer.Write((byte)WorldGen.mushroomBG);
            writer.Write((byte)WorldGen.underworldBG);
            writer.Write((byte)Main.iceBackStyle);
            writer.Write((byte)Main.jungleBackStyle);
            writer.Write((byte)Main.hellBackStyle);
            writer.Write(Main.windSpeedTarget);
            writer.Write((byte)Main.numClouds);
            for (int n = 0; n < 3; n++)
            {
                writer.Write(Main.treeX[n]);
            }
            for (int num8 = 0; num8 < 4; num8++)
            {
                writer.Write((byte)Main.treeStyle[num8]);
            }
            for (int num9 = 0; num9 < 3; num9++)
            {
                writer.Write(Main.caveBackX[num9]);
            }
            for (int num10 = 0; num10 < 4; num10++)
            {
                writer.Write((byte)Main.caveBackStyle[num10]);
            }
            WorldGen.TreeTops.SyncSend(writer);
            if (!Main.raining)
            {
                Main.maxRaining = 0f;
            }
            writer.Write(Main.maxRaining);
            BitsByte bb6 = (byte)0;
            bb6[0] = WorldGen.shadowOrbSmashed;
            bb6[1] = NPC.downedBoss1;
            bb6[2] = NPC.downedBoss2;
            bb6[3] = NPC.downedBoss3;
            bb6[4] = Main.hardMode;
            bb6[5] = NPC.downedClown;
            bb6[6] = ServerSideCharacter;
            bb6[7] = NPC.downedPlantBoss;
            writer.Write(bb6);
            BitsByte bb7 = (byte)0;
            bb7[0] = NPC.downedMechBoss1;
            bb7[1] = NPC.downedMechBoss2;
            bb7[2] = NPC.downedMechBoss3;
            bb7[3] = NPC.downedMechBossAny;
            bb7[4] = Main.cloudBGActive >= 1f;
            bb7[5] = WorldGen.crimson;
            bb7[6] = Main.pumpkinMoon;
            bb7[7] = Main.snowMoon;
            writer.Write(bb7);
            BitsByte bb8 = (byte)0;
            bb8[1] = Main.fastForwardTimeToDawn;
            bb8[2] = Main.slimeRain;
            bb8[3] = NPC.downedSlimeKing;
            bb8[4] = NPC.downedQueenBee;
            bb8[5] = NPC.downedFishron;
            bb8[6] = NPC.downedMartians;
            bb8[7] = NPC.downedAncientCultist;
            writer.Write(bb8);
            BitsByte bb9 = (byte)0;
            bb9[0] = NPC.downedMoonlord;
            bb9[1] = NPC.downedHalloweenKing;
            bb9[2] = NPC.downedHalloweenTree;
            bb9[3] = NPC.downedChristmasIceQueen;
            bb9[4] = NPC.downedChristmasSantank;
            bb9[5] = NPC.downedChristmasTree;
            bb9[6] = NPC.downedGolemBoss;
            bb9[7] = BirthdayParty.PartyIsUp;
            writer.Write(bb9);
            BitsByte bb10 = (byte)0;
            bb10[0] = NPC.downedPirates;
            bb10[1] = NPC.downedFrost;
            bb10[2] = NPC.downedGoblins;
            bb10[3] = Sandstorm.Happening;
            bb10[4] = DD2Event.Ongoing;
            bb10[5] = DD2Event.DownedInvasionT1;
            bb10[6] = DD2Event.DownedInvasionT2;
            bb10[7] = DD2Event.DownedInvasionT3;
            writer.Write(bb10);
            BitsByte bb11 = (byte)0;
            bb11[0] = NPC.combatBookWasUsed;
            bb11[1] = LanternNight.LanternsUp;
            bb11[2] = NPC.downedTowerSolar;
            bb11[3] = NPC.downedTowerVortex;
            bb11[4] = NPC.downedTowerNebula;
            bb11[5] = NPC.downedTowerStardust;
            bb11[6] = Main.forceHalloweenForToday;
            bb11[7] = Main.forceXMasForToday;
            writer.Write(bb11);
            BitsByte bb12 = (byte)0;
            bb12[0] = NPC.boughtCat;
            bb12[1] = NPC.boughtDog;
            bb12[2] = NPC.boughtBunny;
            bb12[3] = NPC.freeCake;
            bb12[4] = Main.drunkWorld;
            bb12[5] = NPC.downedEmpressOfLight;
            bb12[6] = NPC.downedQueenSlime;
            bb12[7] = Main.getGoodWorld;
            writer.Write(bb12);
            BitsByte bb13 = (byte)0;
            bb13[0] = Main.tenthAnniversaryWorld;
            bb13[1] = Main.dontStarveWorld;
            bb13[2] = NPC.downedDeerclops;
            bb13[3] = Main.notTheBeesWorld;
            bb13[4] = Main.remixWorld;
            bb13[5] = NPC.unlockedSlimeBlueSpawn;
            bb13[6] = NPC.combatBookVolumeTwoWasUsed;
            bb13[7] = NPC.peddlersSatchelWasUsed;
            writer.Write(bb13);
            BitsByte bb14 = (byte)0;
            bb14[0] = NPC.unlockedSlimeGreenSpawn;
            bb14[1] = NPC.unlockedSlimeOldSpawn;
            bb14[2] = NPC.unlockedSlimePurpleSpawn;
            bb14[3] = NPC.unlockedSlimeRainbowSpawn;
            bb14[4] = NPC.unlockedSlimeRedSpawn;
            bb14[5] = NPC.unlockedSlimeYellowSpawn;
            bb14[6] = NPC.unlockedSlimeCopperSpawn;
            bb14[7] = Main.fastForwardTimeToDusk;
            writer.Write(bb14);
            BitsByte bb15 = (byte)0;
            bb15[0] = Main.noTrapsWorld;
            bb15[1] = Main.zenithWorld;
            bb15[2] = NPC.unlockedTruffleSpawn;
            writer.Write(bb15);
            writer.Write((byte)Main.sundialCooldown);
            writer.Write((byte)Main.moondialCooldown);
            writer.Write((short)WorldGen.SavedOreTiers.Copper);
            writer.Write((short)WorldGen.SavedOreTiers.Iron);
            writer.Write((short)WorldGen.SavedOreTiers.Silver);
            writer.Write((short)WorldGen.SavedOreTiers.Gold);
            writer.Write((short)WorldGen.SavedOreTiers.Cobalt);
            writer.Write((short)WorldGen.SavedOreTiers.Mythril);
            writer.Write((short)WorldGen.SavedOreTiers.Adamantite);
            writer.Write((sbyte)Main.invasionType);
            if (SocialAPI.Network != null)
            {
                writer.Write(SocialAPI.Network.GetLobbyId());
            }
            else
            {
                writer.Write(0uL);
            }
            writer.Write(Sandstorm.IntendedSeverity);
        }
    }
}
