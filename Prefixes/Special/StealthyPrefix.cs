using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class StealthyPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Stealthy";

        public override float Rarity => 1.0f;

        public override bool IsAllowed(NPC npc) => npc.type != 266; //todo MAGIC NUMBER

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            npc.alpha -= 100;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            npc.alpha -= 100;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
        }

        public override void AI(NPC npc)
        {
            npc.alpha++;
            if (npc.alpha > 240)
            {
                npc.alpha = 240;
            }
        }
    }
}
