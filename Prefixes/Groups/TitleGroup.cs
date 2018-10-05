using Terraria;

namespace EnemyMods.Prefixes.Groups
{
    public class TitleGroup : PrefixGroup
    {
        public static readonly string NAME = "TitleGroup";

        public override string Name => NAME;

        public override bool CanTrigger(NPC npc)
        {
            return NPC.downedBoss3 && (!npc.boss && npc.type != 36 && npc.type != 114 && !(npc.type >= 128 && npc.type <= 131) && !(npc.type >= 246 && npc.type <= 248));
        }

        public override bool IsTriggered() => Main.rand.Next(0, 100) == 0;

    }
}
