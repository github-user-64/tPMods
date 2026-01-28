using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.Social;

namespace ModTool.PatchGame.PNetMessage_SendData
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
            BitsByte bb6 = (byte)0;
            bb6[0] = Main.dayTime;
            bb6[1] = Main.bloodMoon;
            bb6[2] = Main.eclipse;
            writer.Write(bb6);
            writer.Write((byte)Main.moonPhase);
            writer.Write((short)Main.maxTilesX);
            writer.Write((short)Main.maxTilesY);
            writer.Write((short)Main.spawnTileX);
            writer.Write((short)Main.spawnTileY);
            writer.Write((short)Main.worldSurface);
            writer.Write((short)Main.rockLayer);
            writer.Write(Main.ActiveWorldFileData.WorldId);
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
            for (int num11 = 0; num11 < 4; num11++)
            {
                writer.Write((byte)Main.treeStyle[num11]);
            }
            for (int num12 = 0; num12 < 3; num12++)
            {
                writer.Write(Main.caveBackX[num12]);
            }
            for (int num13 = 0; num13 < 4; num13++)
            {
                writer.Write((byte)Main.caveBackStyle[num13]);
            }
            WorldGen.TreeTops.SyncSend(writer);
            if (!Main.raining)
            {
                Main.maxRaining = 0f;
            }
            writer.Write(Main.maxRaining);
            BitsByte bb7 = (byte)0;
            bb7[0] = WorldGen.shadowOrbSmashed;
            bb7[1] = NPC.downedBoss1;
            bb7[2] = NPC.downedBoss2;
            bb7[3] = NPC.downedBoss3;
            bb7[4] = Main.hardMode;
            bb7[5] = NPC.downedClown;
            bb7[6] = ServerSideCharacter;
            bb7[7] = NPC.downedPlantBoss;
            writer.Write(bb7);
            BitsByte bb8 = (byte)0;
            bb8[0] = NPC.downedMechBoss1;
            bb8[1] = NPC.downedMechBoss2;
            bb8[2] = NPC.downedMechBoss3;
            bb8[3] = NPC.downedMechBossAny;
            bb8[4] = Main.cloudBGActive >= 1f;
            bb8[5] = WorldGen.crimson;
            bb8[6] = Main.pumpkinMoon;
            bb8[7] = Main.snowMoon;
            writer.Write(bb8);
            BitsByte bb9 = (byte)0;
            bb9[1] = Main.fastForwardTimeToDawn;
            bb9[2] = Main.slimeRain;
            bb9[3] = NPC.downedSlimeKing;
            bb9[4] = NPC.downedQueenBee;
            bb9[5] = NPC.downedFishron;
            bb9[6] = NPC.downedMartians;
            bb9[7] = NPC.downedAncientCultist;
            writer.Write(bb9);
            BitsByte bb10 = (byte)0;
            bb10[0] = NPC.downedMoonlord;
            bb10[1] = NPC.downedHalloweenKing;
            bb10[2] = NPC.downedHalloweenTree;
            bb10[3] = NPC.downedChristmasIceQueen;
            bb10[4] = NPC.downedChristmasSantank;
            bb10[5] = NPC.downedChristmasTree;
            bb10[6] = NPC.downedGolemBoss;
            bb10[7] = BirthdayParty.PartyIsUp;
            writer.Write(bb10);
            BitsByte bb11 = (byte)0;
            bb11[0] = NPC.downedPirates;
            bb11[1] = NPC.downedFrost;
            bb11[2] = NPC.downedGoblins;
            bb11[3] = Sandstorm.Happening;
            bb11[4] = DD2Event.Ongoing;
            bb11[5] = DD2Event.DownedInvasionT1;
            bb11[6] = DD2Event.DownedInvasionT2;
            bb11[7] = DD2Event.DownedInvasionT3;
            writer.Write(bb11);
            BitsByte bb12 = (byte)0;
            bb12[0] = NPC.combatBookWasUsed;
            bb12[1] = LanternNight.LanternsUp;
            bb12[2] = NPC.downedTowerSolar;
            bb12[3] = NPC.downedTowerVortex;
            bb12[4] = NPC.downedTowerNebula;
            bb12[5] = NPC.downedTowerStardust;
            bb12[6] = Main.forceHalloweenForToday;
            bb12[7] = Main.forceXMasForToday;
            writer.Write(bb12);
            BitsByte bb13 = (byte)0;
            bb13[0] = NPC.boughtCat;
            bb13[1] = NPC.boughtDog;
            bb13[2] = NPC.boughtBunny;
            bb13[3] = NPC.freeCake;
            bb13[4] = Main.drunkWorld;
            bb13[5] = NPC.downedEmpressOfLight;
            bb13[6] = NPC.downedQueenSlime;
            bb13[7] = Main.getGoodWorld;
            writer.Write(bb13);
            BitsByte bb14 = (byte)0;
            bb14[0] = Main.tenthAnniversaryWorld;
            bb14[1] = Main.dontStarveWorld;
            bb14[2] = NPC.downedDeerclops;
            bb14[3] = Main.notTheBeesWorld;
            bb14[4] = Main.remixWorld;
            bb14[5] = NPC.unlockedSlimeBlueSpawn;
            bb14[6] = NPC.combatBookVolumeTwoWasUsed;
            bb14[7] = NPC.peddlersSatchelWasUsed;
            writer.Write(bb14);
            BitsByte bb15 = (byte)0;
            bb15[0] = NPC.unlockedSlimeGreenSpawn;
            bb15[1] = NPC.unlockedSlimeOldSpawn;
            bb15[2] = NPC.unlockedSlimePurpleSpawn;
            bb15[3] = NPC.unlockedSlimeRainbowSpawn;
            bb15[4] = NPC.unlockedSlimeRedSpawn;
            bb15[5] = NPC.unlockedSlimeYellowSpawn;
            bb15[6] = NPC.unlockedSlimeCopperSpawn;
            bb15[7] = Main.fastForwardTimeToDusk;
            writer.Write(bb15);
            BitsByte bb16 = (byte)0;
            bb16[0] = Main.noTrapsWorld;
            bb16[1] = Main.zenithWorld;
            bb16[2] = NPC.unlockedTruffleSpawn;
            bb16[3] = Main.vampireSeed;
            bb16[4] = Main.infectedSeed;
            bb16[5] = Main.teamBasedSpawnsSeed;
            bb16[6] = Main.skyblockWorld;
            bb16[7] = Main.dualDungeonsSeed;
            writer.Write(bb16);
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
            ExtraSpawnPointManager.Write(writer, networking: true);
        }
    }
}
