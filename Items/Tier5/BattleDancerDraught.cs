using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class BattleDancerDraught : ModItem
    {
        public override void SetDefaults()
        {



            item.maxStack = 30;
            item.value = 8000;
            item.rare = 8;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.consumable = true;
            item.useTurn = true;
            item.width = 14;
            item.height = 24;
            item.UseSound = SoundID.Item3;
            item.buffType = mod.BuffType("BattleDancerDraught");
            item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Battle Dancer Draught");
      Tooltip.SetDefault("Gain up to 20% damage by hitting enemies, but reset to 0% when hit\nGain Hunter effect when fully stacked");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyTicket"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
