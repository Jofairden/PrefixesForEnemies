using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class BowTurret : BasicTurret
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.width = 16;
            projectile.height = 32;

            target = null;
            range = 1000;
            ammoType = 1;
            fireRate = 40;//number of frames between shots
            shootSpeed = 12;
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
            if(Main.netMode != 1)
            Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("BowTurret"), 1, false, 0, false, false);
        }
        protected override void shoot(Vector2 toTarget)
        {
            int type;
            int damage = consumeAmmo(out type);
            if (damage == -1)
            {
                return;
            }
            Main.PlaySound(2, projectile.position, 5);
            float distance = toTarget.Length();
            distance = (distance / 5f) * distance;
            float rot = toTarget.ToRotation();
            bool flag = (rot > 1.57f * 1.5f || rot < 1.57f * .5f);
            int quadrant = (int)(rot / (float)Math.PI/2);
            if (quadrant == 1 && flag)
            {
                toTarget = toTarget.RotatedBy(distance / 2400);
            }
            if (quadrant == 2 && flag)
            {
                toTarget = toTarget.RotatedBy(-distance / 2400);
            }
            if(quadrant == 3)
            {
                float rotAdj = (float)Math.Max((rot - Math.PI * 1.5) * 3.5, 1);
                toTarget = toTarget.RotatedBy(-distance / (2400*rotAdj));
            }
            if (quadrant == 4)
            {
                float rotAdj = (float)Math.Max((Math.PI * 1.5 - rot) * 3.5f, 1);
                toTarget = toTarget.RotatedBy(distance / (2400 * rotAdj));
            }
            toTarget.Normalize();
            toTarget *= shootSpeed;
            int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, toTarget.X, toTarget.Y, type, damage, projectile.knockBack, projectile.owner);
            if (Main.projectile[p].Name.Contains("Greatarrow"))
            {
                Main.projectile[p].velocity /= 2;
                Main.projectile[p].damage /= 2;
            }

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
                mplayer.bowTurret = false;
            }
            if (!mplayer.bowTurret)
            {
                projectile.timeLeft = 1;
            }
        }
    }
}
