using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class HollowPoint : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 13;
                item.ranged = true;
                item.width = 8;
                item.height = 8;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 4f;
                item.value = 90;
                item.rare = 4;
                item.shoot = mod.ProjectileType("HollowPoint");
                item.shootSpeed = 4;
                item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Hollow Point Round");
      Tooltip.SetDefault("More effective against targets with low defense");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireTicket"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
