using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Projectiles
{
    public class SolarShowerSpawner : ModProjectile
    {
        int timer = 0;

        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.timeLeft = 600;
            projectile.penetrate = 100;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.alpha = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SolarSpawner");
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void AI()
        {
                //position randomizer weighted towards central values
                float posX = projectile.Center.X;
                float posY = projectile.Center.Y;
                posX += Main.rand.Next(-200, 200) + Main.rand.Next(-30, 30);
                posY += Main.rand.Next(-50, 50) + Main.rand.Next(-30, 30);

                int p = Projectile.NewProjectile(posX, posY, Main.rand.Next(-20, 21)/10, 4 + Main.rand.Next(0, 3), mod.ProjectileType("SolarShower"), projectile.damage, projectile.knockBack, projectile.owner);
        }
    }
}
