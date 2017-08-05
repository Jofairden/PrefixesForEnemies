using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Projectiles.Greatbows
{
    public class TeravoltGreatbow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.width = 36;
            projectile.height = 42;
            Main.projFrames[projectile.type] = 5;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 420;
            projectile.scale = 1f;
        }
        //STATS (these values can be multiplied up to 3 times based on charge, added onto a base of 1)
        float multSpeed = .4f;
        float multDamage = 1.1f;
        float multKnockback = 0.1f; //x1.3
        float multSuperDamage = .95f; // multiplier for full charge
        int arrowDamage = 0;
        bool arrowCollide = true;
        float arrowDrop = 0f;
        bool arrowHurtTile = true;
        float arrowSpeed = 0f;

        int chargeTick = 0;

        bool initial = true;
        bool released = false;
        bool fullPower = false;
        Projectile arrow = default(Projectile);
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, .23f, .37f, .45f);
            Player player = Main.player[projectile.owner];//owner
            Item bow = player.inventory[player.selectedItem];//the bow item
            player.heldProj = projectile.whoAmI;//held proj

            #region Val setups

            if (initial)//this stuff is the arrows stats
            {
                foreach (Projectile find in Main.projectile)
                {
                    if (find.damage == -projectile.owner - 256 && find.owner == projectile.owner)
                    {
                        arrow = find;
                        break;
                    }
                }

                projectile.timeLeft += bow.useAnimation;
                arrowDamage = projectile.damage;
                arrowCollide = arrow.tileCollide;
                arrowDrop = arrow.ai[0];
                //arrowHurtTile = arrow.;
                arrow.damage = 0;
                projectile.damage = 0;
                arrow.tileCollide = false;
                //arrow.hurtsTiles = false;

                arrowSpeed = projectile.knockBack;

                player.itemAnimation = bow.useAnimation - 1;
                player.itemTime = bow.useAnimation - 1;

                initial = false;
            }

            if (projectile.owner == Main.myPlayer)
            {//if mouse down and its the owner
                if (!released)//only follow mouse when aiming
                {
                    float mX = (float)(Main.mouseX + Main.screenPosition.X);
                    float mY = (float)(Main.mouseY + Main.screenPosition.Y);
                    float pX = player.position.X + player.width * 0.5f;
                    float pY = player.position.Y + player.height * 0.5f;
                    projectile.ai[0] = (float)Math.Atan2(mY - pY, mX - pX);
                    projectile.netUpdate = true;
                }
            }

            float itemRotCos = (float)Math.Cos(projectile.ai[0]);
            float itemRotSin = (float)Math.Sin(projectile.ai[0]);
            float reverseItemRot = 0;//reversed item rotation for facing left
            if (projectile.ai[0] < 0)
            {
                reverseItemRot = projectile.ai[0] + (float)Math.PI;
            }
            else {
                reverseItemRot = projectile.ai[0] - (float)Math.PI;
            }

            if (Math.Abs(projectile.ai[0]) > Math.Abs(Math.PI / 2f))//rotate bow and player
            {
                projectile.direction = -1;
                projectile.spriteDirection = -1;
                projectile.rotation = reverseItemRot;
                player.direction = -1;
                player.itemRotation = reverseItemRot;//change the arm direction
            }
            else {
                projectile.direction = 1;
                projectile.spriteDirection = 1;
                projectile.rotation = projectile.ai[0];
                player.direction = 1;
                player.itemRotation = projectile.ai[0];//change the arm direction
            }

            //position bow
            //projectile.position = player.position - new Vector2(-(player.width - projectile.width) / 2 + 0.5f - 0.5f * player.direction, 0);
            projectile.position = player.Center - new Vector2(projectile.width / 2, projectile.height / 2);
            projectile.position += new Vector2((float)(projectile.width * 0.1f * itemRotCos), (float)(projectile.width * 0.1f * itemRotSin));

            #endregion

            if (player.channel && projectile.timeLeft > bow.useAnimation && arrow.active)
            {//whilst channeling and 10 secs (600 ticks) hasn't expired
                projectile.frame = (int)projectile.ai[1];
                //hold player's arrow
                arrow.velocity = new Vector2((float)Math.Cos(projectile.ai[0]), (float)Math.Sin(projectile.ai[0]));
                arrow.position = player.Center - new Vector2(arrow.Name.Contains("Greatarrow") ? arrow.width : arrow.width / 1.5f, arrow.height / 2);
                arrow.position -= new Vector2((float)((2f * ((int)projectile.ai[1]) - 22f) * itemRotCos), (float)((2f * ((int)projectile.ai[1]) - 22) * itemRotSin));
                if (projectile.owner == Main.myPlayer)
                {
                    arrow.netUpdate = true;
                }

                //suspend arrow detonate time and falling time
                arrow.timeLeft += 2;
                arrow.ai[0] = arrowDrop;

                //keep player animation full to continue the hold animation
                player.itemAnimation = bow.useAnimation; // eg if anim = 40, this will stay at 39 until this line
                player.itemTime = bow.useAnimation;

                //shaky bow and arrow if near release time
                if (projectile.timeLeft < bow.useAnimation + bow.useTime * 5)
                {
                    projectile.position += new Vector2(Main.rand.Next(-6, 7) / 6f, Main.rand.Next(-6, 7) / 6f);
                    arrow.position += new Vector2(Main.rand.Next(-6, 7) / 6f, Main.rand.Next(-6, 7) / 6f);
                }

                //charge up bow - faster charge based on item's useTime
                if (projectile.ai[1] < 3)
                {
                    if (chargeTick >= bow.useTime)
                    {
                        chargeTick = 0;
                        projectile.ai[1]++;
                    }
                    else {
                        chargeTick++;
                    }
                }
                else {
                    if (chargeTick == 0)
                    {
                        projectile.ai[1] = 3;//failsafe
                        fullPower = true;
                        arrow.localAI[0] = 1f;
                        //beep mana sound and dust effect to indicate full charge
                        if (projectile.owner == Main.myPlayer) Main.PlaySound(25, (int)arrow.position.X, (int)arrow.position.Y, 0);
                        for (int i = 0; i < 4; i++)
                        {
                            int dustIndex = Dust.NewDust(arrow.position, arrow.width, arrow.height, 43, 0f, 0f, 100, Color.Transparent, 0.6f);
                            Main.dust[dustIndex].velocity *= 0.1f;
                            Main.dust[dustIndex].fadeIn = 0.8f;
                        }
                    }
                    if (chargeTick > 15)
                    {
                        multSuperDamage = 1f;
                    }
                    chargeTick++;
                }
            }
            else {//either player is firing or time has run out - either way
                if (!released)
                {//run on first tick of releasing
                    arrow.position = player.position + new Vector2((player.width - arrow.width) / 2, (player.height - arrow.height) / 2); // normal firing position
                                                                                                                                          //speed up arrow
                    arrowSpeed = (float)(arrowSpeed * (1f + multSpeed * ((int)projectile.ai[1])));
                    arrow.velocity = new Vector2(arrowSpeed * itemRotCos, arrowSpeed * itemRotSin);
                    if (arrow.Name.Contains("Greatarrow"))
                    {
                        arrow.velocity *= 1.8f;
                    }
                    //increased damage and knockback
                    arrowDamage = (int)(arrowDamage * (1f + multDamage * ((int)projectile.ai[1])) * multSuperDamage);
                    arrow.damage = arrowDamage;
                    arrow.knockBack *= 1f + multKnockback * ((int)projectile.ai[1]);
                    //re-enable collision and such
                    arrow.tileCollide = arrowCollide;
                    //arrow.hurtsTiles = arrowHurtTile;

                    if (projectile.owner == Main.myPlayer) arrow.netUpdate = true;

                    //pew sound and replace frame
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 5);
                    projectile.frame = 4;
                    //hold bow for additional bow use time
                    projectile.timeLeft = bow.useAnimation;
                    /*
                    if(Main.netMode == 1){//arrow stuff
                        NetMessage.SendModData(ModWorld.ModIndex,1,-1,-1, projectile.owner, arrow.whoAmI,arrow.position.X,arrow.position.Y,arrow.velocity.X,arrow.velocity.Y);
                    }
                    */
                    if (multSuperDamage != 1 && fullPower)
                    {
                        gProj info = arrow.GetGlobalProjectile<gProj>();
                        info.teravolt = true;
                    }
                    released = true;
                }
                else {//run until bow is removed
                      //add trail effect to fully charged arrows
                    if (fullPower && arrow.active)
                    {
                        int dustIndex = Dust.NewDust(
                            arrow.position,
                            arrow.width, arrow.height,
                            DustID.Electric,
                            arrow.velocity.X, arrow.velocity.Y,
                            45, Color.Transparent, 0.9f
                        );
                        if (multSuperDamage != 1 && fullPower)
                        {
                            float rotToTarget = Main.rand.Next((int)(-Math.PI * 10000), (int)(Math.PI * 10000)) / 10000f;
                            Vector2 arrowCentre = arrow.position + new Vector2(arrow.width / 2, arrow.height / 2);
                            Vector2 spawnPosition = arrowCentre + new Vector2((float)(30 * Math.Cos(rotToTarget)), (float)(30 * Math.Sin(rotToTarget)));
                            dustIndex = Dust.NewDust(spawnPosition, 0, 0, 226, -10 * (float)Math.Cos(rotToTarget), -10 * (float)Math.Sin(rotToTarget), 0, Color.Transparent, 0.9f);
                            Main.dust[dustIndex].velocity /= 12f;
                            Main.dust[dustIndex].fadeIn = 0.3f;
                        }
                        Main.dust[dustIndex].velocity *= -0.1f;
                    }
                    if (player.itemAnimation == 0) projectile.Kill();
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects effect = (SpriteEffects)((Main.player[projectile.owner].direction == 1) ? 0 : 1);
            spriteBatch.Draw(mod.GetTexture("Projectiles/Greatbows/TeravoltGreatbow_Glow"), projectile.Center - Main.screenPosition, new Rectangle(0, mod.GetTexture("Projectiles/Greatbows/Glow1").Height / 5 * projectile.frame, projectile.width, mod.GetTexture("Projectiles/Greatbows/TeravoltGreatbow_Glow").Height / 5), new Color(255, 255, 255), projectile.rotation, new Vector2(projectile.width / 2, mod.GetTexture("Projectiles/Greatbows/TeravoltGreatbow_Glow").Height / 10), projectile.scale, effect, 0f);
        }
    }
}