using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class PrimordialGreatbow : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 12;
            item.height = 42;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.damage = 38;
            item.rare = 6;
            item.value = 50000;
            item.knockBack = 2;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.noMelee = true;
            item.shootSpeed = 6.8f;
            item.ranged = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Primordial Greatbow");
      Tooltip.SetDefault("");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projIndex = Projectile.NewProjectile(position.X - speedX, position.Y - speedY, speedX, speedY, type, -player.whoAmI - 256, knockBack, player.whoAmI);

            float arrowSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //damage is arrow index
            //knockback is arrow speed
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("PrimordialGreatbow"), damage, arrowSpeed, player.whoAmI);
            return false;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }*/
    }
}
