namespace EnemyMods.Prefixes.Groups
{
    public class VoidGroup : PrefixGroup
    {
        public static readonly string NAME = "VoidPrefix";

        public override string Name => NAME;

        public override bool IsTriggered()
        {
            return false;//HAS NO ACTIVE MEMBERS FOR NOW
        }
    }
}
