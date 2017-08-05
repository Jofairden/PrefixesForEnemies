using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles
{
    public class HolyLance : ModProjectile
    {
        private NPC stuckTarget = null;
        private float stuckPosX, stuckPosY;

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.timeLeft = 480;
            projectile.penetrate = -1;
            projectile.maxPenetrate = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            //projectile.extraUpdates = 6;
            projectile.alpha = 50;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HolyLance");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 1f, 1f);
            if (projectile.ai[0] == 0f)
            {
                projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                int num986 = (int)projectile.ai[1];
                if (!Main.npc[num986].active)
                {
                    projectile.Kill();
                    return;
                }
                /*
                Vector2 vector122 = Main.npc[num986].Center - projectile.Center;
                if (vector122 != Vector2.Zero)
                {
                    vector122.Normalize();
                    vector122 *= 14f;
                }
                float num987 = 5f;
                projectile.velocity = (projectile.velocity * (num987 - 1f) + vector122) / num987;
                projectile.velocity.ToRotation();
                */
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                bool flag53 = false;
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] % 30f == 0f)
                {
                    flag53 = true;
                }
                int num991 = (int)projectile.ai[1];
                if (Main.npc[num991].active && !Main.npc[num991].dontTakeDamage)
                {
                    projectile.Center = Main.npc[num991].Center - projectile.velocity * 2f;
                    projectile.gfxOffY = Main.npc[num991].gfxOffY;
                    if (flag53)
                    {
                        Main.npc[num991].HitEffect(0, 1.0);
                    }
                }
                int num986 = (int)projectile.ai[1];
                if (!Main.npc[num986].active)
                {
                    projectile.Kill();
                    return;
                }
            }
            if (projectile.timeLeft < 450 && projectile.ai[0] == 0f && projectile.velocity.Length() < 1)
            {
                projectile.velocity.X *= 12000f;
                projectile.velocity.Y *= 12000f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = 1f;
            projectile.ai[1] = target.whoAmI;
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.lightSpearCount++;
            target.immune[projectile.owner] = 0;
            projectile.friendly = false;
            stuckTarget = target;
            /*
            stuckPosX = projectile.position.X - target.position.X;
            stuckPosY = -projectile.position.Y + target.position.Y;
            */
            projectile.timeLeft = 300;
            projectile.extraUpdates = 0;
        }
        public override void Kill(int timeLeft)
        {
            if(stuckTarget != null)
            {
                gNPC info = stuckTarget.GetGlobalNPC<gNPC>();
                info.lightSpearCount--;
            }
            for(int i=0; i<10; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, (projectile.velocity.X + Main.rand.Next(-2, 3)) * .2f, (projectile.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.White, 3f);
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 25);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)(texture.Height / Main.projFrames[projectile.type]) * 0.5f);
            SpriteEffects effect = projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 adjustment = new Vector2(projectile.Center.X - 4, projectile.Center.Y - 8);
            Main.spriteBatch.Draw(texture, adjustment - Main.screenPosition, new Rectangle?(Utils.Frame(texture, 1, Main.projFrames[projectile.type], 0, projectile.frame)), lightColor, projectile.rotation, origin, projectile.scale, effect, 0);
            return false;
        }
    }
}
