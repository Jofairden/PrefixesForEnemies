using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier4
{
    public class ShadowflameRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 95;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
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
      DisplayName.SetDefault("Shadowflame Ring");
      Tooltip.SetDefault("Summons a dark portal. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[9] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0, 0, mod.ProjectileType("ShadowflamePortal"), item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[9]--;
            if (play.cooldowns[9] == -1)
            {
                play.cooldowns[9] = play.maxCooldowns[9];
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
