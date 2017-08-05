using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class TurretStand : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.scale = 1f;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            if (Main.projectile[(int)projectile.ai[0]].timeLeft == 0)
            {
                projectile.Kill();
            }
        }
    }
}
