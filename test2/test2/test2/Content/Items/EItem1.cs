using ExtenContent.Extens;
using ExtenContent.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using test2.Content.Projectiles;

namespace test2.Content.Items
{
    internal class EItem1 : ExtenItem
    {
        public override string Texture => "Terraria/Images/NPC_400";
        public override LocalizedText DisplayName => LanguageUtils.GetOrRegister($"{FullName}.{nameof(DisplayName)}", "刻意的眼球");
        public override LocalizedText Tooltip => LanguageUtils.GetOrRegister($"{FullName}.{nameof(Tooltip)}",
            "鼠标左键斩击\n" +
            "长按鼠标右键展开献祭领域\n" +
            "\"祂没有瞳孔\"");

        public override void SetStaticDefaults()
        {
            //为要在UI内绘制的项目类型注册动画（不是播放器上的世界或持有的项目）
            //要在世界中启用其动画，请使用ItemID.Sets.AnimatesAsSoul与此结合
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4, false));
            //如果为true则该item将在物品栏和世界中设置动画
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;//有右键单击功能时,允许按住鼠标右键重复使用
        }

        public override void SetDefault(Item item)
        {
            item.width = 78;
            item.height = 120;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.useTime = 16;
            item.noMelee = true;//item的使用动画能否造成伤害
            item.noUseGraphic = true;//item的使用动画是否显示
            item.autoReuse = true;//是否长按连续使用
            item.useTurn = true;//item的使用动画发生时,玩家能否转身
            item.UseSound = SoundID.Item1;
            item.damage = 114;
            item.knockBack = 6f;
            item.shoot = ExtenManag.ProjectileType<EProj1>();
            item.shootSpeed = 1f;
            item.rare = 11;
            item.value = Item.sellPrice(0, 70, 0, 0);
        }

        public override bool CanUseItem(Player player, Item item)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 8;
                item.useAnimation = 90;
                item.useTime = 90;
                item.shoot = ExtenManag.ProjectileType<EProj3>();
                item.shootSpeed = 1f;
            }
            else
            {
                item.useStyle = 1;
                item.useAnimation = 16;
                item.useTime = 16;
                item.shoot = ExtenManag.ProjectileType<EProj1>();
                item.shootSpeed = 1f;
            }

            return true;
        }

        public override bool Shoot(Player player, Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ExtenManag.ProjectileType<EProj1>())
            {
                if (velocity == Vector2.Zero) velocity = Vector2.UnitX;

                float dir = velocity.X > 0 ? 1 : -1;
                velocity = (-Vector2.UnitY).RotatedBy(MathHelper.TwoPi / 360f * 30f * -dir);
                velocity = Vector2.Normalize(velocity) * 16f;

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI,
                    dir);
            }
            else if (type == ExtenManag.ProjectileType<EProj3>())
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override bool AltFunctionUse(Player player, Item item) => true;
    }
}
