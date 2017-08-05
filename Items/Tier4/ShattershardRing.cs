using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier4
{
    public class ShattershardRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 40;
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
      DisplayName.SetDefault("Shattershard Ring");
      Tooltip.SetDefault("Summons a flurry of polar fragments. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[10] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(player.Center.X, player.Center.Y - 50, 0, 0, mod.ProjectileType("ShattershardSpawner"), item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[10]--;
            if (play.cooldowns[10] == -1)
            {
                play.cooldowns[10] = play.maxCooldowns[10];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("IceShardRing"), 1);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
