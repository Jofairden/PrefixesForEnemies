using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles
{
    public class OrionHunter : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.width = 1;
            projectile.height = 1;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 630;
            projectile.alpha = 255;
        }
        public override void AI()
        {
            //ai 0 and 1 are stored velocity X and Y
            if(projectile.timeLeft%60==0 && projectile.owner == Main.myPlayer)
            {
                int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.ai[0], projectile.ai[1], 640, projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
    }
}
