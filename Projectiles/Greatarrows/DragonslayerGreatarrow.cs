using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.IO;

namespace EnemyMods.Projectiles.Greatarrows
{
    public class DragonslayerGreatarrow : ModProjectile
    {
        private bool noApply = false;
        public override void SetDefaults()
        {
            projectile.width = 11;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.timeLeft = 2400;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragonslayer Greatarrow");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[0] == 1f && !noApply)
            {
                target.AddBuff(mod.BuffType("Dragonslay"), 300);
            }
            noApply = false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //apply buff OR clear buff and add damage
            if (projectile.localAI[0] == 1f)
            {
                if (target.FindBuffIndex(mod.BuffType("Dragonslay")) >= 0)
                {
                    damage += (int)(Math.Min(target.lifeMax / 20, 500) * Main.player[projectile.owner].rangedDamage);
                    for(int i=0; i<30; i++)
                    {
                        Dust.NewDust(projectile.position, projectile.width, projectile.height, 183, 0f, 0f, 0, default(Color), 1.5f);
                    }
                    target.DelBuff(target.FindBuffIndex(mod.BuffType("Dragonslay")));
                    noApply = true;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 7, 0f, 0f, 0, default(Color), 1f);
            }
            if (Main.rand.Next(0, 2) == 0 && !projectile.noDropItem)
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("DragonslayerGreatarrow"), 1, false, 0, false, false);
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