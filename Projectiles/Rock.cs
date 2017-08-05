using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class Rock : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 1.3f;
            projectile.aiStyle = 1;
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.14f;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (-projectile.velocity.X + Main.rand.Next(-4, 4)) * .25f, (-projectile.velocity.Y + Main.rand.Next(1, 4)) * 0.6f, mod.ProjectileType("Rock2"), (int)(projectile.damage * .6f), projectile.knockBack - 2, projectile.owner);
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 0, -projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, -projectile.velocity.Y + Main.rand.Next(-4, -4) * 2f, 0);
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
