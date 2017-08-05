using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    public class SimpleRapier : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.noUseGraphic = true;
            item.noMelee = true;


            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTurn = true;
            item.knockBack = 2;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("SimpleRapier");
            item.scale = 1.1f;
            item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Simple Rapier");
      Tooltip.SetDefault("Right-click to counter.\nCounterattacks deal 2x damage");
    }

        public override bool AltFunctionUse(Player player)
        {
            if (player.FindBuffIndex(mod.BuffType("CounterCooldown")) == -1)
            {
                return true;
            }
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
                int bonus = modPlayer.increasedCounterLength ? 15 : 5;
                player.AddBuff(mod.BuffType("CounterStanceRapier"), item.useAnimation + bonus);
                player.AddBuff(mod.BuffType("CounterCooldown"), 360);
                return false;
            }
            if (player.FindBuffIndex(mod.BuffType("Counter"))>=0)
            {
                damage *= 2;
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
                if (modPlayer.counterPlus)
                {
                    damage = (int)(damage * 1.2);
                }
                player.DelBuff(player.FindBuffIndex(mod.BuffType("Counter")));
            }
            return true;
        }
        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noUseGraphic = false;
                float backX = 0f;
                float downY = 0f;
                float cosRot = (float)Math.Cos(player.itemRotation);
                float sinRot = (float)Math.Sin(player.itemRotation);
                player.itemLocation.X = player.itemLocation.X - (backX * cosRot * player.direction) - (downY * sinRot * player.gravDir);
                player.itemLocation.Y = player.itemLocation.Y - (backX * sinRot * player.direction) + (downY * cosRot * player.gravDir);
            }
            else
            {
                item.noUseGraphic = true;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("AmethystTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
