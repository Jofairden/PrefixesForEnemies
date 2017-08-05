using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class EarthenDraught : ModItem
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
            item.buffType = mod.BuffType("EarthenDraught");
            item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Earthen Draught");
      Tooltip.SetDefault("Take 15% less damage while on the ground");
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
