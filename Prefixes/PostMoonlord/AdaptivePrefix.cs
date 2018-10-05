using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.PostMoonlord
{
    public class AdaptivePrefix : NPCPrefix
    {
        public override string Type => PostMoonlordGroup.NAME;

        public override string Name => "Adaptive";

        public override float Rarity => 1.0f;

        private bool rangedResist, meleeResist, magicResist, minionResist;
        private int rangedResistTimer, meleeResistTimer, magicResistTimer, minionResistTimer = 0;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (projectile.melee)
            {
                CalculateMeleeResist(ref damage);
            }
            if (projectile.ranged)
            {
                CalculateRangedResist(ref damage);
            }
            if (projectile.magic)
            {
                CalculateMagicResist(ref damage);
            }
            if (projectile.minion)
            {
                CalculateMinionResist(ref damage);
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (item.melee)
            {
                CalculateMeleeResist(ref damage);
            }
            if (item.ranged)
            {
                CalculateRangedResist(ref damage);
            }
            if (item.magic)
            {
                CalculateMagicResist(ref damage);
            }
            if (item.summon)
            {
                CalculateMinionResist(ref damage);
            }
        }

        public override void AI(NPC npc)
        {
            minionResistTimer--;
            if (minionResistTimer < 0)
                minionResist = false;
            magicResistTimer--;
            if (magicResistTimer < 0)
                magicResist = false;
            meleeResistTimer--;
            if (meleeResistTimer < 0)
                meleeResist = false;
            rangedResistTimer--;
            if (rangedResistTimer < 0)
                rangedResist = false;
        }

        private void CalculateMeleeResist(ref int damage)
        {
            if (meleeResist)
            {
                damage = (int)(damage * .3);
            }
            meleeResist = true;
            meleeResistTimer = 180;
        }

        private void CalculateRangedResist(ref int damage)
        {
            if (rangedResist)
            {
                damage = (int)(damage * .3);
            }
            rangedResist = true;
            rangedResistTimer = 180;
        }

        private void CalculateMagicResist(ref int damage)
        {
            if (magicResist)
            {
                damage = (int)(damage * .3);
            }
            magicResist = true;
            magicResistTimer = 180;
        }

        private void CalculateMinionResist(ref int damage)
        {
            if (minionResist)
            {
                damage = (int)(damage * .3);
            }
            minionResist = true;
            minionResistTimer = 180;
        }


    }
}
