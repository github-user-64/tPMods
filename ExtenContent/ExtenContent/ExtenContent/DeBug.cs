using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tContentPatch;
using tContentPatch.ModLoad;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace DeBug
{
    internal class DeBug : Mod
    {
        public class DrawHeldItem : UIElement
        {
            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                base.DrawSelf(spriteBatch);

                if (Main.keyState.IsKeyDown(Keys.NumPad2))
                {
                    ItemSlot.MouseHover(new Item[] { Main.LocalPlayer.HeldItem });
                }
            }
        }
        private class Test : PatchMain
        {
            public override void DoUpdateInWorldPostfix()
            {
                if (Main.keyState.IsKeyDown(Keys.NumPad1) && Main.oldKeyState.IsKeyDown(Keys.NumPad1) == false)
                {
                    int type = ExtenManag.ItemType<MyItem2>();
                    ExtenItem obj = ExtenManag.GetExtenItem(type);

                    int val = Item.cachedItemSpawnsByType[type];
                    //Item.cachedItemSpawnsByType[ItemID.Count] = -1;

                    int v = Item.NewItem(null, Main.MouseWorld, ExtenManag.ItemType<MyItem>(), modifier: i =>
                    {

                    });

                    Item.NewItem(null, Main.MouseWorld, ExtenManag.ItemType<MyItem2>());
                }
            }
        }

        public override void Load(ModObject mo)
        {
            ExtenManag.RegistMod(mo);
        }

        public class MyItem : ExtenItem
        {
            public override string Texture => "MyItem1";
            public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "蓝色搭建");
            public override LocalizedText Tooltip => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Tooltip)}", "测试提示");

            public override void SetDefault(Item This)
            {
                This.shoot = 45;
                This.shootSpeed = 8;

                This.useStyle = 1;
                This.useTime = 12;
                This.useAnimation = 12;
                This.knockBack = 6.5f;
                This.width = 24;
                This.height = 28;
                This.damage = 40;
                This.scale = 1f;
                This.UseSound = SoundID.Item1;
                This.rare = 3;
                This.value = 27000;
                This.melee = true;
            }

            public override void Shoot(Player Player, Item This, int weaponDamage, bool withAudioVisualFeedback)
            {
                Vector2 v = Vector2.Normalize(Main.MouseWorld - Player.Center) * 6;
                Vector2 v2 = v.RotatedBy(-0.5f);
                Vector2 v3 = v.RotatedBy(0.5f);

                Projectile.NewProjectile(null, Player.Center, v, 274, weaponDamage, This.knockBack + 10, Player.whoAmI);
                Projectile.NewProjectile(null, Player.Center, v2, 274, weaponDamage, This.knockBack + 10, Player.whoAmI);
                Projectile.NewProjectile(null, Player.Center, v3, 274, weaponDamage, This.knockBack + 10, Player.whoAmI);
            }
        }

        public class MyItem2 : ExtenItem
        {
            public override string Texture => "Item_1";

            public override void SetDefault(Item This)
            {
                int type = 3468;

                This.width = 22;
                This.height = 20;
                This.accessory = true;
                This.value = Item.buyPrice(0, 40);
                This.rare = 10;
                This.wingSlot = (sbyte)(29 + type - 3468);
            }
        }
    }
}
