using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class AzureGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Azure Greatarrow");
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 2f)
            {
                Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.4f, 0.4f, 0.9f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.localAI[0] == 1f)
            {
                projectile.aiStyle = 0;
                projectile.velocity = Vector2.Zero;
                projectile.localAI[0] = 2f;
                projectile.timeLeft = 60;
                projectile.penetrate = 1;
                return false;
            }
            if (projectile.localAI[0] == 2f)
            {
                projectile.velocity = Vector2.Zero;
                return false;
            }
                return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            if(projectile.localAI[0] != 2f)
                for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && projectile.localAI[0] != 2f && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("AzureGreatarrow"), 1, false, 0, false, false);
            }
            if (projectile.localAI[0] == 2f && timeLeft < 2)
            {
                for (int num259 = 0; num259 < 40; num259++)
                {
                    int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Normalize();
                    Main.dust[d].velocity *= 8;
                }
                projectile.position = projectile.Center;
                projectile.width = (projectile.height = 160);
                projectile.Center = projectile.position;
                projectile.penetrate = -1;
                projectile.damage = (int)(projectile.damage * 1.4);
                projectile.Damage();
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(projectile.localAI[0]);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            projectile.localAI[0] = (float)reader.ReadDouble();
        }
    }
}