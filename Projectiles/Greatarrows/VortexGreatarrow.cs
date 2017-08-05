using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class VortexGreatarrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 7;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Greatarrow");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] == 1f)
            {
                projectile.damage = (int)(projectile.damage * 1.1);
                projectile.localAI[1]++;
                if (!target.boss)
                {
                    if(target.realLife != -1)
                    {
                        NPC realTarget = Main.npc[target.realLife];
                        if (!realTarget.boss)
                        {
                            realTarget.AddBuff(mod.BuffType("Suspended"), 90 + (int)projectile.localAI[1] * 30);
                        }
                    }
                    else
                        target.AddBuff(mod.BuffType("Suspended"), 90 + (int)projectile.localAI[1]*30);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.boss)
            {
                damage = (int)(damage * 1.3);
            }
        }
        public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.7f, 0.9f, 0.9f);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Electric, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("VortexGreatarrow"), 1, false, 0, false, false);
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