using EnemyMods.Prefixes.Groups;
using System;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class RegeneratingPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Regenerating";

        public override float Rarity => 1.0f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.FindBuffIndex(BuffID.OnFire) == -1 && npc.FindBuffIndex(BuffID.Poisoned) == -1 && npc.FindBuffIndex(BuffID.Venom) == -1 && npc.FindBuffIndex(BuffID.CursedInferno) == -1)
            {
                npc.lifeRegen += (int)Math.Sqrt(npc.lifeMax - npc.life) / 2 + 1;
            }
        }
    }
}
