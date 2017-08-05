using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items.Tier5
{
    public class SoulWellRing : ModItem
    {
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
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("SoulWell1");
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Soul Geyser");
      Tooltip.SetDefault("Pierce the veil. One charge.");
    }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, item.damage, item.knockBack, item.owner);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[13]--;
            if (play.cooldowns[13] == -1)
            {
                play.cooldowns[13] = play.maxCooldowns[13];
            }
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[13] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
