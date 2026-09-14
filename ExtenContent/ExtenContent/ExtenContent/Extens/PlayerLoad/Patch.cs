using ExtenContent.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.IO;

namespace ExtenContent.Extens
{
    public static partial class PlayerLoad
    {
        internal static void LoadPlayerPostfix(PlayerFileData result, string playerPath, bool cloudSave)
        {
            PlayerExtenData data = PlayerExtenData.Load(result.GetFileName());
            if (data == null) return;

            Player player = result.Player;

            Set(player.inventory, data[nameof(Player.inventory)] as ExtenItemData[]);
            Set(player.miscEquips, data[nameof(Player.miscEquips)] as ExtenItemData[]);
            Set(player.miscDyes, data[nameof(Player.miscDyes)] as ExtenItemData[]);
            Set(player.bank.item, data[nameof(Player.bank)] as ExtenItemData[]);
            Set(player.bank2.item, data[nameof(Player.bank2)] as ExtenItemData[]);
            Set(player.bank3.item, data[nameof(Player.bank3)] as ExtenItemData[]);
            Set(player.bank4.item, data[nameof(Player.bank4)] as ExtenItemData[]);
        }

        internal static void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            PlayerExtenData data = new PlayerExtenData();
            Player player = playerFile.Player;

            data[nameof(Player.inventory)] = Get(player.inventory);//物品栏
            data[nameof(Player.miscEquips)] = Get(player.miscEquips);//杂项装备
            data[nameof(Player.miscDyes)] = Get(player.miscDyes);//杂项染料
            data[nameof(Player.bank)] = Get(player.bank.item);//猪猪
            data[nameof(Player.bank2)] = Get(player.bank2.item);//保险箱
            data[nameof(Player.bank3)] = Get(player.bank3.item);//护卫熔炉
            data[nameof(Player.bank4)] = Get(player.bank4.item);//虚空保险库

            //装备
            List<ExtenItemData[]> LoadoutsArmor = new List<ExtenItemData[]>();
            foreach (EquipmentLoadout items in player.Loadouts)
            {
                LoadoutsArmor.Add(Get(items.Armor));
            }
            data["LoadoutsArmor"] = LoadoutsArmor;

            //装备染料
            List<ExtenItemData[]> LoadoutsDye = new List<ExtenItemData[]>();
            foreach (EquipmentLoadout items in player.Loadouts)
            {
                LoadoutsDye.Add(Get(items.Dye));
            }
            data["LoadoutsDye"] = LoadoutsDye;

            PlayerExtenData.Save(playerFile.GetFileName(), data);
        }

        private static ExtenItemData[] Get(Item[] items)
        {
            ExtenItemData[] EItems = new ExtenItemData[items.Length];

            for (int i = 0; i < items.Length; ++i)
            {
                if (items[i].IsAir) continue;
                if (ExtenManag.IsExtenItem(items[i].type) != true) continue;

                EItems[i] = new ExtenItemData(items[i]);
            }

            return EItems;
        }

        private static void Set(Item[] items, ExtenItemData[] EItems = null)
        {
            if (EItems == null) return;

            List<ExtenItemData> occupy = new List<ExtenItemData>();//对应格子已经有物品了

            for (int i = 0; i < items.Length; ++i)
            {
                if (EItems[i] == null) continue;
                //对应格子已经有物品, 且不是模组物品
                if (items[i].IsAir != true && ExtenManag.IsExtenItem(items[i].type) != true)
                {
                    occupy.Add(EItems[i]);
                    continue;
                }

                //items[i].SetDefaults();
            }
        }

        private class ExtenItemData
        {
            public string Name;
            public int Stack;
            public int Prefix;

            public ExtenItemData(Item item)
            {
                ExtenItem EItem = ExtenManag.GetExtenItem(item.type);

                //if (EItem is ) return;
                Name = ExtenManag.GetExtenItem(item.type).FullName;
                Stack = item.stack;
                Prefix = item.prefix;
            }
        }
    }
}
