using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    public class Azure : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 9;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;

            item.consumable = true;
            item.knockBack = 2f;
            item.value = 70;
            item.rare = 3;
            item.shoot = mod.ProjectileType("Azure");
            item.shootSpeed = 5;
            item.ammo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Azure Round");
      Tooltip.SetDefault("Explodes if it hits a tile");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TopazTicket"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
