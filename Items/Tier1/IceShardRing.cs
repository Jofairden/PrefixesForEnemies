using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier1
{
    public class IceShardRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ice Shard Ring");
      Tooltip.SetDefault("Shoots a lot of icicles. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[0] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(player.Center.X, player.Center.Y - 50, 0, 0, mod.ProjectileType("IceShardSpawner"), item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[0]--;
            if(play.cooldowns[0] == -1)
            {
                play.cooldowns[0] = play.maxCooldowns[0];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("AmethystTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
