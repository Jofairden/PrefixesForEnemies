using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class VengefulPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Vengeful";

        public override float Rarity => 0.8f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            damage += (int)((npc.life / (npc.life + npc.lifeMax)) * damage);
        }
    }
}
