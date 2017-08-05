using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class Shattershard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 10;
            projectile.timeLeft = 480;
            projectile.maxPenetrate = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 6;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shattershard");
        }
        public override void AI()
        {
            if (Main.rand.Next(0, 320) < Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y) + 2)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, projectile.velocity.X *.3f, projectile.velocity.Y * .3f, 100, Color.AliceBlue, 0.8f);
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
            if (Main.rand.Next(0, 3) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 600 + Main.rand.Next(0, 6) * 60);
            }
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 100, Color.AliceBlue, 1f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 51);
            for(int i=0; i<Main.rand.Next(2,4); i++)
            {
                float velX = 0;
                float velY = 0;
                while (velX == 0 || velY == 0)
                {
                    velX = Main.rand.Next(-8, 9) / 2f;
                    velY = Main.rand.Next(-8, 9) / 2f;
                }
                int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velX, velY, 344, (int)(projectile.damage * .75), 0, projectile.owner);
                Main.projectile[p].penetrate = 4;
                Main.projectile[p].maxPenetrate = 4;
                Main.projectile[p].timeLeft = 90;
                Main.projectile[p].tileCollide = true;
            }
        }
    }
}
