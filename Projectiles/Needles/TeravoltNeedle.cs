using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles.Needles
{
    public class TeravoltNeedle : ModProjectile
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
            DisplayName.SetDefault("Teravolt Needle");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(8) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Electric, 0, 0, 100, default(Color), 0.6f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.numNeedlesTeravolt++;
            if (info.numNeedlesTeravolt >= 6)
            {
                info.numNeedlesTeravolt = 0;
                if (!target.boss)
                {
                    if(target.realLife != -1)
                    {
                        target = Main.npc[target.realLife];
                    }
                    target.AddBuff(mod.BuffType("Stunned"), 40);
                }
                damage += (int)(180 * Main.player[projectile.owner].thrownDamage + Math.Min(250, target.lifeMax * .08));
                Main.PlaySound(2, projectile.position, 93);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Electric, 0, 0, 100, default(Color), 0.6f);
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
    }
}
