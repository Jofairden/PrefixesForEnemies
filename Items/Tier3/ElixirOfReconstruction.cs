using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class ElixirOfReconstruction : ModItem
    {
        public override void SetDefaults()
        {


            item.maxStack = 30;
            item.value = 4000;
            item.rare = 4;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.consumable = true;
            item.useTurn = true;
            item.width = 14;
            item.height = 24;
            item.UseSound = SoundID.Item3;
            item.buffType = mod.BuffType("ElixirOfReconstruction");
            item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Elixir of Reconstruction");
      Tooltip.SetDefault("Heal 25% of the last damage you took over 10 seconds");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireTicket"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
