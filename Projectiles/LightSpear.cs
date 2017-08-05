using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles
{
    public class LightSpear : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 24;
            projectile.timeLeft = 480;
            projectile.maxPenetrate = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.extraUpdates = 6;
            projectile.light = 1f;
            projectile.alpha = 100;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LightSpear");
        }
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if(projectile.timeLeft < 360)
            {
                projectile.velocity.X *= 1.0255f;
                projectile.velocity.Y *= 1.0255f;
            }
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, (projectile.velocity.X + Main.rand.Next(-2, 2)) * .2f, (projectile.velocity.Y + Main.rand.Next(-2, -2)) * .2f, 100, Color.White, 3f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 25);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
