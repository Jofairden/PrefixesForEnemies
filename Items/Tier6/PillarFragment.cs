using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class PillarFragment : ModItem
    {
        public override void SetDefaults()
        {

                item.damage = 17;
                item.ranged = true;
                item.width = 8;
                item.height = 8;
                item.maxStack = 999;

                item.consumable = true;
                item.knockBack = 3f;
                item.value = 200;
                item.rare = 10;
                item.shoot = mod.ProjectileType("PillarFragment");
                item.shootSpeed = 4;
                item.ammo = AmmoID.Bullet;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Pillar Fragment Round");
      Tooltip.SetDefault("Splits into 2-3 pillar fragments midair");
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmberTicket"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
        }
    }
}
