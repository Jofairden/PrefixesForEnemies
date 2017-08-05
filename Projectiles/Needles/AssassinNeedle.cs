using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace EnemyMods.Projectiles.Needles
{
    public class AssassinNeedle : ModProjectile
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
            DisplayName.SetDefault("Assassin Needle");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if((target.life == target.lifeMax || target.life < target.lifeMax / 5) && Main.rand.Next(4) == 0)
            {
                crit = true;
                damage *= 2;
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
