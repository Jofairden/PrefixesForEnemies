using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class MiniaturePrefix : NPCPrefix
    {
        public override string Type => StatsGroup.NAME;

        public override string Name => "Miniature";

        public override float Rarity => 0.5f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.scale *= .45f;
            npc.damage = (int)(npc.damage * .8);
        }
    }
}
