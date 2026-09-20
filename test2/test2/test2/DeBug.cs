using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using tContentPatch;
using tContentPatch.ModLoad;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;

namespace test2
{
    internal class DeBug : Mod
    {
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

            public override void SetStaticDefaults()
            {
                ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;//有右键单击功能时,允许按住鼠标右键重复使用
            }

            public override void SetDefault(Item item)
            {
                item.shoot = ExtenManag.ProjectileType<MyProj1>();
                item.shootSpeed = 8;

                item.useStyle = 1;
                item.useTime = 12;
                item.useAnimation = 12;
                item.knockBack = 6.5f;
                item.width = 24;
                item.height = 28;
                item.damage = 40;
                item.scale = 1f;
                item.UseSound = SoundID.Item1;
                item.rare = 3;
                item.value = 27000;
                item.melee = true;
                item.noMelee = true;//true时该物品的使用动画不会造成伤害
                item.noUseGraphic = true;//true时该物品的使用动画不会显示
                item.useStyle = 5;
            }

            public override void Shoot(Player player, Item item, int weaponDamage, bool withAudioVisualFeedback)
            {
                //Vector2 v = Vector2.Normalize(Main.MouseWorld - player.Center) * 6;
                //Vector2 v2 = v.RotatedBy(-0.5f);
                //Vector2 v3 = v.RotatedBy(0.5f);

                //Projectile.NewProjectile(null, player.Center, v, 274, weaponDamage, item.knockBack + 10, player.whoAmI);
                //Projectile.NewProjectile(null, player.Center, v2, 274, weaponDamage, item.knockBack + 10, player.whoAmI);
                //Projectile.NewProjectile(null, player.Center, v3, 274, weaponDamage, item.knockBack + 10, player.whoAmI);
            }

            public override bool CanUseItem(Player player, Item item)
            {
                Main.NewText($"{player.altFunctionUse}");

                return true;
            }

            public override bool AltFunctionUse(Player player, Item item)
            {
                return true;
            }
        }

        public class MyItem2 : ExtenItem
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
                item.wingSlot = ExtenManag.GetEquipSlot<MyWing>();
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

        public class MyWing : ExtenEquip
        {
            public override EquipType EquipType => EquipType.Wings;
            public override string Texture => "Wings_1";
            protected Vector2 oldVelocity = Vector2.Zero;

            public override void SetStaticDefaults()
            {
                //飞行时间,速度,加速倍数,悬浮,悬浮水平速度,悬浮水平加速倍数
                ArmorIDs.Wing.Sets.Stats[Slot] = new WingStats(60 * 5, 16f, 8f, true, 16f * 2, 8f * 2);
            }

            public override void ApplyEquipFunctionalPostfix(Player player, int itemSlot, Item currentItem)
            {
                //ArmorIDs.Wing.Sets.Stats[Slot] = new WingStats(60 * 5, 16f, 8f, true, 16f * 2, 8f * 2);

                Effects(player);

                if (player.controlJump == false || player.controlDown == false) return;

                float num2 = 0.95f;
                float num5 = 0.15f;
                float num4 = 1f;
                float num3 = 4.5f;

                if (player.gravDir != 1f)
                {
                    if (player.velocity.Y > 0f)
                    {
                        player.velocity.Y -= num2;
                    }
                    else if (player.velocity.Y > (0f - Player.jumpSpeed) * num4)
                    {
                        player.velocity.Y -= num5;
                    }
                    if (player.velocity.Y < (0f - Player.jumpSpeed) * num3)
                    {
                        player.velocity.Y = (0f - Player.jumpSpeed) * num3;
                    }
                }
                else
                {
                    if (player.velocity.Y < 0f)
                    {
                        player.velocity.Y += num2;
                    }
                    else if (player.velocity.Y < Player.jumpSpeed * num4)
                    {
                        player.velocity.Y += num5;
                    }
                    if (player.velocity.Y > Player.jumpSpeed * num3)
                    {
                        player.velocity.Y = Player.jumpSpeed * num3;
                    }
                }
            }

            public override void ApplyEquipVanityPostfix(Player player, int itemSlot, Item currentItem)
            {
                Effects(player);
            }

            protected void Effects(Player player)
            {
                if (player != Main.LocalPlayer) return;
                if (player.mount.Active) return;

                Vector2 oldVelocity = this.oldVelocity;
                this.oldVelocity = player.velocity;

                if ((player.velocity.Length() > 16 && oldVelocity.Length() <= 16) != true) return;

                Vector2 vector = Vector2.Normalize(player.velocity);
                Vector2 pos = player.Center + vector * 100;
                vector *= -20f;
                ParticleOrchestraType type = (ParticleOrchestraType)76;

                for (int i = 0; i < 5; ++i)
                {
                    ParticleOrchestrator.BroadcastOrRequestParticleSpawn(type, new ParticleOrchestraSettings
                    {
                        PositionInWorld = pos,
                        MovementVector = vector,
                    });
                }

                SoundEngine.PlaySound(SoundID.Item14, player.position);
            }
        }
    }
}
