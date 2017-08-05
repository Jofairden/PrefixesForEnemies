using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    public class BlazingEstoc : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 23;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.noUseGraphic = true;
            item.noMelee = true;


            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTurn = true;
            item.knockBack = 2;
            item.value = 20000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("BlazingEstoc");
            item.scale = 1.1f;
            item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blazing Estoc");
      Tooltip.SetDefault("Right-click to counter.\nCounterattacks spew fire");
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
                player.AddBuff(mod.BuffType("CounterStanceEstoc"), item.useAnimation + bonus);
                player.AddBuff(mod.BuffType("CounterCooldown"), 360);
                return false;
            }
            if (player.FindBuffIndex(mod.BuffType("Counter")) >= 0)
            {
                int p = Projectile.NewProjectile(position.X, position.Y, speedX*1.5f, speedY*1.5f, 376, damage*2, knockBack*2, player.whoAmI);
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
                if (modPlayer.counterPlus)
                {
                    Main.projectile[p].damage = (int)(Main.projectile[p].damage * 1.2);
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
            recipe.AddIngredient(mod.ItemType("TopazTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
