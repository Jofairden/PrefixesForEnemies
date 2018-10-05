using Terraria;

namespace EnemyMods.Prefixes
{
    public abstract class PrefixGroup
    {
        public uint ID { get; internal set; }

        public abstract string Name { get; }

        public virtual bool CanTrigger(NPC npc) => true;

        public abstract bool IsTriggered();
    }
}
