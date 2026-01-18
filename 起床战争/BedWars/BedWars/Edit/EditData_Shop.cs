using BedWars.BedWarsData;
using Microsoft.Xna.Framework;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string ShopAdd(Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            ShopData temp = new ShopData();
            temp.pos = pos;
            DataCheck.RepairList(ref temp.item, Chest.maxItems);
            temp.ItemBuyOnceToMax();

            DataShops.Add(temp);

            return null;
        }

        public string ShopDel(ShopData data)
        {
            if (Data == null) return "地图数据为null";

            DataShops.Remove(data);

            return null;
        }

        /// <summary>
        /// <paramref name="pos"/>为世界位置
        /// </summary>
        public string ShopSetPos(ShopData data, Point pos)
        {
            if (CheckPos(ref pos) is string ex) return ex;

            data.pos = pos;

            return null;
        }

        /// <summary>
        /// 复制到每个商店
        /// </summary>
        public string ShopCopyToAll(ShopData data)
        {
            if (Data == null) return "地图数据为null";

            foreach (ShopData i in DataShops)
            {
                if (i == data) continue;

                i.Copy(data);
            }

            return null;
        }
    }
}
