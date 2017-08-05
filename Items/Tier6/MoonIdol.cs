using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class MoonIdol : ModItem
    {
        int cooldown = 0;
        public override void SetDefaults()
        {

            item.accessory = true;
            item.width = 10;
            item.height = 10;
            item.rare = 10;

            item.value = 100000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Idol of the Moon God");
      Tooltip.SetDefault("Critical hits have a 20% chance to deal an additional 2x damage");
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
