using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles.Needles
{
    public class LodestarNeedle : ModProjectile
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
            DisplayName.SetDefault("Lodestar Needle");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(8) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 265, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 265, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            //spawn lunar flare
            int p = Projectile.NewProjectile(new Vector2(projectile.position.X + Main.rand.Next(-200, 201), projectile.position.Y - 1000), Vector2.Zero, 645, (int)(projectile.damage * 1.3), 0, projectile.owner, 0, projectile.height);
            Vector2 vel = projectile.position - Main.projectile[p].position;
            vel.Normalize();
            vel *= 14;
            Main.projectile[p].velocity = vel;
        }
    }
}
