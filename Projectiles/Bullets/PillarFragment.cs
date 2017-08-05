using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Bullets
{
    public class PillarFragment : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 1;
            projectile.friendly = true;
            projectile.light = 0.5f;
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] == 0 && projectile.timeLeft <= 580)
            {
                double n = Main.rand.Next(2, 4);
                float spread = 40f * 0.0174f;
                float baseSpeed = projectile.velocity.Length();
                double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / n;
                double offsetAngle;
                int i;
                for (i = 0; i < n; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    int rand = Main.rand.Next(0, 4);
                    if (rand == 0)
                    {
                        int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11)/10, mod.ProjectileType("SolarFrag"), (int)(projectile.damage * .6), projectile.knockBack, projectile.owner);
                        for(int j=0; j<4; j++)
                        {
                            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 259, 0, 0, 100, default(Color), 0.6f);
                        }
                    }
                    else if (rand == 1)
                    {
                        int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11)/10, mod.ProjectileType("VortexFrag"), (int)(projectile.damage * .6), projectile.knockBack, projectile.owner);
                        for (int j = 0; j < 4; j++)
                        {
                            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0, 0, 100, default(Color), 0.6f);
                        }
                    }
                    else if (rand == 2)
                    {
                        int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, baseSpeed * .3f * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * .3f * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11)/10, mod.ProjectileType("NebulaFrag"), (int)(projectile.damage * .6), projectile.knockBack, projectile.owner);
                        for (int j = 0; j < 4; j++)
                        {
                            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 69, 0, 0, 100, default(Color), 0.9f);
                        }
                    }
                    else if (rand == 3)
                    {
                        int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, baseSpeed * .8f * (float)Math.Sin(offsetAngle) + Main.rand.Next(-10, 11)/10, baseSpeed * .8f * (float)Math.Cos(offsetAngle) + Main.rand.Next(-10, 11) / 10, mod.ProjectileType("StardustFrag"), (int)(projectile.damage * .6), projectile.knockBack, projectile.owner);
                        for (int j = 0; j < 4; j++)
                        {
                            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0, 0, 100, default(Color), 0.6f);
                        }
                    }
                }
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);//look for better one
        }
    }
}
