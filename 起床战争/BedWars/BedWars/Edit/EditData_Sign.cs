using BedWars.BedWarsData;
using System.Collections.Generic;
using Terraria;

namespace BedWars.Edit
{
    public partial class EditData
    {
        /// <summary>
        /// 复制告示牌数据
        /// </summary>
        public string SignCopy()
        {
            if (Data == null) return "地图数据为null";

            List<(Sign, int)> cs = Common.Utils.GetInRangeSign(DataInfo.rect);

            DataSigns.Clear();

            cs.ForEach(i =>
            {
                SignData c = new SignData();
                c.Copy(Data, i.Item1);

                DataSigns.Add(c);
            });

            return null;
        }

        /// <summary>
        /// 粘贴告示牌数据
        /// </summary>
        public string SignPaste()
        {
            if (Data == null) return "地图数据为null";

            Common.Utils.ClearInRangeSign(DataInfo.rect);//清除

            DataSigns.ForEach(i => i.Paste(Data));//创建

            return null;
        }
    }
}
