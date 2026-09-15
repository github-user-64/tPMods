using ExtenContent.IO;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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

            List<ExtenItemData> occupy = new List<ExtenItemData>();

            occupy.AddRange(LoadData(player.inventory, data.inventory));
            occupy.AddRange(LoadData(player.miscEquips, data.miscEquips));
            occupy.AddRange(LoadData(player.miscDyes, data.miscDyes));
            occupy.AddRange(LoadData(player.bank.item, data.bank));
            occupy.AddRange(LoadData(player.bank2.item, data.bank2));
            occupy.AddRange(LoadData(player.bank3.item, data.bank3));
            occupy.AddRange(LoadData(player.bank4.item, data.bank4));

            int LoadoutsArmorLen = Math.Min(player.Loadouts.Length, data.LoadoutsArmor?.Count ?? 0);
            for (int i = 0; i < LoadoutsArmorLen; ++i)
            {
                occupy.AddRange(LoadData(player.Loadouts[i].Armor, data.LoadoutsArmor[i]));
            }

            int LoadoutsDyeLen = Math.Min(player.Loadouts.Length, data.LoadoutsDye?.Count ?? 0);
            for (int i = 0; i < LoadoutsDyeLen; ++i)
            {
                occupy.AddRange(LoadData(player.Loadouts[i].Armor, data.LoadoutsDye[i]));
            }

            foreach (ExtenItemData i in occupy)
            {
                ExtenItem ei = ExtenManag.GetItem(i.Name);
                int type;
                int v = 0;
                if (ei == null)
                {
                    v = ExtenItemUnload.TryRegist(i.Name);//注册卸载物品

                    type = ExtenManag.ItemType<ExtenItemUnload>();
                }
                else
                {
                    type = ei.Type;
                }
                //进入世界后再说
                Item.NewItem(player.GetItemSource_InventoryOverflow(), Vector2.Zero, type, i.Stack, i.Prefix, modifier: o =>
                {
                    o.inner.buffType = v;
                });
            }
        }

        internal static void SavePlayerPrefix(PlayerFileData playerFile, bool skipMapSave)
        {
            Player player = playerFile.Player;
            PlayerExtenData data = new PlayerExtenData(player);

            PlayerExtenData.Save(playerFile.GetFileName(), data);
        }

        private static List<ExtenItemData> LoadData(Item[] items, ExtenItemData[] EItems = null)
        {
            List<ExtenItemData> occupy = new List<ExtenItemData>();//对应格子已经有物品了

            if (EItems == null) return occupy;

            int len = Math.Min(items.Length, EItems.Length);
            for (int i = 0; i < len; ++i)
            {
                //空物品
                if (EItems[i] == null) continue;
                if (EItems[i].Name == null) continue;
                if (EItems[i].Stack < 1) continue;

                //对应格子已经有物品, 且不是扩展物品
                if (items[i].IsAir != true && ExtenManag.IsExtenItem(items[i].type) != true)
                {
                    occupy.Add(EItems[i]);
                    continue;
                }

                if (GetItem(EItems[i].Name) == null)//物品没加载
                {
                    int v = ExtenItemUnload.TryRegist(EItems[i].Name);//注册卸载物品

                    items[i].SetDefaults(ExtenManag.ItemType<ExtenItemUnload>());
                    items[i].buffType = v;
                }
                else
                {
                    items[i].SetDefaults(ExtenManag.GetItem(EItems[i].Name).Type);
                }

                items[i].stack = EItems[i].Stack;
                items[i].Prefix(EItems[i].Prefix);
            }

            return occupy;
        }
    }
}
