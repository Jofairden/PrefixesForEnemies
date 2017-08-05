using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier2
{
    public class RazorwindRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 20;
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
      DisplayName.SetDefault("Razorwind Ring");
      Tooltip.SetDefault("Winds blow crystalline leaves through your foes. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[3] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X - 1600, player.Center.Y, 0, 0, mod.ProjectileType("Razorwind"), item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[3]--;
            if (play.cooldowns[3] == -1)
            {
                play.cooldowns[3] = play.maxCooldowns[3];
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
