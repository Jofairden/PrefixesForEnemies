using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class FrozenPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Frozen";

        public override float Rarity => 0.7f;

        public override bool IsAllowed(NPC npc)
        {
            return Main.hardMode;
        }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.coldDamage = true;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 300);
            if (Main.rand.Next(1, 12) == 1)
            {
                target.AddBuff(BuffID.Frozen, 120);
            }
        }
    }
}
