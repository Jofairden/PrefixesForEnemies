using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;

namespace EnemyMods.Projectiles
{
    public class SoulProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            projectile.width = 8;
            projectile.height = 8;
            projectile.timeLeft = 600;
            projectile.maxPenetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.aiStyle = 1;
            projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SoulProj");
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.5f, 0.5f, 0.9f);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 8f)
            {
                projectile.frame = (projectile.frame + 1) % 4;
                projectile.ai[0] = 0f;
            }
            //homing
            if (projectile.alpha < 170)
            {
                for (int num136 = 0; num136 < 10; num136++)
                {
                    float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)num136;
                    float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)num136;
                    int num137 = Dust.NewDust(new Vector2(x2, y2), 1, 1, DustID.BlueCrystalShard, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num137].alpha = projectile.alpha;
                    Main.dust[num137].position.X = x2;
                    Main.dust[num137].position.Y = y2;
                    Main.dust[num137].velocity *= 0f;
                    Main.dust[num137].noGravity = true;
                }
            }
            float num138 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num139 = projectile.localAI[0];
            if (num139 == 0f)
            {
                projectile.localAI[0] = num138;
                num139 = num138;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            float num140 = projectile.position.X;
            float num141 = projectile.position.Y;
            float num142 = 300f;
            bool flag4 = false;
            int num143 = 0;
            if (projectile.ai[1] == 0f)
            {
                for (int num144 = 0; num144 < 200; num144++)
                {
                    if (Main.npc[num144].CanBeChasedBy(this, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num144 + 1)))
                    {
                        float num145 = Main.npc[num144].position.X + (float)(Main.npc[num144].width / 2);
                        float num146 = Main.npc[num144].position.Y + (float)(Main.npc[num144].height / 2);
                        float num147 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num145) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num146);
                        if (num147 < num142 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num144].position, Main.npc[num144].width, Main.npc[num144].height))
                        {
                            num142 = num147;
                            num140 = num145;
                            num141 = num146;
                            flag4 = true;
                            num143 = num144;
                        }
                    }
                }
                if (flag4)
                {
                    projectile.ai[1] = (float)(num143 + 1);
                }
                flag4 = false;
            }
            if (projectile.ai[1] > 0f)
            {
                int num148 = (int)(projectile.ai[1] - 1f);
                if (Main.npc[num148].active && Main.npc[num148].CanBeChasedBy(this, true) && !Main.npc[num148].dontTakeDamage)
                {
                    float num149 = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                    float num150 = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                    float num151 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num149) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num150);
                    if (num151 < 1000f)
                    {
                        flag4 = true;
                        num140 = Main.npc[num148].position.X + (float)(Main.npc[num148].width / 2);
                        num141 = Main.npc[num148].position.Y + (float)(Main.npc[num148].height / 2);
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }
            if (!projectile.friendly)
            {
                flag4 = false;
            }
            if (flag4)
            {
                float num152 = num139;
                Vector2 vector13 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num153 = num140 - vector13.X;
                float num154 = num141 - vector13.Y;
                float num155 = (float)Math.Sqrt((double)(num153 * num153 + num154 * num154));
                num155 = num152 / num155;
                num153 *= num155;
                num154 *= num155;
                int num156 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num156 - 1) + num153) / (float)num156;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num156 - 1) + num154) / (float)num156;
            }
            projectile.rotation = 0f;
        }
        public override void Kill(int timeLeft)
        {
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.BlueCrystalShard, projectile.velocity.X + Main.rand.Next(-4, 4) * .2f, projectile.velocity.Y + Main.rand.Next(-4, 4) * .2f, 100, default(Color), 0.2f);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            DamageArea(projectile, 70);
            for (int num958 = 0; num958 < 40; num958++)
            {
                int dust11 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0f, -2f, 0, default(Color), 2f);
                Main.dust[dust11].noGravity = true;
                Main.dust[dust11].position.X += Main.rand.Next(-50,51)/20 - 1.5f;
                Main.dust[dust11].position.Y += Main.rand.Next(-50, 51) / 20 - 1.5f;
                if (Main.dust[dust11].position != projectile.Center)
                {
                    Main.dust[dust11].velocity = projectile.DirectionTo(Main.dust[dust11].position) * 6f;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        public void DamageArea(Projectile p, int x)//hostile npcs, no crit, no immunity
        {
            Rectangle hurtbox = new Rectangle((int)p.position.X-x, (int)p.position.Y-x, x*2, x*2);
            for(int i=0; i<200; i++)
            {
                bool flag = !p.usesLocalNPCImmunity || p.localNPCImmunity[i] == 0;
                bool flag2 = p.Colliding(hurtbox, Main.npc[i].getRect());
                if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && flag && flag2 && ((p.friendly && (!Main.npc[i].friendly || p.type == 318 || (Main.npc[i].type == 22 && p.owner < 255 && Main.player[p.owner].killGuide) || (Main.npc[i].type == 54 && p.owner < 255 && Main.player[p.owner].killClothier))) || (p.hostile && Main.npc[i].friendly)) && (p.owner < 0 || Main.npc[i].immune[p.owner] == 0 || p.maxPenetrate == 1))
                {
                    int damage = (int)Main.npc[i].StrikeNPC(p.damage, p.knockBack, p.direction, false, false, false);
                }
            }
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
