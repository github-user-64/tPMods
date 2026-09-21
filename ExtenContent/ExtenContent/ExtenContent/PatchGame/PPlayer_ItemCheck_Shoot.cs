using ExtenContent.Extens;
using HarmonyLib;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Testing;

namespace ExtenContent.PatchGame
{
    internal partial class PPlayer
    {
        [HarmonyPatch("ItemCheck_Shoot")]
        [HarmonyPrefix]
        private static bool ItemCheck_ShootPrefix(Player __instance, int i, Item sItem, int weaponDamage, bool withAudioVisualFeedback)
        {
            Player player = __instance;
            ExtenItem ei = ExtenManag.GetExtenItem(sItem.type);
            if (ei == null) return true;

            int projToShoot = sItem.shoot;
            float speed = sItem.shootSpeed;
            int damage = sItem.damage;
            if (sItem.melee && !ProjectileID.Sets.NoMeleeSpeedVelocityScaling[projToShoot])
            {
                speed /= player.meleeSpeed;
            }
            bool canShoot = false;
            int Damage = weaponDamage;
            float KnockBack = sItem.knockBack;
            int usedAmmoItemId = 0;
            if (sItem.useAmmo > 0)
            {
                player.PickAmmo(sItem, ref projToShoot, ref speed, ref canShoot, ref Damage, ref KnockBack, out usedAmmoItemId, ItemID.Sets.gunProj[sItem.type]);
            }
            else
            {
                canShoot = true;
            }
            if (ItemID.Sets.gunProj[sItem.type])
            {
                KnockBack = sItem.knockBack;
                Damage = weaponDamage;
                speed = sItem.shootSpeed;
            }
            if (ProjectileID.Sets.IsAPhaseblade[sItem.shoot] && sItem.type != 671)
            {
                KnockBack *= 1.25f;
                switch (sItem.shoot)
                {
                    default:
                        Damage = (int)((double)Damage * 1.25);
                        break;
                    case 1065:
                    case 1066:
                    case 1067:
                    case 1068:
                    case 1069:
                    case 1070:
                    case 1072:
                    case 1076:
                        Damage = (int)((double)Damage * 1.5);
                        break;
                }
            }
            if (sItem.IsACoin)
            {
                canShoot = false;
            }
            if (sItem.type == 1254 && projToShoot == 14)
            {
                projToShoot = 242;
            }
            if (sItem.type == 1255 && projToShoot == 14)
            {
                projToShoot = 242;
            }
            if (sItem.type == 1265 && projToShoot == 14)
            {
                projToShoot = 242;
            }
            if (sItem.type == 3542)
            {
                if (Main.rand.Next(100) < 20)
                {
                    projToShoot++;
                    Damage *= 3;
                }
                else
                {
                    speed -= 1f;
                }
            }
            if (sItem.type == 1928)
            {
                Damage = (int)((float)Damage * 1f);
            }
            if (sItem.type == 3063)
            {
                Damage = (int)((float)Damage * 1.25f);
            }
            if (sItem.type == 1306)
            {
                Damage = (int)((double)Damage * 0.67);
            }
            if (sItem.type == 1227)
            {
                Damage = (int)((double)Damage * 0.7);
            }
            if (!canShoot)
            {
                return false;
            }
            KnockBack = player.GetWeaponKnockback(sItem, KnockBack);
            IEntitySource projectileSource_Item_WithPotentialAmmo = player.GetProjectileSource_Item_WithPotentialAmmo(sItem, usedAmmoItemId);
            if (projToShoot == 228)
            {
                KnockBack = 0f;
            }
            if (projToShoot == 1 && sItem.type == 120)
            {
                projToShoot = 2;
            }
            if (sItem.type == 682)
            {
                projToShoot = 117;
            }
            if (sItem.type == 725)
            {
                projToShoot = 120;
            }
            if (sItem.type == 2796)
            {
                projToShoot = 442;
            }
            if (sItem.type == 2223)
            {
                projToShoot = 357;
            }
            if (sItem.type == 5117)
            {
                projToShoot = 968;
            }
            if (sItem.fishingPole > 0 && player.overrideFishingBobber > -1)
            {
                projToShoot = player.overrideFishingBobber;
            }
            if (withAudioVisualFeedback)
            {
                if (DebugOptions.ManaV2 && sItem.mana > 0 && player.slowMagicUse)
                {
                    float slowMagicMultiplier = GetSlowMagicMultiplier();
                    player.ApplyItemTime(sItem, slowMagicMultiplier);
                }
                else
                {
                    player.ApplyItemTime(sItem);
                }
            }
            Vector2 mountedCenter = player.MountedCenter;
            Vector2 pointPosition = player.RotatedRelativePoint(mountedCenter);
            bool flag = true;
            int type = sItem.type;
            if (type == 3611)
            {
                flag = false;
            }
            Vector2 value = Vector2.UnitX.RotatedBy(player.fullRotation);
            Vector2 vector = Main.MouseWorld - pointPosition;
            Vector2 v = player.itemRotation.ToRotationVector2() * player.direction;
            if (sItem.type == 3852 && !player.ItemAnimationJustStarted)
            {
                vector = (v.ToRotation() + player.fullRotation).ToRotationVector2();
            }
            if (vector != Vector2.Zero)
            {
                vector.Normalize();
            }
            float num = Vector2.Dot(value, vector);
            if (flag)
            {
                if (num > 0f)
                {
                    player.ChangeDir(1);
                }
                else
                {
                    player.ChangeDir(-1);
                }
            }
            if (sItem.type == 3094 || sItem.type == 3378 || sItem.type == 3543)
            {
                pointPosition.Y = player.position.Y + (float)(player.height / 3);
            }
            if (sItem.type == 5117)
            {
                pointPosition.Y = player.position.Y + (float)(player.height / 3);
            }
            if (sItem.type == 517)
            {
                pointPosition.X += (float)Main.rand.Next(-3, 4) * 3.5f;
                pointPosition.Y += (float)Main.rand.Next(-3, 4) * 3.5f;
            }
            if (sItem.type == 2611 || sItem.type == 5526)
            {
                Vector2 vector2 = vector;
                if (vector2 != Vector2.Zero)
                {
                    vector2.Normalize();
                }
                pointPosition += vector2;
            }
            if (sItem.type == 3827)
            {
                pointPosition += vector.SafeNormalize(Vector2.Zero).RotatedBy((float)player.direction * (-(float)Math.PI / 2f)) * 24f;
            }
            if (projToShoot == 9)
            {
                float num2 = (float)Main.mouseX + Main.screenPosition.X;
                int num3 = -1;
                if (num2 < player.Left.X)
                {
                    num3 = 1;
                }
                else if (num2 <= player.Right.X && Main.rand.Next(2) == 0)
                {
                    num3 = 1;
                }
                pointPosition = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * num3) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                KnockBack = 0f;
                Damage = (int)((float)Damage * 1.5f);
            }
            if (sItem.type == 986 || sItem.type == 281)
            {
                pointPosition.X += 6 * player.direction;
                pointPosition.Y -= 6f * player.gravDir;
            }
            if (sItem.type == 3007)
            {
                pointPosition.X -= 4 * player.direction;
                pointPosition.Y -= 2f * player.gravDir;
            }
            float num4 = (float)Main.mouseX + Main.screenPosition.X - pointPosition.X;
            float num5 = (float)Main.mouseY + Main.screenPosition.Y - pointPosition.Y;
            if (sItem.type == 3852 && !player.ItemAnimationJustStarted)
            {
                Vector2 vector3 = vector;
                num4 = vector3.X;
                num5 = vector3.Y;
            }
            if (player.gravDir == -1f)
            {
                num5 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - pointPosition.Y;
            }
            float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
            float num7 = num6;
            if ((float.IsNaN(num4) && float.IsNaN(num5)) || (num4 == 0f && num5 == 0f))
            {
                num4 = player.direction;
                num5 = 0f;
                num6 = speed;
            }
            else
            {
                num6 = speed / num6;
            }
            if (sItem.type == 1929 || sItem.type == 2270)
            {
                num4 += (float)Main.rand.Next(-50, 51) * 0.03f / num6;
                num5 += (float)Main.rand.Next(-50, 51) * 0.03f / num6;
            }
            num4 *= num6;
            num5 *= num6;
            if (projToShoot == 250)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (Main.projectile[j].active && Main.projectile[j].owner == player.whoAmI && (Main.projectile[j].type == 250 || Main.projectile[j].type == 251))
                    {
                        Main.projectile[j].Kill();
                    }
                }
            }
            if (projToShoot == 12 && Collision.CanHitLine(player.Center, 0, 0, pointPosition + new Vector2(num4, num5) * 4f, 0, 0))
            {
                pointPosition += new Vector2(num4, num5) * 3f;
            }
            if (projToShoot == 728 && !Collision.CanHitLine(player.Center, 0, 0, pointPosition + new Vector2(num4, num5) * 2f, 0, 0))
            {
                Vector2 value2 = new Vector2(num4, num5) * 0.25f;
                pointPosition = player.Center - value2;
            }
            if (projToShoot == 85)
            {
                pointPosition += new Vector2(0f, -6f * (float)player.direction * player.Directions.Y).RotatedBy(vector.ToRotation());
                if (Collision.CanHitLine(pointPosition, 0, 0, pointPosition + new Vector2(num4, num5) * 5f, 0, 0))
                {
                    pointPosition += new Vector2(num4, num5) * 4f;
                }
            }
            if (projToShoot == 802 || projToShoot == 842 || projToShoot == 1127)
            {
                Vector2 v2 = new Vector2(num4, num5);
                float num8 = (float)Math.PI / 4f;
                float num9 = 0.7f;
                if (projToShoot == 1127)
                {
                    num9 = 0.5f;
                    num8 = 0.65f * Terraria.Utils.Remap(player.meleeSpeed, 1f, 0.333333343f, 1f, 0.25f);
                }
                Vector2 vector4 = v2.SafeNormalize(Vector2.Zero).RotatedBy(num8 * (Main.rand.NextFloat() - 0.5f)) * (v2.Length() - Main.rand.NextFloatDirection() * num9);
                num4 = vector4.X;
                num5 = vector4.Y;
            }

            if (sItem.useStyle == 5)
            {
                if (sItem.type == 3029)
                {
                    Vector2 vector5 = new Vector2(num4, num5);
                    vector5.X = (float)Main.mouseX + Main.screenPosition.X - pointPosition.X;
                    vector5.Y = (float)Main.mouseY + Main.screenPosition.Y - pointPosition.Y - 1000f;
                    player.itemRotation = (float)Math.Atan2(vector5.Y * (float)player.direction, vector5.X * (float)player.direction);
                }
                else if (sItem.type == 4381)
                {
                    Vector2 vector6 = new Vector2(num4, num5);
                    vector6.X = (float)Main.mouseX + Main.screenPosition.X - pointPosition.X;
                    vector6.Y = (float)Main.mouseY + Main.screenPosition.Y - pointPosition.Y - 1000f;
                    player.itemRotation = (float)Math.Atan2(vector6.Y * (float)player.direction, vector6.X * (float)player.direction);
                }
                else if (sItem.type == 3779)
                {
                    player.itemRotation = 0f;
                }
                else
                {
                    player.itemRotation = (float)Math.Atan2(num5 * (float)player.direction, num4 * (float)player.direction) - player.fullRotation;
                }
                NetMessage.SendData(13, -1, -1, null, player.whoAmI);
                NetMessage.SendData(41, -1, -1, null, player.whoAmI);
            }
            if (sItem.useStyle == 13)
            {
                player.itemRotation = (float)Math.Atan2(num5 * (float)player.direction, num4 * (float)player.direction) - player.fullRotation;
                NetMessage.SendData(13, -1, -1, null, player.whoAmI);
                NetMessage.SendData(41, -1, -1, null, player.whoAmI);
            }

            return ItemLoad.ItemCheck_Shoot(player, ei, sItem,
                (EntitySource_ItemUse_WithAmmo)projectileSource_Item_WithPotentialAmmo,
                pointPosition, new Vector2(num4, num5), projToShoot, Damage, KnockBack);
        }

        private static float GetSlowMagicMultiplier()
        {
            return 2.5f;
        }
    }
}
