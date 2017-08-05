using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Bullets
{
    public class PolarIce : ModProjectile
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
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Polar Ice");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), .2f, 1f, 1f);
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.timeLeft%4 == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 91, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 91, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 50);
            if (projectile.owner == Main.myPlayer)
            {
                for (int num385 = 0; num385 < 4; num385++)
                {
                    float num386 = -projectile.velocity.X * (float)Main.rand.Next(40, 60) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
                    float num387 = -projectile.velocity.Y * (float)Main.rand.Next(40, 60) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
                    num386 *= .5f;
                    num387 *= .5f;
                    int p = Projectile.NewProjectile(projectile.position.X + num386, projectile.position.Y + num387, num386, num387, 337, (int)((double)projectile.damage * 0.65), 0f, projectile.owner, 0f, 0f);
                    Main.projectile[p].timeLeft = 80;
                }
            }
        }
    }
}
