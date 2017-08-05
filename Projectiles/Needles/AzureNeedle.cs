using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Needles
{
    public class AzureNeedle : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.timeLeft = 600;
            projectile.thrown = true;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 1;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azure Needle");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), .4f, .4f, 1f);
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int num259 = 0; num259 < 30; num259++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 2f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity.Normalize();
                Main.dust[d].velocity *= 6;
            }
            projectile.position = projectile.Center;
            projectile.width = (projectile.height = 115);
            projectile.Center = projectile.position;
            projectile.penetrate = -1;
            projectile.damage = (int)(projectile.damage * 1.3);
            projectile.knockBack *= 1.2f;
            projectile.Damage();
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
