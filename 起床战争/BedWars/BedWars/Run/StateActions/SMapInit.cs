using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace BedWars.Run.StateActions
{
    internal class SMapInit : IStateAction
    {
        public SMapInit(GameRun game) : base(game) { }

        //设置玩家队伍,禁用pvp,所有玩家重生到进入游戏位置,清空玩家背包,清空玩家放置图格列表,清理图格,放置图格,进入准备游戏
        public override void OnStart(object arg)
        {
            ModTool.ServerHelp.ToPlayerPrint.PrintToPlayAll("初始化地图", Color.YellowGreen);

            game.ForActivePlayer(i =>
            {
                game.SetTeamPvP(i, 0, false);

                game.SpawnToMapSpaw(i);

                game.ClearInventory(i);
            });

            ResetTile();

            game.SetStateUpdate(GameRun.StateReadyGame);
        }

        private void ResetTile()
        {
            MapData data = game.Data;
            MapInfoData info = game.DataInfo;

            Common.Utils.ClearInRangeChest(info.rect);//清除箱子
            Common.Utils.ClearInRangeSign(info.rect);//清除告示牌

            //

            for (int y = 0; y < info.Height; ++y)
            {
                for (int x = 0; x < info.Width; ++x)
                {
                    Tile tile = Main.tile[info.X + x, info.Y + y];
                    TileData td = data.Tile[y][x];

                    TileData.Place(td, tile);
                }
            }

            data.Chests.ForEach(i => i.Paste(data));//创建箱子
            data.Signs.ForEach(i => i.Paste(data));//创建告示牌

            //

            if (Netplay.HasClients == false) return;

            needSend = info.rect;
            sendBlock = new Rectangle(needSend.X, needSend.Y, maxSendWidth, maxSendHeight);

            while (SendUpdate() == false)
            {
                if (Netplay.HasClients == false) return;

                System.Threading.Thread.Sleep(250);
            }
        }

        private readonly int maxSendWidth = 512;
        private readonly int maxSendHeight = 512;
        private Rectangle needSend = Rectangle.Empty;
        private Rectangle sendBlock = Rectangle.Empty;
        private bool SendUpdate()
        {
            Rectangle send = Rectangle.Intersect(needSend, sendBlock);
            if (send.IsEmpty) return true;

            NetMessage.TrySendData(MessageID.TileSection, -1, -1, null,
                send.X, send.Y, send.Width, send.Height);

            sendBlock.X += maxSendWidth;
            if (needSend.Intersects(sendBlock) == false)
            {
                sendBlock.X = needSend.X;
                sendBlock.Y += maxSendHeight;
            }

            return false;
        }
    }
}
