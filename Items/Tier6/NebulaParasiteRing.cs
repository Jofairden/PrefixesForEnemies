using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    public class NebulaParasiteRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 1;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 100000;
            item.rare = 10;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            item.shootSpeed = 8f;
            item.shoot = mod.ProjectileType("NebulaParasite");
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Nebula Parasite");
      Tooltip.SetDefault("Fires a parasite that damages enemies from within. One charge.");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[16]--;
            if (play.cooldowns[16] == -1)
            {
                play.cooldowns[16] = play.maxCooldowns[16];
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[16] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
