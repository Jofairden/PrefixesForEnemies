using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class SmallPrefix : NPCPrefix
    {
        public override string Type => StatsGroup.NAME;

        public override string Name => "Small";

        public override float Rarity => 1.0f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.scale *= 0.7f;
        }
    }
}
