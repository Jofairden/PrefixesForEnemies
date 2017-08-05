using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class IceShardSpawner : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 120;
            projectile.penetrate = 100;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = 0.7f;
            projectile.alpha = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceShardSpawner");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 10f && projectile.owner==Main.myPlayer && Main.netMode != 1)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, (projectile.velocity.X + Main.rand.Next(-2, 3)) * .01f, (projectile.velocity.Y + Main.rand.Next(-2, 3)) * .01f, 100, Color.AliceBlue, 1.5f);
                projectile.ai[0] = 0;

                //conversion to polar from cartesian for velocity
                double x = projectile.Center.X - Main.MouseWorld.X;
                double y = -projectile.Center.Y + Main.MouseWorld.Y;
                //double r = Math.Sqrt((x * x) + (y * y));
                double q = Math.Atan2(x, y);
                //x = r; r not needed since we always want r=1 so each has the same speed
                y = q;
                float XVel = (float)(0.1 * Math.Cos(q+ 1.57f));
                float YVel = (float)(0.1 * Math.Sin(q+ 1.57f));

                //position randomizer weighted towards central values
                float posX = projectile.Center.X;
                float posY = projectile.Center.Y;

                posX += (Main.rand.Next(-20, 20) + Main.rand.Next(-30, 30));
                posY += (Main.rand.Next(-20, 20) + Main.rand.Next(-30, 30));

                int p = Projectile.NewProjectile(posX, posY, XVel, YVel, mod.ProjectileType("IceShard"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
    }
}
