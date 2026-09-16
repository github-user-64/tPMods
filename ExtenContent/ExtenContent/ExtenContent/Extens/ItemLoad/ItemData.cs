using ExtenContent.IO;
using System;
using Terraria;
using Terraria.IO;

namespace ExtenContent.Extens
{
    public static partial class ItemLoad
    {
        internal static void LoadPlayerPostfix(PlayerFileData result, string playerPath, bool cloudSave)
        {
            PlayerExtenData data = PlayerExtenData.Load(result.GetFileName());
            if (data == null) return;

            Player player = result.Player;

            LoadData(player.inventory, data.inventory);
            LoadData(player.miscEquips, data.miscEquips);
            LoadData(player.miscDyes, data.miscDyes);
            LoadData(player.bank.item, data.bank);
            LoadData(player.bank2.item, data.bank2);
            LoadData(player.bank3.item, data.bank3);
            LoadData(player.bank4.item, data.bank4);

            int LoadoutsArmorLen = Math.Min(player.Loadouts.Length, data.LoadoutsArmor?.Count ?? 0);
            for (int i = 0; i < LoadoutsArmorLen; ++i)
            {
                LoadData(player.Loadouts[i].Armor, data.LoadoutsArmor[i]);
            }

            int LoadoutsDyeLen = Math.Min(player.Loadouts.Length, data.LoadoutsDye?.Count ?? 0);
            for (int i = 0; i < LoadoutsDyeLen; ++i)
            {
                LoadData(player.Loadouts[i].Armor, data.LoadoutsDye[i]);
            }
        }

        internal static void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            Player player = playerFile.Player;
            PlayerExtenData data = new PlayerExtenData(player);

            PlayerExtenData.Save(playerFile.GetFileName(), data);
        }

        private static void LoadData(Item[] items, ExtenItemData[] EItems = null)
        {
            int len = Math.Min(items.Length, EItems.Length);
            for (int i = 0; i < len; ++i)
            {
                //空物品
                if (EItems[i] == null) continue;
                if (EItems[i].Name == null) continue;
                if (EItems[i].Stack < 1) continue;

                bool isUnload = GetItem(EItems[i].Name) == null;//物品没加载
                if (isUnload) RegisterUnload(EItems[i].Name);//注册卸载物品

                //对应格子已经有物品, 且不是扩展物品
                if (items[i].IsAir != true && ExtenManag.IsExtenItem(items[i].type) != true) continue;

                if (isUnload)
                {
                    items[i].SetDefaults(ExtenManag.ItemType<UnloadItem>());
                    items[i].SetNameOverride(EItems[i].Name);
                }
                else
                {
                    items[i].SetDefaults(ExtenManag.GetItem(EItems[i].Name).Type);
                }

                items[i].stack = EItems[i].Stack;
                items[i].Prefix(EItems[i].Prefix);
            }
        }
    }
}
