using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class Seekers : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood: Seekers");
      Tooltip.SetDefault("Rapidly fires homing blood clots from your Well");
    }

        public override bool UseItem(Player player)
        {
            Projectile p = null;
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == mod.ProjectileType("BloodWell"))
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 241)
            {
                return false;
            }
            player.buffTime[b] -= 240;
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 12;
            int q = Projectile.NewProjectile(p.Center.X, p.Center.Y, vel.X, vel.Y, mod.ProjectileType("BloodSeeker"), (int)(item.damage * player.magicDamage), item.knockBack, player.whoAmI);
            return true;
        }
    }
}
