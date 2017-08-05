using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier4
{
    public class Shadowflame : ModItem
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
                item.value = 120;
                item.rare = 6;
                item.shoot = mod.ProjectileType("Shadowflame");
                item.shootSpeed = 4;
                item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Shadowflame Round");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
