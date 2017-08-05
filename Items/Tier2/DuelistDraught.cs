using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    public class DuelistDraught : ModItem
    {
        public override void SetDefaults()
        {


            item.maxStack = 30;
            item.value = 2000;
            item.rare = 3;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.consumable = true;
            item.useTurn = true;
            item.width = 14;
            item.height = 24;
            item.UseSound = SoundID.Item3;
            item.buffType = mod.BuffType("DuelistDraught");
            item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Duelist Draught");
      Tooltip.SetDefault("Deal more and take less damage to the last enemy hit");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TopazTicket"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
