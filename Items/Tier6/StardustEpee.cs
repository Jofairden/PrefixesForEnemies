using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier6
{
    public class StardustEpee : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 180;
            item.melee = true;
            item.width = 42;
            item.height = 42;
            item.noUseGraphic = true;
            item.noMelee = true;


            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.useTurn = true;
            item.knockBack = 2;
            item.value = 100000;
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("StardustEpee");
            item.scale = 1.1f;
            item.shootSpeed = 5f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Stardust Épeé");
      Tooltip.SetDefault("Right-click to counter.\nCountering reflects projectiles");
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
                player.AddBuff(mod.BuffType("CounterStanceEpee3"), item.useAnimation + bonus);
                player.AddBuff(mod.BuffType("CounterCooldown"), 360);
                return false;
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
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
