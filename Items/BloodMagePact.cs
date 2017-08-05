using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class BloodMagePact : ModItem
    {
        public override void SetDefaults()
        {

            item.accessory = true;
            item.width = 10;
            item.height = 10;
            item.rare = 2;

            item.value = 5000;
        }


    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood Mage's Pact");
      Tooltip.SetDefault("Provides a blood well.");
    }

        public override void UpdateEquip(Player player)
        {
            if(player.FindBuffIndex(mod.BuffType("BloodWell")) == -1)
            {
                //int p = Projectile.NewProjectile(player.position.X, player.position.Y, 0, 0, mod.ProjectileType("BloodWell"), 0, 0, player.whoAmI);
                Main.projectile[999] = new Projectile();
                Main.projectile[999].SetDefaults(mod.ProjectileType("BloodWell"));
                Projectile proj = Main.projectile[999];
                proj.owner = player.whoAmI;
                proj.damage = 0;
                proj.knockBack = 0;
                proj.position = player.position;
                proj.identity = 999;
                proj.velocity = Vector2.Zero;
                proj.gfxOffY = 0f;
                proj.stepSpeed = 1f;
                proj.wet = false;

            }
            player.AddBuff(mod.BuffType("BloodWell"), 2);
        }/*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AmethystTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
