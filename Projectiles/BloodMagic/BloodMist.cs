using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles.BloodMagic
{
    public class BloodMist : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.timeLeft = 120;
            projectile.maxPenetrate = 100;
            projectile.penetrate = 100;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.alpha = 220;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Mist");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            return false;
        }
        public override void AI()
        {
            if (Main.rand.Next(0, 2) == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, 0, 0, 100, default(Color), 1f);
                    Main.dust[dust].fadeIn = 0.7f + projectile.timeLeft / 800f;
                    Main.dust[dust].noGravity = true;
                }
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 60, 0, 0, 100, default(Color), 0.5f);
                Main.dust[d].fadeIn = 0.7f + projectile.timeLeft / 800f;
                Main.dust[d].noGravity = true;
            }
            projectile.velocity *= .98f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bloodied"), 180);
        }
    }
}
