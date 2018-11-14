using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class EnduringPrefix : NPCPrefix
    {
        public override string Type => StatsGroup.NAME;

        public override string Name => "Enduring";

        public override float Rarity => 1.0f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.takenDamageMultiplier *= .8f;
        }
    }
}
