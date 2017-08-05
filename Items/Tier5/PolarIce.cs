using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class PolarIce : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 16;
                item.ranged = true;
                item.width = 8;
                item.height = 8;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 3f;
                item.value = 150;
                item.rare = 8;
                item.shoot = mod.ProjectileType("PolarIce");
                item.shootSpeed = 4;
                item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Polar Ice Cap");
      Tooltip.SetDefault("Breaks into ice shards on impact");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyTicket"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
