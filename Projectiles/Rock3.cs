using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class Rock3 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = 0.7f;
            projectile.aiStyle = 1;
            projectile.friendly = true;
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            projectile.velocity.Y += 0.14f;
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 0, -projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, -projectile.velocity.Y + Main.rand.Next(-4, -4) * 1f, 0);
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
