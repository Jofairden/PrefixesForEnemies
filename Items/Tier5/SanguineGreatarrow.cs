using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class SanguineGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 13;
                item.ranged = true;
                item.width = 14;
                item.height = 48;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 3f;
                item.value = 1400;
                item.rare = 8;
                item.shoot = mod.ProjectileType("SanguineGreatarrow");
                item.shootSpeed = -2f;
                item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Sanguine Greatarrow");
      Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyTicket"));
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
