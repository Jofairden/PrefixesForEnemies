using System;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Projectiles.BloodMagic
{
    public class BloodSword : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 54;
            projectile.timeLeft = 70;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Sword");
        }
        public override void AI()
        {
            int p = (int)projectile.ai[0];//blood well index
            Projectile proj = Main.projectile[p];
            //Factors for calculations
            double deg = (double)projectile.ai[1]*6; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 32 * proj.scale/2; //Distance away from well
            projectile.scale = proj.scale/2;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            projectile.position.X = proj.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
            projectile.position.Y = proj.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            projectile.ai[1] += 1f;
            projectile.rotation = projectile.AngleFrom(Main.projectile[p].position) + .785f;
            if (Main.rand.Next(0, 2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, 0, 0, 100, default(Color), 1.2f);
            }
        }
        /*public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            projHitbox.X -= (int)(projHitbox.Width * (projectile.scale - 1));
            projHitbox.Y -= (int)(projHitbox.Height * (projectile.scale - 1));
            projHitbox.Width = (int)(projHitbox.Width * projectile.scale);
            projHitbox.Height = (int)(projHitbox.Height * projectile.scale);
            return Colliding(projHitbox, targetHitbox);
            //return base.Colliding(projHitbox, targetHitbox);
        }*/
        public override void Kill(int timeLeft)
        {
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 5, 0f, 0f, 0, default(Color), 1f);
            }
            //Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Bloodied"), 360);
        }
    }
}
