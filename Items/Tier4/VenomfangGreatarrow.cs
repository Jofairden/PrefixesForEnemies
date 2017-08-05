using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class VenomfangGreatarrow : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 20;
                item.ranged = true;
                item.width = 18;
                item.height = 48;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 3f;
                item.value = 1000;
                item.rare = 6;
                item.shoot = mod.ProjectileType("VenomfangGreatarrow");
                item.shootSpeed = -2f;
                item.ammo = AmmoID.Arrow;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Venomfang Greatarrow");
      Tooltip.SetDefault("Too heavy for normal bows to use effectively");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"));
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
