using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class PetrifyingPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Petrifying";

        public override float Rarity => 0.5f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (Main.rand.Next(0, 3) == 0 && target.FindBuffIndex(BuffID.Stoned) == -1)
            {
                target.AddBuff(BuffID.Stoned, 180);
            }
        }
    }
}
