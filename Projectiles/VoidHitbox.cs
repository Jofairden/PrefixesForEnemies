using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles
{
    public class VoidHitbox : ModProjectile
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

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            MPlayer pinf = ((MPlayer)target.GetModPlayer(mod, "MPlayer"));
            pinf.voidBurn = Math.Max((int)(damage / 25f + target.statLifeMax2 / 200f + 1), pinf.voidBurn);
            target.AddBuff(mod.BuffType("VoidBurn"), 480 + 100 * damage);
        }
    }
}
