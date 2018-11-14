using Terraria;

namespace EnemyMods.Prefixes.Groups
{
    public class SpecialGroup : PrefixGroup
    {
        public static readonly string NAME = "SpecialPrefix";

        public override string Name => NAME;

        public override bool IsTriggered()
        {
            return Main.rand.Next(0, 6) == 0;
        }
    }
}
