using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class SolarShower : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.timeLeft = 600;
            projectile.maxPenetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = .4f;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.extraUpdates = 1;
            projectile.light = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SolarShower");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 4.71f;

            if (Main.rand.Next(0, 16) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 259, (projectile.velocity.X) * .4f, (projectile.velocity.Y + Main.rand.Next(-4, 4)) * .05f, 100, default(Color), 1.5f);
                //int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 158, (projectile.velocity.X) * .3f, (projectile.velocity.Y + Main.rand.Next(-4, 4)) * .05f, 100, default(Color), 1.5f);
                int dust3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, (projectile.velocity.X) * .2f, (projectile.velocity.Y + Main.rand.Next(-4, 4)) * .2f, 100, default(Color), 1.5f);
            }
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 259, 0, 0, 100, default(Color), 0.5f);
            if (Main.rand.Next(0, 8) == 0)
            {
                int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, 612, projectile.damage, 1f, projectile.owner, 0f, 0.45f + Main.rand.NextFloat() * 1.15f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.AddBuff(BuffID.Daybreak, 90);
        }
    }
}
