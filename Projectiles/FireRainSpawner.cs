using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class FireRainSpawner : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 480;
            projectile.penetrate = 100;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.alpha = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FireRainSpawner");
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
            timer++;
            if (timer >= 2)
            {
                timer = 0;
                if (Main.rand.Next(0, 2) == 0) { timer++; }
                
                    //position randomizer weighted towards central values
                float posX = projectile.Center.X;
                float posY = projectile.Center.Y;
                posX += Main.rand.Next(-200, 200) + Main.rand.Next(-30, 30);
                posY += Main.rand.Next(-50, 50) + Main.rand.Next(-30, 30);

                int p = Projectile.NewProjectile(posX, posY, 2 + Main.rand.Next(0,2), 7 + Main.rand.Next(0,3), mod.ProjectileType("FireRain"), projectile.damage, projectile.knockBack, projectile.owner);
                if(Main.rand.Next(0, 10) == 0)
                {
                    int q = Projectile.NewProjectile(posX + Main.rand.Next(-20, 20), posY + Main.rand.Next(-20, 20), 2 + Main.rand.Next(0, 2), 7 + Main.rand.Next(0, 3), 400 + Main.rand.Next(0, 3), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }
    }
}
