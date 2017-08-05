using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class FireRain : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 600;
            projectile.maxPenetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = .5f;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.extraUpdates = 2;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FireRain");
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 4.71f;
            if (Main.rand.Next(0, 7) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0, 0, 100, Color.OrangeRed, 1.6f);
            }
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 127, projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, projectile.velocity.Y + Main.rand.Next(-4, 4) * .2f, 100, Color.OrangeRed, 0.2f);
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(24, 300);
        }
    }
}
