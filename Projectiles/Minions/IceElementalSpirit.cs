using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Projectiles.Minions
{
    public class IceElementalSpirit : HoverLOS
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 24;
            projectile.height = 32;
            Main.projFrames[projectile.type] = 5;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.minionSlots = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            inertia = 20f;
            shoot = ProjectileID.Blizzard;
            shootSpeed = 8f;
            shootCool = 20f;
            chaseDist = 150f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Elemental Spirit Greatarrow");
        }
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (player.dead)
            {
                modPlayer.waterSpirit = false;
            }
            if (modPlayer.waterSpirit)
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
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, 45);
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
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 45);
                    Main.dust[dust].velocity -= 1.2f * dustVel;
                }
            }
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 5;
            }
        }
    }
}
