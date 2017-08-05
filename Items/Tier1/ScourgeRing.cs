using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;


namespace EnemyMods.Items.Tier1
{
    public class ScourgeRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 9;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 6;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            item.shoot = ProjectileID.TinyEater;
            item.shootSpeed = 6f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Scourge Ring");
      Tooltip.SetDefault("Summon a swarm of corrupt creatures. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[1] <= 0)
            {
                return false;
            }
            else
            {
                play.charges[1]--;
                if (play.cooldowns[1] == -1)
                {
                    play.cooldowns[1] = play.maxCooldowns[0];
                }
                return true;
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int p = Projectile.NewProjectile(position.X, position.Y, speedX + Main.rand.Next(-30, 31)/15f, speedY + Main.rand.Next(-30, 31) / 15f, type, damage, knockBack, item.owner);
            return false;
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
