using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class IceShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 10;
            projectile.timeLeft = 480;
            projectile.maxPenetrate = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 6;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceShard");
        }
        public override void AI()
        {
            if (Main.rand.Next(0, 150) < Math.Sqrt(projectile.velocity.X*projectile.velocity.X + projectile.velocity.Y*projectile.velocity.Y) + 2)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, projectile.velocity.X * .3f, projectile.velocity.Y * .3f, 100, Color.AliceBlue, 0.6f);
            }
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.timeLeft < 300)
            {
                projectile.velocity.X *= 1.0255f;
                projectile.velocity.Y *= 1.0255f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(0, 4) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 180 + Main.rand.Next(0, 4) * 60);
            }
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, (projectile.velocity.X + Main.rand.Next(-2, 3)) * .2f, (projectile.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.AliceBlue, 1f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 51);
        }
    }
}
