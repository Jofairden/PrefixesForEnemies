using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class TricksterPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Trickster";

        public override float Rarity => 0.6f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Confused, 120);
        }
    }
}
