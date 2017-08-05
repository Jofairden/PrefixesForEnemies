using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using EnemyMods.NPCs;

namespace EnemyMods.Projectiles
{
    public class BigHolyLance : ModProjectile
    {
        private NPC stuckTarget = null;
        private float stuckPosX, stuckPosY;

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.timeLeft = 510;
            projectile.penetrate = -1;
            projectile.maxPenetrate = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.scale = 2f;
            projectile.aiStyle = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BigHolyLance");
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 1f, 1f);
            if (projectile.ai[0] == 0f)
            {
                projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + .785f;
                int num986 = (int)projectile.ai[1];
                if (!Main.npc[num986].active)
                {
                    projectile.Kill();
                    return;
                }
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
                projectile.velocity.X *= 8000f;
                projectile.velocity.Y *= 8000f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.ai[0] = 1f;
            projectile.ai[1] = target.whoAmI;
            gNPC info = target.GetGlobalNPC<gNPC>();
            info.lightSpearCount+=10;
            target.immune[projectile.owner] = 0;
            projectile.friendly = false;
            stuckTarget = target;
            projectile.timeLeft = 300;
            projectile.extraUpdates = 0;
        }
        public override void Kill(int timeLeft)
        {
            if (stuckTarget != null)
            {
                gNPC info = stuckTarget.GetGlobalNPC<gNPC>();
                info.lightSpearCount-=10;
            }
            for (int i = 0; i < 50; i++)
            {
                int d = Dust.NewDust(new Vector2(projectile.position.X-16, projectile.position.Y), projectile.width, projectile.height, 57, (projectile.velocity.X + Main.rand.Next(-2, 3)) * .2f, (projectile.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.White, 3f);
            }
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 25);
        }
    }
}
