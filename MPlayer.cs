using EnemyMods.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EnemyMods
{
    public class MPlayer : ModPlayer
    {
        public int voidTargetDamage = 0;
        public int voidBurn = 0;
        public float voidAffinity = 1f;

        //cooldowns and charges by index of spell, by tier then alphabetically
        

        public override void Initialize()
        {
        }

        public override void ResetEffects()
        {
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            int index = player.FindBuffIndex(mod.BuffType("VoidBurn"));
            if (index != -1)
            {
                player.buffTime[index] -= (5 + damage);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            int index = player.FindBuffIndex(mod.BuffType("VoidBurn"));
            if(index != -1)
            {
                player.buffTime[index] -= (5 + damage);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (player.FindBuffIndex(mod.BuffType("VoidBurn")) == -1)
            {
                voidBurn = 0;
            }
            else
            {
                player.lifeRegenTime = 0;
                if(player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegen -= voidBurn;
            }
        }
    }
}
