using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Groups
{
    public class ProjectilesGroup : PrefixGroup
    {
        public static readonly string NAME = "ProjectilesPrefix";

        public override string Name => NAME;

        public override bool CanTrigger(NPC npc) => npc.type != NPCID.Creeper && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3);

        public override bool IsTriggered()
        {
            return Main.rand.Next(0, 12 + (Main.hardMode ? 0 : 8)) == 0;
        }

        public static float DamageMultiplier => (Main.hardMode ? 0.5f : .4f) / (Main.expertMode ? Main.expertDamage : 1);
    }
}
