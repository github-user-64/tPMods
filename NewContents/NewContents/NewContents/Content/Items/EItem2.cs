using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using NewContents.Content.Equips;
using System;
using Terraria;
using Terraria.Localization;

namespace NewContents.Content.Items
{
    internal class EItem2 : ExtenItem
    {
        public override string Texture => "Item_1";
        public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "姿色恣意");
        public override LocalizedText Tooltip => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Tooltip)}",
            $"{Language.GetTextValue("CommonItemTooltip.FlightAndSlowfall")}\n" +
            $"{Language.GetTextValue("CommonItemTooltip.PressDownToHover")}");

        public override void SetDefault(Item item)
        {
            item.width = 22;
            item.height = 20;
            item.accessory = true;
            item.value = Item.buyPrice(0, 40);
            item.rare = 10;
            item.wingSlot = ExtenManag.GetEquipSlot<EEquip1>();
        }

        public override void ModifyTooltips(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            string r = Convert.ToString(Utils.getRand(0, byte.MaxValue), 16).PadLeft(2, '0');
            string g = Convert.ToString(Utils.getRand(0, byte.MaxValue), 16).PadLeft(2, '0');
            string b = Convert.ToString(Utils.getRand(0, byte.MaxValue), 16).PadLeft(2, '0');

            toolTipLine[numLines] = $"\"如[c/{r}{g}{b}:疯]般飞行\"";
            numLines++;
        }


    }
}
