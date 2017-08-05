using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Projectiles
{
    public class MartyrBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.HappyBomb);
            projectile.timeLeft = 181;
            projectile.hostile = false;
        }
        public override void AI()
        {
            if(projectile.timeLeft % 60 == 0 && projectile.timeLeft > 59)
            {
                int time = projectile.timeLeft / 60;
                CombatText.NewText(projectile.getRect(), new Microsoft.Xna.Framework.Color(255, 20, 20), "" + time, true);
            }
        }
        public override void Kill(int timeLeft)
        {
            int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ProjectileID.Landmine, projectile.damage, 0);
        }
    }
}
