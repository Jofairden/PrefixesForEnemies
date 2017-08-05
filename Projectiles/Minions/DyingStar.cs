using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Minions
{
    public abstract class DyingStar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 24;
            projectile.height = 24;
            Main.projFrames[projectile.type] = 3;//frames on the sprite sheet
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            //projectile.aiStyle = 54;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dying Star");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            MPlayer playerinfo = (MPlayer)Main.player[projectile.owner].GetModPlayer(mod, "MPlayer");
            if (Main.player[Main.myPlayer].dead)
            {
                playerinfo.dyingStar = false;
            }
            if (playerinfo.dyingStar)
            {
                projectile.timeLeft = 2;
            }
            // most of vanilla raven code
            for (int num522 = 0; num522 < 1000; num522++)
            {
                if (num522 != projectile.whoAmI && Main.projectile[num522].active && Main.projectile[num522].owner == projectile.owner && Main.projectile[num522].type == projectile.type && Math.Abs(projectile.position.X - Main.projectile[num522].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num522].position.Y) < (float)projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num522].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + 0.05f;
                    }
                    if (projectile.position.Y < Main.projectile[num522].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + 0.05f;
                    }
                }
            }
            float num523 = projectile.position.X;
            float num524 = projectile.position.Y;
            float num525 = 900f;
            bool flag19 = false;
            int num526 = 500;
            if (projectile.ai[1] != 0f || projectile.friendly)
            {
                num526 = 1400;
            }
            if (Math.Abs(projectile.Center.X - Main.player[projectile.owner].Center.X) + Math.Abs(projectile.Center.Y - Main.player[projectile.owner].Center.Y) > (float)num526)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.tileCollide = true;
                for (int num527 = 0; num527 < 200; num527++)
                {
                    if (Main.npc[num527].CanBeChasedBy(this, false))
                    {
                        float num528 = Main.npc[num527].position.X + (float)(Main.npc[num527].width / 2);
                        float num529 = Main.npc[num527].position.Y + (float)(Main.npc[num527].height / 2);
                        float num530 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num528) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num529);
                        if (num530 < num525 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num527].position, Main.npc[num527].width, Main.npc[num527].height))
                        {
                            num525 = num530;
                            num523 = num528;
                            num524 = num529;
                            flag19 = true;
                        }
                    }
                }
            }
            else
            {
                projectile.tileCollide = false;
            }
            if (!flag19)
            {
                projectile.friendly = true;
                float num531 = 8f;
                if (projectile.ai[0] == 1f)
                {
                    num531 = 12f;
                }
                Vector2 vector38 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num532 = Main.player[projectile.owner].Center.X - vector38.X;
                float num533 = Main.player[projectile.owner].Center.Y - vector38.Y - 60f;
                float num534 = (float)Math.Sqrt((double)(num532 * num532 + num533 * num533));
                if (num534 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                }
                if (num534 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.width / 2);
                }
                if (num534 > 70f)
                {
                    num534 = num531 / num534;
                    num532 *= num534;
                    num533 *= num534;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num532) / 21f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num533) / 21f;
                }
                else
                {
                    if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                    {
                        projectile.velocity.X = -0.15f;
                        projectile.velocity.Y = -0.05f;
                    }
                    projectile.velocity *= 1.01f;
                }
                projectile.friendly = false;
                projectile.rotation = projectile.velocity.X * 0.05f;
                projectile.frameCounter++;
                if (projectile.frameCounter >= 4)
                {
                    projectile.frameCounter = 0;
                    projectile.frame++;
                }
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
                if ((double)Math.Abs(projectile.velocity.X) > 0.2)
                {
                    projectile.spriteDirection = -projectile.direction;
                    return;
                }
            }
            else
            {
                if (projectile.ai[1] == -1f)
                {
                    projectile.ai[1] = 17f;
                }
                if (projectile.ai[1] > 0f)
                {
                    projectile.ai[1] -= 1f;
                }
                if (projectile.ai[1] == 0f)
                {
                    projectile.friendly = true;
                    float num535 = 8f;
                    Vector2 vector39 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num536 = num523 - vector39.X;
                    float num537 = num524 - vector39.Y;
                    float num538 = (float)Math.Sqrt((double)(num536 * num536 + num537 * num537));
                    if (num538 < 100f)
                    {
                        num535 = 10f;
                    }
                    num538 = num535 / num538;
                    num536 *= num538;
                    num537 *= num538;
                    projectile.velocity.X = (projectile.velocity.X * 14f + num536) / 15f;
                    projectile.velocity.Y = (projectile.velocity.Y * 14f + num537) / 15f;
                }
                else
                {
                    projectile.friendly = false;
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 10f)
                    {
                        projectile.velocity *= 1.05f;
                    }
                }
                projectile.rotation = projectile.velocity.X * 0.05f;
            }
        }
        public void DamageArea(Projectile p, int x)//hostile npcs, no crit, no immunity
        {
            Rectangle hurtbox = new Rectangle((int)p.position.X - x, (int)p.position.Y - x, x * 2, x * 2);
            for (int i = 0; i < 200; i++)
            {
                bool flag = !p.usesLocalNPCImmunity || p.localNPCImmunity[i] == 0;
                bool flag2 = p.Colliding(hurtbox, Main.npc[i].getRect());
                if (Main.npc[i].active && !Main.npc[i].dontTakeDamage && flag && flag2 && ((p.friendly && (!Main.npc[i].friendly || p.type == 318 || (Main.npc[i].type == 22 && p.owner < 255 && Main.player[p.owner].killGuide) || (Main.npc[i].type == 54 && p.owner < 255 && Main.player[p.owner].killClothier))) || (p.hostile && Main.npc[i].friendly)) && (p.owner < 0 || Main.npc[i].immune[p.owner] == 0 || p.maxPenetrate == 1))
                {
                    int damage = (int)Main.npc[i].StrikeNPC(p.damage, p.knockBack, p.direction, false, false, false);
                }
            }
        }
    }
}
