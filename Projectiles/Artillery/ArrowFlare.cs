using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Artillery
{
    public class ArrowFlare : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(163);
            projectile.timeLeft = 1200;
        }
        public override void AI()
        {
            if (projectile.velocity.Length() == 0 && Main.player[projectile.owner].FindBuffIndex(mod.BuffType("ArtilleryCooldown"))==-1 && projectile.position.Y < Main.worldSurface)
            {
                for(int i=0; i<15; i++)
                {
                    float X = Main.rand.Next(-100, 101)+projectile.position.X;
                    float Y = Main.rand.Next(-800, 301)+projectile.position.Y - 2000;
                    int p = Projectile.NewProjectile(X, Y, -X / 200, 5 + Main.rand.Next(-100, 101) / 100, 1, (int)(15 * Main.player[projectile.owner].rangedDamage), 2, projectile.owner);
                }
                Main.player[projectile.owner].AddBuff(mod.BuffType("ArtilleryCooldown"), 1200);
            }
        }
    }
}
