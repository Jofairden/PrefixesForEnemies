using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class ColossalPrefix : NPCPrefix
    {
        public override string Type => StatsGroup.NAME;

        public override string Name => "Colossal";

        public override float Rarity => 0.5f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
            npc.scale *= 1.8f;
            npc.damage = (int)(npc.damage * 1.2);
        }
    }
}
