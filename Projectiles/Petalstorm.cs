using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class Petalstorm : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 300;
            projectile.penetrate = 100;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.alpha = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Petalstorm");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            timer++;
            if (timer >= 3)
            {
                timer = 0;
                if (Main.rand.Next(0, 2) == 0) { timer++; }

                //position randomizer weighted towards central values
                float posX = projectile.Center.X;
                float posY = projectile.Center.Y;
                posX += Main.rand.Next(-50, 50) + Main.rand.Next(-30, 30);
                posY += Main.rand.Next(-150, 150) + Main.rand.Next(-50, 50);
                int a = Projectile.NewProjectile(posX, posY, 20 + Main.rand.Next(0, 2), 0, 227, projectile.damage, projectile.knockBack, projectile.owner);
                Main.projectile[a].maxPenetrate = 5;
                Main.projectile[a].penetrate = 5;
                if (projectile.timeLeft < 180 && projectile.timeLeft % 2==0)
                {
                    int p = Projectile.NewProjectile(posX, posY, 12 + Main.rand.Next(0, 2), 0, ProjectileID.FlowerPetal, (int)(projectile.damage * 1.2), projectile.knockBack, projectile.owner);
                    Main.projectile[p].timeLeft += 120;
                }
            }
        }
    }
}
