using Terraria;

namespace EnemyMods.Prefixes.Groups
{
    public class PostMoonlordGroup : PrefixGroup
    {
        public static readonly string NAME = "PostMoonlordPrefix";

        public override string Name => NAME;

        public override bool CanTrigger(NPC npc) => NPC.downedMoonlord;

        public override bool IsTriggered()
        {
            return Main.rand.Next(0, 10) == 0;
        }
    }
}
