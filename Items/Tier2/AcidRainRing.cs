using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier2
{
    public class AcidRainRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 17;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 20000;
            item.rare = 3;
            item.UseSound = SoundID.Item43;
            item.autoReuse = false;
            item.shootSpeed = 0f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Acid Rain Ring");
      Tooltip.SetDefault("Rains toxic fluid. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[2] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X - 100, player.Center.Y - 900, 0, 0, mod.ProjectileType("AcidRainSpawner"), item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[2]--;
            if (play.cooldowns[2] == -1)
            {
                play.cooldowns[2] = play.maxCooldowns[2];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("TopazTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
