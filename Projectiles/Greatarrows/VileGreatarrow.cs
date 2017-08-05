using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class VileGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
            projectile.ranged = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vile Greatarrow");
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] == 1f)
            {
                target.AddBuff(BuffID.Poisoned, 1800);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 7, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("VileGreatarrow"), 1, false, 0, false, false);
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