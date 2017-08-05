using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier4
{
    public class LightSpearRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 55;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 50000;
            item.rare = 6;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Light Spear Ring");
      Tooltip.SetDefault("Summons a ring of holy spears. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[8] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X + 300, Main.MouseWorld.Y, -.001f, 0, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int q = Projectile.NewProjectile(Main.MouseWorld.X - 300, Main.MouseWorld.Y, .001f, 0, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int w = Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y + 300, 0, -.001f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int e = Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y - 300, 0, .001f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int r = Projectile.NewProjectile(Main.MouseWorld.X + 212, Main.MouseWorld.Y + 212, -.000707f, -.000707f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int t = Projectile.NewProjectile(Main.MouseWorld.X - 212, Main.MouseWorld.Y + 212, .000707f, -.000707f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int y = Projectile.NewProjectile(Main.MouseWorld.X + 212, Main.MouseWorld.Y - 212, -.000707f, .000707f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int u = Projectile.NewProjectile(Main.MouseWorld.X - 212, Main.MouseWorld.Y - 212, .000707f, .000707f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);

            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[8]--;
            if (play.cooldowns[8] == -1)
            {
                play.cooldowns[8] = play.maxCooldowns[8];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
