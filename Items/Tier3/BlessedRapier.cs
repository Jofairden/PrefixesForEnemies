using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier3
{
    public class BlessedRapier : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 39;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.noUseGraphic = true;
            item.noMelee = true;


            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTurn = true;
            item.knockBack = 2;
            item.value = 30000;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("BlessedRapier");
            item.scale = 1.1f;
            item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blessed Rapier");
      Tooltip.SetDefault("Right-click to counter.\nCounterattacks deal 2.5x damage");
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
                damage = (int)(damage * 2.5);
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
                if (modPlayer.counterPlus)
                {
                    damage = (int)(damage * 1.2);
                }
                player.DelBuff(player.FindBuffIndex(mod.BuffType("Counter")));
                for(int i=0; i<20; i++)
                {
                    int d = Dust.NewDust(position, item.width, item.height, 73, speedX, speedY);
                }
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
            recipe.AddIngredient(mod.ItemType("SapphireTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
