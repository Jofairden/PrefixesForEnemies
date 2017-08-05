using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Turrets
{
    public class GunTurretCapsule : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
        }
        public override void AI()
        {

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            float aiOut = 1.57f * 3;
            Vector2 tilePos = (projectile.Center) / 16;
            //determines which way the turret should face depending on how it collided
            //aiOut is the angle from turret to stand
            if(Main.tile[(int)tilePos.X, (int)tilePos.Y + 1].active() && projectile.oldVelocity.Y > 0)
            {
                aiOut = 1.57f * 3;
            }
            if (Main.tile[(int)tilePos.X + 1, (int)tilePos.Y].active() && projectile.oldVelocity.X > 0)
            {
                aiOut = 3.14f;
            }
            if (Main.tile[(int)tilePos.X -1, (int)tilePos.Y].active() && projectile.oldVelocity.X <= 0)
            {
                aiOut = 0f;
            }
            if (Main.tile[(int)tilePos.X, (int)tilePos.Y - 1].active() && projectile.oldVelocity.Y <= 0)
            {
                aiOut = 1.57f;
            }
            Player player = Main.player[projectile.owner];
            player.AddBuff(mod.BuffType("GunTurret"), 18002);
            MPlayer mplayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            mplayer.gunTurret = true;
            int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("GunTurret"), projectile.damage, projectile.knockBack, projectile.owner, 0, aiOut);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            if (timeLeft == 0)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("GunTurret"), 1, false, 0, false, false);
            }
        }
    }
}