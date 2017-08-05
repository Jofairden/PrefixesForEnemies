using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier6
{
    public class GunbladeSpreadMkIV : ModItem
    {
        int shotBonus = 0;
        int reload = 0;
        public override void SetDefaults()
        {

            item.damage = 178;
            item.melee = true;
            item.width = 54;
            item.height = 56;

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 100000;
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = 10;
            item.shootSpeed = 10f;
            item.scale = 1.2f;
            Item.staff[item.type] = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Gunblade: Spread MkIV");
      Tooltip.SetDefault("Right-click to shoot.");
    }

        public override bool AltFunctionUse(Player player)
        {
            if (reload == 0)
                return true;
            else return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 5 + shotBonus / 3; i++)
                {
                    float vX = speedX + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;
                    float vY = speedY + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;

                    Projectile.NewProjectile(position.X, position.Y, vX, vY, type, (int)(damage * .8 + (25 * shotBonus * (player.meleeDamage - 1))), knockBack, Main.myPlayer);
                }
                shotBonus = 0;
            }
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            return false;
        }
        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                float backX = 12f;
                float downY = 0f;
                float cosRot = (float)Math.Cos(player.itemRotation);
                float sinRot = (float)Math.Sin(player.itemRotation);
                player.itemLocation.X = player.itemLocation.X - (backX * cosRot * player.direction) - (downY * sinRot * player.gravDir);
                player.itemLocation.Y = player.itemLocation.Y - (backX * sinRot * player.direction) + (downY * cosRot * player.gravDir);
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (item.useAmmo == 0)
            {
                item.useAmmo = AmmoID.Bullet;
                item.shoot = 10;
            }
            if (!player.HasAmmo(item, true))
            {
                item.useAmmo = 0;
                item.shoot = 0;
            }
            if (player.altFunctionUse == 2 && item.useAmmo == AmmoID.Bullet)
            {
                item.melee = false;
                item.ranged = true;
                item.useStyle = 5;
                item.UseSound = SoundID.Item36;
                reload = 100;
                item.noMelee = true;
            }
            else
            {
                item.useStyle = 1;
                item.UseSound = SoundID.Item1;
                item.ranged = false;
                item.melee = true;
                item.noMelee = false;
            }
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (reload > 0)
                reload--;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            shotBonus++;
            if (crit)
            {
                shotBonus++;
            }
            if (shotBonus > 27)
            {
                shotBonus = 27;
                Main.PlaySound(12, player.position);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GunbladeSpreadMkIII"), 1);
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
