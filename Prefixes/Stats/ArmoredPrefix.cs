using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class ArmoredPrefix : NPCPrefix
    {
        public override string Type => StatsGroup.NAME;

        public override string Name => "Armored";

        public override float Rarity => 1.0f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.defense = (int)(1.2 * npc.defense + 4);
        }
    }
}
