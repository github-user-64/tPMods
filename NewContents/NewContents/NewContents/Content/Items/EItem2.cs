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
        public override string Texture => "Content/Item1";
        public override LocalizedText DisplayName => this.GetLocalization($"{nameof(DisplayName)}", "姿色恣意");
        public override LocalizedText Tooltip => this.GetLocalization($"{nameof(Tooltip)}",
            $"{Language.GetTextValue("CommonItemTooltip.FlightAndSlowfall")}\n" +
            $"{Language.GetTextValue("CommonItemTooltip.PressDownToHover")}");

        public override void SetDefault(Item item)
        {
            item.width = 22;
            item.height = 20;
            item.accessory = true;
            item.value = Item.buyPrice(0, 40);
            item.rare = 10;
            item.wingSlot = (sbyte)ExtenManag.EquipSlot<EEquip1>();
        }

        public override void ModifyTooltips(Item item, ref int yoyoLogo, ref float oldKB, ref int numLines, ref string[] toolTipLine, ref Color[] lineColors)
        {
            string r = Convert.ToString(Utils.getRand(0, byte.MaxValue), 16).PadLeft(2, '0');
            string g = Convert.ToString(Utils.getRand(0, byte.MaxValue), 16).PadLeft(2, '0');
            string b = Convert.ToString(Utils.getRand(0, byte.MaxValue), 16).PadLeft(2, '0');

            toolTipLine[numLines] = $"\"如[c/{r}{g}{b}:疯]般飞行\"";
            numLines++;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ExtenManag.ItemType<EItem3>())
                .Register();
        }
    }
}
