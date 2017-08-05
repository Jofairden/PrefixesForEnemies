using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier5
{
    public class GunbladeSpreadMkIII : ModItem
    {
        int shotBonus = 0;
        int reload = 0;
        public override void SetDefaults()
        {

            item.damage = 85;
            item.melee = true;
            item.width = 50;
            item.height = 62;

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 80000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = 10;
            item.scale = 1.2f;
            item.shootSpeed = 10f;
            Item.staff[item.type] = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Gunblade: Spread MkIII");
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
                for (int i = 0; i < 4 + shotBonus / 3; i++)
                {
                    float vX = speedX + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;
                    float vY = speedY + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;

                    Projectile.NewProjectile(position.X, position.Y, vX, vY, type, (int)(damage * .8 + (15 * shotBonus * (player.meleeDamage - 1))), knockBack, Main.myPlayer);
                }
                shotBonus = 0;
            }
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            if (reload > 0)
                reload--;
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
        public override bool ConsumeAmmo(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                return true;
            }
            return false;
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
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            shotBonus++;
            if (crit)
            {
                shotBonus++;
            }
            if (shotBonus > 18)
            {
                shotBonus = 18;
                Main.PlaySound(12, player.position);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GunbladeSpreadMkII"), 1);
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
