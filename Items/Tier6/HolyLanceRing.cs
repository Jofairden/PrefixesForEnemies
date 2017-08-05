using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier6
{
    public class HolyLanceRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 150;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 10;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Holy Lance Ring");
      Tooltip.SetDefault("Bring down angelic lances. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[15] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X + 304, Main.MouseWorld.Y, -.001f, 0, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int q = Projectile.NewProjectile(Main.MouseWorld.X - 296, Main.MouseWorld.Y, .001f, 0, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int e = Projectile.NewProjectile(Main.MouseWorld.X + 12, Main.MouseWorld.Y - 300, 0, .001f, mod.ProjectileType("BigHolyLance"), item.damage*3, item.knockBack, item.owner);
            int y = Projectile.NewProjectile(Main.MouseWorld.X + 216, Main.MouseWorld.Y - 212, -.000707f, .000707f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);
            int u = Projectile.NewProjectile(Main.MouseWorld.X - 208, Main.MouseWorld.Y - 212, .000707f, .000707f, mod.ProjectileType("HolyLance"), item.damage, item.knockBack, item.owner);

            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[15]--;
            if (play.cooldowns[15] == -1)
            {
                play.cooldowns[15] = play.maxCooldowns[15];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LightSpearRing"), 1);
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
