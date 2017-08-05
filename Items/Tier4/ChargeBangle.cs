using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class ChargeBangle : ModItem
    {
        public override void SetDefaults()
        {

            item.accessory = true;
            item.rare = 6;
            item.width = 10;
            item.height = 10;

            item.value = 50000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Charge Bangle");
      Tooltip.SetDefault("Adds an extra charge to rings with 2 or more base charges");
    }

        public override void UpdateEquip(Player player)
        {

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
