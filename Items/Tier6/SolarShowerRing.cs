using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier6
{
    public class SolarShowerRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 90;
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
            item.UseSound = SoundID.Item43;
            item.autoReuse = false;
            item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Solar Shower Ring");
      Tooltip.SetDefault("Rains solar matter. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[17] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X, player.Center.Y - 900, 0, 0, mod.ProjectileType("SolarShowerSpawner"), item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[17]--;
            if (play.cooldowns[17] == -1)
            {
                play.cooldowns[17] = play.maxCooldowns[17];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("FireRainRing"), 1);
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
