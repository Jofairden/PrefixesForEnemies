using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class ExecutionerPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Executioner";

        public override float Rarity => 0.7f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (target.statLife <= target.statLifeMax2 / 5)
            {
                damage *= 2;
            }
        }
    }
}
