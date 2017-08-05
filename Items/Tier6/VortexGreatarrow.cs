using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class VortexGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 28;
                item.ranged = true;
                item.width = 14;
                item.height = 48;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 3f;
                item.value = 2000;
                item.rare = 10;
                item.shoot = mod.ProjectileType("VortexGreatarrow");
                item.shootSpeed = -2f;
                item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Vortex Greatarrow");
      Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmberTicket"));
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
