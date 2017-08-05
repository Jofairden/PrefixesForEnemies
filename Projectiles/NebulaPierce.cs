using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles
{
    public class NebulaPierce : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 26;
            projectile.timeLeft = 300;
            projectile.penetrate = -1;
            projectile.maxPenetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.aiStyle = 0;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NebulaPiercer");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            projectile.timeLeft -= 30;
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 1f, .1f, 0.6f);
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 10; num136++)
                {
                    float x2 = projectile.Center.X - projectile.velocity.X / 10f * (float)num136;
                    float y2 = projectile.Center.Y - projectile.velocity.Y / 10f * (float)num136;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 242, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.PinkCrystalShard, projectile.velocity.X, projectile.velocity.Y);
        }/*
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / Main.projFrames[projectile.type]) * 0.5f);
            SpriteEffects effect = projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 adjustment = new Vector2(projectile.Center.X - 4, projectile.Center.Y - 4);
            Main.spriteBatch.Draw(texture, adjustment - Main.screenPosition, new Rectangle?(Utils.Frame(texture, 1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, origin, projectile.scale, effect, 0);
            return false;
        }*/
    }
}
