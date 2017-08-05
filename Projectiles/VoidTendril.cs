using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class VoidTendril : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.timeLeft = 120;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.magic = true;
            projectile.tileCollide = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Tendril");
        }
        public override void AI()
        {
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("VoidDust"), 0, 0);
            Main.dust[d].noGravity = true;
            Main.dust[d].scale += .5f;
            projectile.velocity = projectile.velocity.RotatedByRandom(.2);
            if(projectile.timeLeft % 10 == 0)
            {
                int p = Projectile.NewProjectile(projectile.position, Vector2.Zero, mod.ProjectileType("VoidHitbox"), projectile.damage, projectile.knockBack);
                Main.projectile[p].timeLeft = projectile.timeLeft + 20;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            MPlayer pinf = ((MPlayer)target.GetModPlayer(mod, "MPlayer"));
            pinf.voidBurn = Math.Max((int)(damage / 25f + target.statLifeMax2 / 200f + 1), pinf.voidBurn);
            target.AddBuff(mod.BuffType("VoidBurn"), 480 + 100*damage);
        }
    }
}
