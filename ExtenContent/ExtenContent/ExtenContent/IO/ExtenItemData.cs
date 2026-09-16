using ExtenContent.Extens;
using Terraria;

namespace ExtenContent.IO
{
    /// <summary>
    /// 扩展物品数据
    /// </summary>
    public class ExtenItemData
    {
        /// <summary>
        /// <see cref="ExtenType.FullName"/>
        /// </summary>
        public string Name;
        /// <summary/>
        public int Stack;
        /// <summary/>
        public int Prefix;

        /// <summary/>
        public ExtenItemData() { }

        /// <summary/>
        public ExtenItemData(Item item)
        {
            ExtenItem EItem = ExtenManag.GetExtenItem(item.type);

            if (EItem is UnloadItem)
            {
                Name = ExtenManag.GetUnloadItemKey(item.Name);
            }
            else
            {
                Name = ExtenManag.GetExtenItem(item.type).FullName;
            }

            Stack = item.stack;
            Prefix = item.prefix;
        }
    }
}
