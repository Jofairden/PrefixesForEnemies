using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier1
{
    public class Splitter : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 4;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;

            item.consumable = true;
            item.knockBack = 2f;
            item.value = 50;
            item.rare = 2;
            item.shoot = mod.ProjectileType("Splitter");
            item.shootSpeed = 4;
            item.ammo = AmmoID.Bullet;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Splitter Round");
      Tooltip.SetDefault("Splits into 2 bullets");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmethystTicket"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
