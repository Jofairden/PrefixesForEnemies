using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class ShotgunTurret : BasicTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 42;
            projectile.height = 30;

            target = null;
            range = 800;
            ammoType = 14;
            fireRate = 65;//number of frames between shots
            shootSpeed = 11;
            secondaryType = ProjectileID.GreenLaser;
            secondaryRate = 120;
            secondarySpeed = 15;
            secondaryDam = 20;
        }
        public override void AI()
        {
            basicAI();
            if (!hasStand)
            {
                createStand();
                hasStand = true;
            }

        }
        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("ShotgunTurret"), 1, false, 0, false, false);
        }
        protected override void shoot(Vector2 toTarget)
        {
            int type;
            int damage = consumeAmmo(out type);
            if (damage == -1)
            {
                return;
            }
            Main.PlaySound(2, projectile.position, 36);
            toTarget.Normalize();
            toTarget *= shootSpeed;
            for(int i=0; i<5; i++)
            {
                float spread = 30f * 0.0174f;//40 degrees converted to radians
                float baseSpeed = toTarget.Length();
                double baseAngle = Math.Atan2(toTarget.X, toTarget.Y);
                double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
                float speedX = baseSpeed * (float)Math.Sin(randomAngle);
                float speedY = baseSpeed * (float)Math.Cos(randomAngle);
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, speedX, speedY, type, damage, projectile.knockBack, projectile.owner);
            }
        }

        protected override void shootSecondary(Vector2 toTarget)
        {
            Main.PlaySound(2, projectile.position, 12);
            int damageMod = (int)(secondaryDam*Main.player[projectile.owner].rangedDamage);
            toTarget.Normalize();
            toTarget *= secondarySpeed;
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, toTarget.X, toTarget.Y, secondaryType, projectile.damage + damageMod, projectile.knockBack, projectile.owner);
        }
        protected override void update()
        {
            Player player = Main.player[projectile.owner];
            MPlayer mplayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (player.dead)
            {
                mplayer.shotgunTurret = false;
            }
            if (!mplayer.shotgunTurret)
            {
                projectile.timeLeft = 1;
            }
        }
    }
}
