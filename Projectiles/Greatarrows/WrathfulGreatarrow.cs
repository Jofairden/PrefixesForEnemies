using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class WrathfulGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
            projectile.maxPenetrate = 10;
            projectile.penetrate = 10;
            projectile.light = 1f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrathful Greatarrow");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(projectile.localAI[0]==1f)
            projectile.ghostHurt(projectile.damage/ (crit ? 1:2), target.Center);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            if (Main.rand.Next(0, 100) < Main.player[projectile.owner].rangedCrit)
            {
                crit = true;
            }
        }
        public override void AI()
        {
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.localAI[0] == 1f)
            {
                for (int num385 = 0; num385 < 2; num385++)
                {
                    projectile.ghostHurt(projectile.damage / 2, projectile.position);
                }
            }
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.AncientLight, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("WrathfulGreatarrow"), 1, false, 0, false, false);
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