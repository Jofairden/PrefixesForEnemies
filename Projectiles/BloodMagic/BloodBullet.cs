using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles.BloodMagic
{
    public class BloodBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 30;
            projectile.timeLeft = 600;
            projectile.maxPenetrate = 1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = .7f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Bullet");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.9f, 0.5f, 0.5f);
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(0, 2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, 0, 0, 100, default(Color), 1.2f);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 5, 0f, 0f, 0, default(Color), 1f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bloodied"), 180);
        }
    }
}
