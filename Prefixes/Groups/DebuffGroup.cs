using Terraria;

namespace EnemyMods.Prefixes.Groups
{
    public class DebuffGroup : PrefixGroup
    {
        public static readonly string NAME = "DebuffPrefix";

        public override string Name => NAME;

        public override bool IsTriggered()
        {
            return Main.rand.Next(0, 5) == 0;
        }
    }
}
