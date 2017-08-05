using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items
{
    public class GItem : GlobalItem
    {
        public int numNeedles = 0;
        public int timeToNeedle = 0;
        public override bool CloneNewInstances => true;
        public override bool InstancePerEntity => true;
    }
}
