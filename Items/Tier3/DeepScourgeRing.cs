using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items.Tier3
{
    public class DeepScourgeRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 22;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 10;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 30000;
            item.rare = 4;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            item.shoot = ProjectileID.TinyEater;
            item.shootSpeed = 6f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Deep Scourge Ring");
      Tooltip.SetDefault("Summon a vast swarm of corrupt creatures. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[5] <= 0)
            {
                return false;
            }
            else
            {
                play.charges[5]--;
                if (play.cooldowns[5] == -1)
                {
                    play.cooldowns[5] = play.maxCooldowns[5];
                }
                return true;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < (Main.rand.Next(2, 4)); i++)
            {
                int p = Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-30, 30) / 15f, speedY + Main.rand.Next(-30, 30) / 15f, 307, damage, knockBack, item.owner);
            }
            int q = Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-30, 30) / 15f, speedY + Main.rand.Next(-30, 30) / 15f, 306, (int)(damage * 1.3), knockBack, item.owner);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ScourgeRing"), 1);
            recipe.AddIngredient(mod.ItemType("SapphireTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
