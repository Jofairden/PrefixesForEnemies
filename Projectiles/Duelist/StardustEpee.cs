using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace EnemyMods.Projectiles.Duelist
{
    public class StardustEpee : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 70;
            projectile.height = 70;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.hide = true;
            projectile.melee = true;
            projectile.scale = 1f;
            projectile.aiStyle = 19;
            projectile.friendly = true;
        }
        public override void AI()
        {
            Main.player[projectile.owner].direction = projectile.direction;
            Main.player[projectile.owner].heldProj = projectile.whoAmI;
            Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
            projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
            projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
            projectile.position += projectile.velocity * projectile.ai[0] + (projectile.velocity / 5) * projectile.width / 3; if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 3f;
                projectile.netUpdate = true;
            }
            if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
            {
                projectile.ai[0] -= .68f;
            }
            else
            {
                projectile.ai[0] += 0.68f;
            }

            if (Main.player[projectile.owner].itemAnimation == 0)
            {
                projectile.Kill();
            }
            if (projectile.timeLeft % 2 == 0)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135);
                Main.dust[d].scale = .8f;
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
        }
    }
}
