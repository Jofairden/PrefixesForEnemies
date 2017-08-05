using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier5
{
    public class AncientRapier : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 95;
            item.melee = true;
            item.width = 62;
            item.height = 62;
            item.noUseGraphic = true;
            item.noMelee = true;


            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTurn = true;
            item.knockBack = 2;
            item.value = 80000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("AncientRapier");
            item.scale = 1.1f;
            item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ancient Rapier");
      Tooltip.SetDefault("Right-click to counter. Partially ignores defense.\nCounterattacks deal 3x damage");
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
            if (player.FindBuffIndex(mod.BuffType("Counter")) >= 0)
            {
                damage = (int)(damage * 3);
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
                if (modPlayer.counterPlus)
                {
                    damage = (int)(damage * 1.2);
                }
                player.DelBuff(player.FindBuffIndex(mod.BuffType("Counter")));
                for (int i = 0; i < 20; i++)
                {
                    int d = Dust.NewDust(position, item.width, item.height, 87, speedX, speedY);
                }
                //lunge
                player.velocity.X = speedX * 2.6f;
                player.velocity.Y = speedY * 2.6f;
                player.immune = true;
                player.immuneTime = 60;
            }
            return true;
        }
        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noUseGraphic = false;
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
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
