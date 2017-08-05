using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class Firecracker : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 1200;
            projectile.maxPenetrate = -1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.light = 1f;
            projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firecracker");
        }
        public override void AI()
        {
            for(int i=0; i<4; i++)
            {
                int num356 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 8, 8, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                Main.dust[num356].noGravity = true;
                Main.dust[num356].velocity *= 0.2f;
            }
            projectile.velocity.X += projectile.ai[1];
            projectile.velocity.Y += projectile.ai[2];
            projectile.ai[1] += (float)(Main.rand.Next(-4, 5) / 100);
            projectile.ai[2] += (float)(Main.rand.Next(-4, 5) / 100);

        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, (projectile.velocity.X + Main.rand.Next(-2, 2)) * .2f, (projectile.velocity.Y + Main.rand.Next(-2, -2)) * .2f, 100, Color.White, 3f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 25);
            int p = Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-30, 30), projectile.Center.Y + Main.rand.Next(-30, 30), 0, 0, 415, projectile.damage, projectile.knockBack, projectile.owner);
            int q = Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-30, 30), projectile.Center.Y + Main.rand.Next(-30, 30), 0, 0, 416, projectile.damage, projectile.knockBack, projectile.owner);
            int w = Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-30, 30), projectile.Center.Y + Main.rand.Next(-30, 30), 0, 0, 417, projectile.damage, projectile.knockBack, projectile.owner);
            Main.projectile[p].timeLeft = 2;
            Main.projectile[q].timeLeft = 2;
            Main.projectile[w].timeLeft = 2;
            Main.projectile[p].alpha = 255;
            Main.projectile[q].alpha = 255;
            Main.projectile[w].alpha = 255;
        }
    }
}
