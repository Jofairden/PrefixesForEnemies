using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class BloodShot : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 35;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item43;
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood: Shot");
      Tooltip.SetDefault("Fires a spread of blood clots from your Well");
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
            if (player.buffTime[b] <= 901)
            {
                return false;
            }
            player.buffTime[b] -= 900;
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 18;
            for(int i=0; i<6; i++)
            {
                int q = Projectile.NewProjectile(p.Center.X, p.Center.Y, vel.X + Main.rand.Next(-30, 31)/15, vel.Y + Main.rand.Next(-30, 31) / 15, mod.ProjectileType("BloodBullet"), (int)(item.damage * player.magicDamage), item.knockBack, player.whoAmI);
            }
            return true;
        }
    }
}
