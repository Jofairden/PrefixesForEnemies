using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class AcidRain : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.timeLeft = 600;
            projectile.maxPenetrate = 1;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 1;
            projectile.extraUpdates = 2;
            projectile.friendly = true;
            //projectile.light = 0.5f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AcidRain");
        }

        public override void AI()
        {
            if (Main.rand.Next(0, 65) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 61, 0, 0, 100, Color.Green, 0.6f);
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(0, 5) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 61, 0, 0, 100, Color.Green, 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);//look for better one
            if (Main.rand.Next(1, 20) == 1)
            {
                int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -1f, 512, projectile.damage, projectile.knockBack + 2, projectile.owner);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(20, 300);
        }
    }
}
