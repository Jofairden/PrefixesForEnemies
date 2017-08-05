using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier4
{
    public class GunbladeMkIII : ModItem
    {
        int shotBonus = 0;
        public override void SetDefaults()
        {

            item.damage = 58;
            item.melee = true;
            item.width = 54;
            item.height = 59;

            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = 10;
            item.scale = 1.1f;
            item.shootSpeed = 10f;
            Item.staff[item.type] = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Gunblade MkIII");
      Tooltip.SetDefault("Right-click to shoot.");
    }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                damage += -8 + shotBonus * item.damage / 4;
                player.AddBuff(mod.BuffType("GunbladeDefense"), shotBonus * 40);
                shotBonus = 0;
                return true;
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
                float backX = 18f;
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
                item.useStyle = 5;
                item.UseSound = SoundID.Item11;
                //add defense buff based on shotBonus consumed
                item.melee = false;
                item.ranged = true;
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
            if (shotBonus > 10)
            {
                shotBonus = 10;
                Main.PlaySound(12, player.position);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GunbladeMkII"), 1);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
