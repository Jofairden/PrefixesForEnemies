using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Bullets.Fragments
{
    public class StardustFrag : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 1;
            projectile.penetrate = -1;
            projectile.maxPenetrate = -1;
            projectile.friendly = true;
            projectile.scale = .6f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Fragment");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), .5f, .5f, 1f);
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.timeLeft % 15 == 0)
            {
                int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 2, ProjectileID.Twinkle, projectile.damage - 5, projectile.knockBack, projectile.owner);
                Main.projectile[p].hostile = false;
                Main.projectile[p].friendly = true;
            }
            if (projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 5; num136++)
                {
                    float x2 = projectile.Center.X - projectile.velocity.X / 10f * (float)num136*2;
                    float y2 = projectile.Center.Y - projectile.velocity.Y / 10f * (float)num136*2;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 68, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
