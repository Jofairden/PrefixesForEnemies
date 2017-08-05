using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles
{
    public class gProj : GlobalProjectile
    {
        public bool azure = false;//apply slow debuff to enemies
        public bool shadowstrung = false;//arrow spawns two little eaters on kill
        public bool teravolt = false;//makes arrow behave like electrosphere
        public bool lihzahrd = false;
        public bool hellfire = false;//explode like hellfire arrow
        public bool lightcrystal = false;//explode like crystal serpent

        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public override void AI(Projectile projectile)
        {//todo - forking lightning in Kill(), kill projectile when far from player in AI(), homing in OnHitNPC()
            if (projectile.aiStyle == 88 && projectile.knockBack == .5f || (projectile.knockBack >= .2f && projectile.knockBack < .5f))
            {
                projectile.hostile = false;
                projectile.friendly = true;
                projectile.magic = true;
                projectile.penetrate = -1;
                if((projectile.knockBack >= .45f && projectile.knockBack < .5f) && projectile.oldVelocity != projectile.velocity && Main.rand.Next(0, 4)==0)
                {
                    projectile.knockBack -= .0125f;
                    Vector2 vector83 = projectile.velocity.RotatedByRandom(.1f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector83.X, vector83.Y, projectile.type, projectile.damage, projectile.knockBack-.025f, projectile.owner, projectile.velocity.ToRotation(), projectile.ai[1]);
                }
            }
        }
        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == 158 && projectile.hostile)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, 71, 1, false, 0, false, false);
            }
            if (projectile.type == 159 && projectile.hostile)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, 72, 1, false, 0, false, false);
            }
            if (projectile.type == 160 && projectile.hostile)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, 73, 1, false, 0, false, false);
            }
            if (projectile.type == 161 && projectile.hostile)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, 74, 1, false, 0, false, false);
            }
            //ProjInf info = (ProjInf)projectile.GetModInfo(mod, "ProjInf");
            if (shadowstrung)
            {
                Main.PlaySound(3, (int)projectile.position.X, (int)projectile.position.Y, 1);
                for (int num515 = 0; num515 < 20; num515++)
                {
                    int num516 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 184, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num516].scale *= 1.1f;
                    Main.dust[num516].noGravity = true;
                }
                for (int num517 = 0; num517 < 30; num517++)
                {
                    int num518 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 184, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[num518].velocity *= 2.5f;
                    Main.dust[num518].scale *= 0.8f;
                    Main.dust[num518].noGravity = true;
                }
                if (projectile.owner == Main.myPlayer)
                {
                    int num519 = 2;
                    if (Main.rand.Next(10) == 0)
                    {
                        num519++;
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        num519++;
                    }
                    if (Main.rand.Next(10) == 0)
                    {
                        num519++;
                    }
                    for (int num520 = 0; num520 < num519; num520++)
                    {
                        float num521 = (float)Main.rand.Next(-35, 36) * 0.02f;
                        float num522 = (float)Main.rand.Next(-35, 36) * 0.02f;
                        num521 *= 10f;
                        num522 *= 10f;
                        Projectile.NewProjectile(projectile.position.X, projectile.position.Y, num521, num522, 307, (int)((double)projectile.damage * 0.6), (float)((int)((double)projectile.knockBack * 0.35)), Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if (lightcrystal)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 110);
                for (int num158 = 0; num158 < 20; num158++)
                {
                    int num159 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 254, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
                    if (Main.rand.Next(3) == 0)
                    {
                        Main.dust[num159].fadeIn = 1.1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                        Main.dust[num159].scale = 0.35f + (float)Main.rand.Next(-10, 11) * 0.01f;
                        Main.dust[num159].type++;
                    }
                    else
                    {
                        Main.dust[num159].scale = 1.2f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    }
                    Main.dust[num159].noGravity = true;
                    Main.dust[num159].velocity *= 2.5f;
                    Main.dust[num159].velocity -= projectile.oldVelocity / 10f;
                }
                if (Main.myPlayer == projectile.owner)
                {
                    int num160 = Main.rand.Next(3, 6);
                    for (int num161 = 0; num161 < num160; num161++)
                    {
                        Vector2 value12 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        while (value12.X == 0f && value12.Y == 0f)
                        {
                            value12 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        }
                        value12.Normalize();
                        value12 *= (float)Main.rand.Next(70, 101) * 0.1f;
                        Projectile.NewProjectile(projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value12.X, value12.Y, 522, (int)((double)projectile.damage * 0.8), projectile.knockBack * 0.8f, projectile.owner, 0f, 0f);
                    }
                }
            }
            if (teravolt)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 94);
                int num191 = Main.rand.Next(3, 7);
                for (int num192 = 0; num192 < num191; num192++)
                {
                    int num193 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 2.1f);
                    Main.dust[num193].velocity *= 2f;
                    Main.dust[num193].noGravity = true;
                }
                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle value15 = new Rectangle((int)projectile.Center.X - 40, (int)projectile.Center.Y - 40, 80, 80);
                    for (int num194 = 0; num194 < 1000; num194++)
                    {
                        if (num194 != projectile.whoAmI && Main.projectile[num194].active && Main.projectile[num194].owner == projectile.owner && Main.projectile[num194].type == 443 && Main.projectile[num194].getRect().Intersects(value15))
                        {
                            Main.projectile[num194].ai[1] = 1f;
                            Main.projectile[num194].velocity = (projectile.Center - Main.projectile[num194].Center) / 5f;
                            Main.projectile[num194].netUpdate = true;
                        }
                    }
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, 443, projectile.damage, 0f, projectile.owner, 0f, 0f);
                }
            }
            if (hellfire)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                for (int num507 = 0; num507 < 10; num507++)
                {
                    Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                }
                for (int num508 = 0; num508 < 5; num508++)
                {
                    int num509 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num509].noGravity = true;
                    Main.dust[num509].velocity *= 3f;
                    num509 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num509].velocity *= 2f;
                }
                int num510 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num510].velocity *= 0.4f;
                Gore expr_10989_cp_0 = Main.gore[num510];
                expr_10989_cp_0.velocity.X = expr_10989_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
                Gore expr_109B9_cp_0 = Main.gore[num510];
                expr_109B9_cp_0.velocity.Y = expr_109B9_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
                num510 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num510].velocity *= 0.4f;
                Gore expr_10A4D_cp_0 = Main.gore[num510];
                expr_10A4D_cp_0.velocity.X = expr_10A4D_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
                Gore expr_10A7D_cp_0 = Main.gore[num510];
                expr_10A7D_cp_0.velocity.Y = expr_10A7D_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
                if (projectile.owner == Main.myPlayer)
                {
                    projectile.penetrate = -1;
                    projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                    projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                    projectile.width = 80;
                    projectile.height = 80;
                    projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                    projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                    projectile.Damage();
                }
            }
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (projectile.aiStyle == 88 && projectile.knockBack == .5f || (projectile.knockBack >= .2f && projectile.knockBack < .5f))
            {
                //causes it to run along the ground
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                    if(Math.Abs(projectile.velocity.X) <= 4)
                    {
                        projectile.velocity.X *= 2;
                    }
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                    if (Math.Abs(projectile.velocity.Y) <= 4)
                    {
                        projectile.velocity.Y *= 2;
                    }
                }
                //projectile.ai[0] = projectile.velocity.ToRotation(); makes it actually turn instead of bouncing along the ground
                return true;//change to false to make this stuff actually go through
            }
            return true;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.aiStyle == 88 && (projectile.knockBack >= .2f && projectile.knockBack <= .5f))
            {
                target.immune[projectile.owner] = 6;
            }
            //ProjInf info = (ProjInf)projectile.GetModInfo(mod, "ProjInf");
            if (azure && target.realLife == -1 && !target.boss)
            {
                target.AddBuff(mod.BuffType("Slow"), 300);
            }
        }
        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (projectile.aiStyle == 88 && ((projectile.knockBack == .5f || projectile.knockBack == .4f) || (projectile.knockBack >= .4f && projectile.knockBack < .5f)) && target.immune[projectile.owner] > 0)
            {
                return false;
            }
            return null;
        }
        public static NPC getClosestNPC(Projectile projectile)
        {
            float lowestD = 300;
            NPC closest = null;
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - projectile.Center.X) * (npc.Center.X - projectile.Center.X) + (npc.Center.Y - projectile.Center.Y) * (npc.Center.Y - projectile.Center.Y));
                if (lowestD > distance && !npc.townNPC && npc.life > 0 && npc.lifeMax > 10 && npc.CanBeChasedBy(projectile) && distance > 100)
                {
                    closest = npc;
                    lowestD = distance;
                }
            }
            return closest;
        }
    }
}
