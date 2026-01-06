using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;

namespace BedWars.BedWarsData
{
    public class ShopData : ICheck
    {
        public string name = null;
        /// <summary>
        /// 商店npc左上角位置, 相对位置
        /// </summary>
        public Point pos;
        /// <summary>
        /// <see cref="Chest.maxItems"/>
        /// </summary>
        public List<ModTool.Common.ModifyShop.ItemData> item = null;

        public void Check(MapData mapData)
        {
            if (mapData.InMapRelative(pos) == false) throw new Exception("商店超出地图");
        }
    }
}
