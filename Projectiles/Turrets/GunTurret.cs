using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class GunTurret : BasicTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 42;
            projectile.height = 30;

            target = null;
            range = 1000;
            ammoType = 14;
            fireRate = 25;//number of frames between shots
            shootSpeed = 12;
        }
        public override void AI()
        {
            basicAI();
            if(!hasStand)
            {
                createStand();
                hasStand = true;
            }

        }
        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("DualRifle"), 1, false, 0, false, false);
        }
        protected override void shoot(Vector2 toTarget)
        {
            int type;
            int damage = consumeAmmo(out type);
            if(damage == -1)
            {
                return;
            }
            Main.PlaySound(2, projectile.position, 11);
            toTarget.Normalize();
            toTarget *= shootSpeed;
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, toTarget.X, toTarget.Y, type, damage, projectile.knockBack, projectile.owner);

        }

        protected override void shootSecondary(Vector2 toTarget)
        {
            return;
        }

        protected override void update()
        {
            Player player = Main.player[projectile.owner];
            MPlayer mplayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (player.dead)
            {
                mplayer.gunTurret = false;
            }
            if (!mplayer.gunTurret)
            {
                projectile.timeLeft = 1;
            }
        }
    }
}
