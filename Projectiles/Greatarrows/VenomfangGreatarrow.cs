using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class VenomfangGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 9;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
            projectile.maxPenetrate = 10;
            projectile.penetrate = 10;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venomfang Greatarrow");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Venom, 180 * (int)(1 + projectile.localAI[0]) * 10);
        }
        public override void AI()
        {
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.localAI[0] == 1f)
            {
                for (int num385 = 0; num385 < 3; num385++)
                {
                    float num386 = -projectile.velocity.X * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float num387 = -projectile.velocity.Y * Main.rand.Next(30, 60) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    int p = Projectile.NewProjectile(projectile.position.X + num386, projectile.position.Y + num387, num386, num387, ProjectileID.VenomFang, (int)(projectile.damage * 0.5), 0f, projectile.owner);
                    Main.projectile[p].timeLeft = 90;
                }
            }          
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.ToxicBubble, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("VenomfangGreatarrow"), 1, false, 0, false, false);
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