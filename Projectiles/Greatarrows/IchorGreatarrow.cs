using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class IchorGreatarrow : ModProjectile
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
            DisplayName.SetDefault("Ichor Greatarrow");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] == 1f && projectile.penetrate <= 1)
            {
                int p = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X/4, projectile.velocity.Y/4, ProjectileID.IchorSplash, projectile.damage, projectile.knockBack/2);
            }
            target.AddBuff(BuffID.Ichor, 600 * (int)(1 + projectile.localAI[0]));
        }
        public override void AI()
        {
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 228, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("IchorGreatarrow"), 1, false, 0, false, false);
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