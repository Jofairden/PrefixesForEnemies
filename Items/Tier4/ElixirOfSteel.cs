using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class ElixirOfSteel : ModItem
    {
        public override void SetDefaults()
        {


            item.maxStack = 30;
            item.value = 6000;
            item.rare = 6;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.consumable = true;
            item.useTurn = true;
            item.width = 14;
            item.height = 24;
            item.UseSound = SoundID.Item3;
            item.buffType = mod.BuffType("ElixirOfSteel");
            item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Elixir of Steel");
      Tooltip.SetDefault("Gain 4 defense for 15 seconds when hit, stacking up to 20 defense");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
