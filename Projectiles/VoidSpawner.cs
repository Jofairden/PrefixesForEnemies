using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class VoidSpawner : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 90;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Breach");
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            projectile.ai[1] += 1f;
            if (projectile.ai[1] >= 4f)
            {
                projectile.ai[1] = 0f;
                for (int i = 0; i < 3; i++)
                {
                    int d = Dust.NewDust(new Vector2(projectile.position.X + Main.rand.Next(-20, 21), projectile.position.Y + Main.rand.Next(-20, 21)), projectile.width, projectile.height, mod.DustType("VoidDust"), (Main.rand.Next(-15, 16)) * .3f, (Main.rand.Next(-15, 16)) * .3f);
                    Main.dust[d].noGravity = false;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            //find a sound to play here
            Vector2 velocity = new Vector2(Main.player[(int)projectile.ai[0]].Center.X - projectile.position.X, Main.player[(int)projectile.ai[0]].Center.Y - projectile.position.Y);
            velocity.Normalize();
            velocity *= 3.4f;
            Projectile.NewProjectile(projectile.position, velocity, mod.ProjectileType("VoidTendril"), projectile.damage, 2);
        }
    }
}
