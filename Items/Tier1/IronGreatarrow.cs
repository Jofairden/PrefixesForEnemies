using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    public class IronGreatarrow : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 14;
            item.height = 48;
            item.maxStack = 999;

            item.consumable = true;
            item.knockBack = 3f;
            item.value = 200;
            item.rare = 2;
            item.shoot = mod.ProjectileType("IronGreatarrow");
            item.shootSpeed = -2f;
            item.ammo = AmmoID.Arrow;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Greatarrow");
            Tooltip.SetDefault("Too heavy for normal bows to use effectively");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmethystTicket"));
            recipe.SetResult(this, 100);
            recipe.AddRecipe();
        }
    }
}
