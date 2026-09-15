using ExtenContent.Extens;
using System.Collections.Generic;
using System.IO;
using tContentPatch.Utils;
using Terraria;

namespace ExtenContent.IO
{
    /// <summary>
    /// 玩家扩展数据
    /// </summary>
    public class PlayerExtenData
    {
        /// <summary>
        /// 玩家扩展数据后缀名
        /// </summary>
        public const string ExtensionName = "plre";

        /// <summary/>
        public ExtenItemData[] inventory = null;
        /// <summary/>
        public ExtenItemData[] miscEquips = null;
        /// <summary/>
        public ExtenItemData[] miscDyes = null;
        /// <summary/>
        public ExtenItemData[] bank = null;
        /// <summary/>
        public ExtenItemData[] bank2 = null;
        /// <summary/>
        public ExtenItemData[] bank3 = null;
        /// <summary/>
        public ExtenItemData[] bank4 = null;
        /// <summary/>
        public List<ExtenItemData[]> LoadoutsArmor = new List<ExtenItemData[]>();
        /// <summary/>
        public List<ExtenItemData[]> LoadoutsDye = new List<ExtenItemData[]>();
        /// <summary>
        /// 其它扩展数据
        /// </summary>
        public Dictionary<string, object> Extens = new Dictionary<string, object>();

        /// <summary/>
        public PlayerExtenData() { }

        /// <summary/>
        public PlayerExtenData(Player player)
        {
            inventory = GetSaveData(player.inventory);//物品栏
            miscEquips = GetSaveData(player.miscEquips);//杂项装备
            miscDyes = GetSaveData(player.miscDyes);//杂项染料
            bank = GetSaveData(player.bank.item);//猪猪
            bank2 = GetSaveData(player.bank2.item);//保险箱
            bank3 = GetSaveData(player.bank3.item);//护卫熔炉
            bank4 = GetSaveData(player.bank4.item);//虚空保险库

            //装备
            foreach (EquipmentLoadout items in player.Loadouts)
            {
                LoadoutsArmor.Add(GetSaveData(items.Armor));
            }

            //装备染料
            foreach (EquipmentLoadout items in player.Loadouts)
            {
                LoadoutsDye.Add(GetSaveData(items.Dye));
            }
        }

        private static ExtenItemData[] GetSaveData(Item[] items)
        {
            ExtenItemData[] EItems = new ExtenItemData[items.Length];

            for (int i = 0; i < items.Length; ++i)
            {
                if (items[i].IsAir) continue;//空物品
                if (ExtenManag.IsExtenItem(items[i].type) != true) continue;//不是扩展物品

                EItems[i] = new ExtenItemData(items[i]);
            }

            return EItems;
        }

        /// <summary>
        /// 保存玩家扩展数据(没有扩展名的文件名, 保存的数据)
        /// </summary>
        /// <param name="fileName">没有扩展名的文件名</param>
        /// <param name="data">保存的数据</param>
        /// <returns>保存成功返回<see langword="true"/></returns>
        public static bool Save(string fileName, PlayerExtenData data)
        {
            return ModFile.SaveFileTry(Path.Combine("Players", $"{fileName}.{ExtensionName}"), path =>
            {
                MyJson1.Save(data, path, true);

                return true;
            });
        }

        /// <summary>
        /// 加载玩家扩展数据(没有扩展名的文件名)
        /// </summary>
        /// <param name="fileName">没有扩展名的文件名</param>
        /// <returns>失败返回<see langword="null"/></returns>
        public static PlayerExtenData Load(string fileName)
        {
            object data = null;

            ModFile.ReadFileTry(Path.Combine("Players", $"{fileName}.{ExtensionName}"), path =>
            {
                data = MyJson1.Get2(path, typeof(PlayerExtenData));

                return true;
            });

            return data as PlayerExtenData;
        }
    }
}
