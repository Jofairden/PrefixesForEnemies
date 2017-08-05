using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Projectiles.Minions
{
    public class UnboundSoul:HoverLOS
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 24;
            projectile.height = 32;
            Main.projFrames[projectile.type] = 3;//frames on the sprite sheet
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            inertia = 20f;
            shoot = mod.ProjectileType("SoulProj");
            shootSpeed = 9f;
        }
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (player.dead)
            {
                modPlayer.soulMinion = false;
            }
            if (modPlayer.soulMinion)
            {
                projectile.timeLeft = 2;
            }
        }

        public override void CreateDust()
        {
            if (projectile.ai[0] == 0f)
            {
                if (Main.rand.Next(5) == 0)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, DustID.BlueCrystalShard);
                    Main.dust[dust].velocity.Y -= 1.2f;
                }
            }
            else
            {
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 dustVel = projectile.velocity;
                    if (dustVel != Vector2.Zero)
                    {
                        dustVel.Normalize();
                    }
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68);
                    Main.dust[dust].velocity -= 1.2f * dustVel;
                }
            }
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.2f, 0.2f, 0.9f);
        }

        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)//number of ticks (1/60 seconds) spent on each frame
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 3;//be sure to change the 3 to match the number of frames if it is not also 3
            }
        }
    }
}
