using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class RazorSeekers : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 60;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 12;
            item.useAnimation = 12;
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
      DisplayName.SetDefault("Blood: Razor Seekers");
      Tooltip.SetDefault("Piercing seekers hardened and sharpened by magic");
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
            if (player.buffTime[b] <= 481)
            {
                return false;
            }
            player.buffTime[b] -= 480;
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 12;
            int q = Projectile.NewProjectile(p.Center.X, p.Center.Y, vel.X, vel.Y, mod.ProjectileType("RazorSeeker"), (int)(item.damage * player.magicDamage), item.knockBack, player.whoAmI);
            return true;
        }
    }
}
