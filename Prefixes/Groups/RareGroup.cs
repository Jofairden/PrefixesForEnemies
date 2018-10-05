using Terraria;

namespace EnemyMods.Prefixes.Groups
{
    public class RareGroup : PrefixGroup
    {
        public static readonly string NAME = "RarePrefix";

        public override string Name => NAME;

        public override bool CanTrigger(NPC npc)
        {
            return (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163;
        }

        public override bool IsTriggered() => Main.rand.Next(0, Main.expertMode ? 80 : 100) == 0;
    }
}
