using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class Orion : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 36;
            item.height = 52;
            item.channel = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.damage = 94;
            item.value = 100000;
            item.rare = 10;
            item.knockBack = 2;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.noMelee = true;
            item.shootSpeed = 8f;
            item.ranged = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Orion");
      Tooltip.SetDefault("");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projIndex = Projectile.NewProjectile(position.X - speedX, position.Y - speedY, speedX, speedY, type, -player.whoAmI - 256, knockBack, player.whoAmI);

            float arrowSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            //damage is arrow index
            //knockback is arrow speed
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("Orion"), damage, arrowSpeed, player.whoAmI);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
