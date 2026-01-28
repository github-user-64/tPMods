using BedWars.BedWarsData;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// 复制箱子数据
        /// </summary>
        public string ChestCopy()
        {
            if (Data == null) return "地图数据为null";

            List<(Chest, int)> cs = Common.Utils.GetInRangeChest(DataInfo.rect);

            DataChests.Clear();

            cs.ForEach(i =>
            {
                if (i.Item1.bankChest) return;//是类似猪猪存钱罐的东西

                ChestData c = new ChestData();
                DataCheck.RepairList(ref c.item, DataCheck.ChestMaxItems);
                c.Copy(Data, i.Item1);

                DataChests.Add(c);
            });

            return null;
        }

        /// <summary>
        /// 粘贴箱子数据
        /// </summary>
        public string ChestPaste()
        {
            if (Data == null) return "地图数据为null";

            Common.Utils.ClearInRangeChest(DataInfo.rect);//清除

            DataChests.ForEach(i => i.Paste(Data));//创建

            return null;
        }
    }
}
