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

        public static void Copy(ItemData data, Item item)
        {
            data.type = item.type;
            data.stack = item.stack;
            data.prefix = item.prefix;
        }

        public static void Paste(ItemData data, Item item)
        {
            item.type = data.type;
            item.stack = data.stack;
            item.prefix = data.prefix;
        }
    }
}
