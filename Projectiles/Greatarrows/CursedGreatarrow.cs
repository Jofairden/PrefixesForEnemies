using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class CursedGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
            projectile.maxPenetrate = 3;
            projectile.penetrate = 3;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Greatarrow");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] == 1f)
            {
                target.AddBuff(BuffID.CursedInferno, 600);
            }
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 1f && projectile.damage != 0)
            {
                projectile.localAI[1]++;
                if(projectile.localAI[1] % 3 == 0)
                {
                    int p = Projectile.NewProjectile(projectile.Center.X - projectile.velocity.X * 2, projectile.Center.Y - projectile.velocity.Y * 2, 0, 0, 101, (int)(projectile.damage * 0.33), 0f, projectile.owner, 0f, 0f);
                    Main.projectile[p].maxPenetrate -= 1;
                    Main.projectile[p].penetrate -= 1;
                    Main.projectile[p].extraUpdates = 0;
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = false;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 68, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("CursedGreatarrow"), 1, false, 0, false, false);
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