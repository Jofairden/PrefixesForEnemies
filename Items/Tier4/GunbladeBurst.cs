using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EnemyMods.NPCs;

namespace EnemyMods.Items.Tier4
{
    public class GunbladeBurst : ModItem
    {
        int shotBonus = 0;
        int DoT = 0;
        int shots = 0;
        public override void SetDefaults()
        {

            item.damage = 58;
            item.melee = true;
            item.width = 48;
            item.height = 58;

            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 50000;
            item.rare = 6;
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
      DisplayName.SetDefault("Gunblade: Burst");
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
                Main.PlaySound(2, player.position, 11);
                damage += -20 + shotBonus * item.damage / 8;
                shots++;
                if(DoT<15)
                    DoT++;
                if (shots >= 3)
                {
                    shotBonus = 0;
                    shots = 0;
                }
                return true;
            }
            return false;
        }
        public override bool ConsumeAmmo(Player player)
        {
            if (player.altFunctionUse == 2 && shots == 0)
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
                item.useTime = 8;
                item.reuseDelay = 30;
                item.useStyle = 5;
                item.UseSound = SoundID.Item11;
                item.noMelee = true;
            }
            else
            {
                item.useTime = 24;
                item.useStyle = 1;
                item.UseSound = SoundID.Item1;
                item.ranged = false;
                item.melee = true;
                item.noMelee = false;
            }
            return true;
        }
        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2 || item.useStyle == 5)
            {
                float backX = 12f;
                float downY = 0f;
                float cosRot = (float)Math.Cos(player.itemRotation);
                float sinRot = (float)Math.Sin(player.itemRotation);
                player.itemLocation.X = player.itemLocation.X - (backX * cosRot * player.direction) - (downY * sinRot * player.gravDir);
                player.itemLocation.Y = player.itemLocation.Y - (backX * sinRot * player.direction) + (downY * cosRot * player.gravDir);
            }
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
            if(DoT > 0)
            {
                gNPC info = target.GetGlobalNPC<gNPC>();
                info.gunbladeBurn += DoT * 40;
                DoT = 0;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
