using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles
{
    public class SoulWell1 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.timeLeft = 600;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.alpha = 150;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Well");
        }
        public override void AI()
        {
            if (projectile.localAI[0]==0f && projectile.timeLeft < 595)
            {
                projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                for (int num136 = 0; num136 < 10; num136++)
                {
                    float x2 = projectile.position.X - projectile.velocity.X / 10f * num136;
                    float y2 = projectile.position.Y - projectile.velocity.Y / 10f * num136;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 175, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            if (projectile.localAI[0] == 1f && projectile.timeLeft%8==0)
            {
                for (int num136 = 0; num136 < 10; num136++)
                {
                    int num137 = Dust.NewDust(projectile.position, 10, 10, 263);
                    Main.dust[num137].velocity *= 4f;
                    Main.dust[num137].noGravity = true;
                    Main.dust[num137].color = new Color (255,255,255);
                }
                int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.ai[0] + Main.rand.Next(-30, 31) / 10f, projectile.ai[1] + Main.rand.Next(-30, 31) / 10f, 297, projectile.damage, projectile.knockBack, projectile.owner);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.velocity = Vector2.Zero;
                projectile.localAI[0] = 1f;
                projectile.timeLeft = 240;
                projectile.penetrate = 1;
                projectile.ai[0] = -oldVelocity.X;
                projectile.ai[1] = -oldVelocity.Y;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 46);
                return false;
            }
            if (projectile.localAI[0] == 1f)
            {
                projectile.velocity = Vector2.Zero;
                return false;
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int num136 = 0; num136 < 10; num136++)
            {
                int num137 = Dust.NewDust(projectile.position, 10, 10, 175);
            }
            Main.PlaySound(4, (int)projectile.position.X, (int)projectile.position.Y, 6);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / Main.projFrames[projectile.type]) * 0.5f);
            SpriteEffects effect = projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 adjustment = new Vector2(projectile.Center.X, projectile.Center.Y - 2);
            Main.spriteBatch.Draw(texture, adjustment - Main.screenPosition, new Rectangle?(Utils.Frame(texture, 1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, origin, projectile.scale, effect, 0);
            return false;
        }
    }
}
