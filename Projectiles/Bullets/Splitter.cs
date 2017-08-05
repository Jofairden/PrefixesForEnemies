using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Bullets
{
    public class Splitter : ModProjectile
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
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Splitter");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.ai[1] == 0 && projectile.timeLeft <= 580 && Main.myPlayer == projectile.owner)
            {
                float spread = 25f * 0.0174f;
                float baseSpeed = projectile.velocity.Length();
                double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread;
                double offsetAngle;
                int i;
                for (i = 0; i < 2; i++)
                {
                    offsetAngle = startAngle + deltaAngle * i;
                    int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), projectile.type, (int)(projectile.damage * .7), projectile.knockBack, projectile.owner, 0, 1);
                }
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 7, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
