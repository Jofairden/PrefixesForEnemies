using Terraria.ModLoader;
using Terraria.ID;
namespace EnemyMods.Items.Turrets
{
    public class GunTurret : ModItem
    {
        public override void SetDefaults()
            {

                item.damage = 9;
                item.ranged = true;
                item.width = 12;
                item.height = 12;
                item.maxStack = 999;
                item.consumable = true;
                item.useStyle = 1;
                item.noUseGraphic = true;
                item.knockBack = 3f;
                item.value = 400;
                item.rare = 3;
                item.shoot = mod.ProjectileType("GunTurretCapsule");
                item.shootSpeed = 7f;
                item.noMelee = true;
            }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Gun Turret");
      Tooltip.SetDefault("");
    }

        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }*/
    }
