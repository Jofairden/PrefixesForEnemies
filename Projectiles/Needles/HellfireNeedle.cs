using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles.Needles
{
    public class HellfireNeedle : ModProjectile
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
            DisplayName.SetDefault("Hellfire Needle");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(8) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(projectile.width >= 80)
            {
                return;
            }
            target.AddBuff(BuffID.OnFire, 300);
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.numNeedlesHellfire++;
            if(info.numNeedlesHellfire >= 6)
            {
                info.numNeedlesHellfire = 0;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                for (int num507 = 0; num507 < 10; num507++)
                {
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                }
                for (int num508 = 0; num508 < 5; num508++)
                {
                    int num509 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num509].noGravity = true;
                    Main.dust[num509].velocity *= 3f;
                    num509 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num509].velocity *= 2f;
                }
                int num510 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num510].velocity *= 0.4f;
                Gore expr_10989_cp_0 = Main.gore[num510];
                expr_10989_cp_0.velocity.X = expr_10989_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
                Gore expr_109B9_cp_0 = Main.gore[num510];
                expr_109B9_cp_0.velocity.Y = expr_109B9_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
                num510 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num510].velocity *= 0.4f;
                Gore expr_10A4D_cp_0 = Main.gore[num510];
                expr_10A4D_cp_0.velocity.X = expr_10A4D_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
                Gore expr_10A7D_cp_0 = Main.gore[num510];
                expr_10A7D_cp_0.velocity.Y = expr_10A7D_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
                if (projectile.owner == Main.myPlayer)
                {
                    projectile.penetrate = -1;
                    projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                    projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                    projectile.width = 80;
                    projectile.height = 80;
                    projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                    projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                    projectile.damage = (int)((60 * Main.player[projectile.owner].thrownDamage) + (Math.Min(80, target.lifeMax * .04)));
                    projectile.Damage();
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
