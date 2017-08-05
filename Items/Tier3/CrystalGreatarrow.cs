using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class CrystalGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 14;
                item.ranged = true;
                item.width = 14;
                item.height = 48;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 3f;
                item.value = 600;
                item.rare = 4;
                item.shoot = mod.ProjectileType("CrystalGreatarrow");
                item.shootSpeed = -2f;
                item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Crystal Greatarrow");
      Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireTicket"));
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
