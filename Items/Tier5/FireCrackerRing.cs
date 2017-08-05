using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class FireCrackerRing : ModItem
    {
        int maxCharges = 2;
        int rechargeTime = 120;
        int charges = 2;
        int rechargeCount = 0;

        //conditional charge modifiers go here

        public override void SetDefaults()
        {

            item.damage = 50;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 80000;
            item.rare = 8;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("Firecracker");
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Firecracker Ring");
      Tooltip.SetDefault("Shoots an unpredictable explosive blast. Two charges.");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, item.damage, item.knockBack, item.owner);
            Main.projectile[p].ai[1] = (float)Main.rand.Next(-20, 21) / 200;
            Main.projectile[p].ai[2] = (float)Main.rand.Next(-20, 21) / 200;
            charges--;
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (charges <= 0)
            {
                return false;
            }
            else return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (((MPlayer)player.GetModPlayer(mod, "MPlayer")).chargeBangle)
            {
                maxCharges = 3;
            }
            if (charges < maxCharges)
            {
                rechargeCount++;
                if (((MPlayer)player.GetModPlayer(mod, "MPlayer")).embellishedRegen && Main.rand.Next(0, 2) == 0)
                {
                    rechargeCount++;
                }
                if (rechargeCount >= rechargeTime)
                {
                    charges++;//consider combat text or other alert
                    rechargeCount = 0;
                }
            }
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("FireCrackerRing"), 1);
            recipe2.AddIngredient(mod.ItemType("RubyTicket"), 2);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }*/
    }
}
