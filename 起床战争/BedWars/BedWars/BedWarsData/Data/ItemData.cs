using Terraria;

namespace BedWars.BedWarsData
{
    public class ItemData : ICheck
    {
        public int type;
        public int stack;
        public byte prefix;

        public void Check(MapData mapData)
        {

        }

        /// <summary>
        /// 复制物品, 如果参数为空, 则重置数据
        /// </summary>
        public ItemData Copy(Item item = null)
        {
            if (item == null)
            {
                type = 0;
                stack = 0;
                prefix = 0;
                return this;
            }
            type = item.type;
            stack = item.stack;
            prefix = item.prefix;
            return this;
        }

        /// <summary>
        /// 粘贴物品, 如果参数为空则跳过
        /// </summary>
        public void Paste(Item item = null)
        {
            if (item == null) return;
            item.SetDefaults(type);
            //item.type = type;
            item.stack = stack;
            item.prefix = prefix;
        }
    }
}
