using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class MapData
    {
        public MapInfoData Info = null;
        public List<SpawItemData> SpawItems = null;
        public List<TeamData> Teams = null;
        public List<List<TileData>> Tile = null;
        public List<ChestData> Chests = null;
        public List<SignData> Signs = null;

        /// <exception cref="Exception"/>
        public void Check()
        {
            Info.Check(this);
            SpawItems.ForEach(i => i.Check(this));
            Teams.ForEach(i => i.Check(this));
            Tile.ForEach(i => i.ForEach(j => j.Check(this)));
            Chests.ForEach(i => i.Check(this));
            Signs.ForEach(i => i.Check(this));
        }

        /// <summary>
        /// 修复地图数据, 清理<see langword="null"/>, 不包括数据合理性, 检查数据合理性调用<see cref="Check"/>
        /// </summary>
        public void Repair()
        {
            if (Info == null) Info = new MapInfoData();

            DataCheck.RepairList(ref SpawItems);

            DataCheck.RepairList(ref Teams);

            DataCheck.RepairList(ref Chests);
            Chests.ForEach(i =>
            {
                DataCheck.RepairList(ref i.item, Chest.maxItems);
            });

            DataCheck.RepairList(ref Signs);

            RepairTile();
        }

        /// <summary>
        /// <see cref="Tile"/>超出大小的就删除, 小于就添加, 填满<see langword="null"/>
        /// </summary>
        public void RepairTile()
        {
            DataCheck.RepairList(ref Tile, Info.size.X, Info.size.Y);
        }
    }
}
