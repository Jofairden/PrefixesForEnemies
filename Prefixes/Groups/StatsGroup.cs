using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Groups
{
    public class StatsGroup : PrefixGroup
    {
        public static readonly string NAME = "StatsPrefix";

        public override string Name => NAME;

        public override bool CanTrigger(NPC npc)
        {
            return !(npc.type == NPCID.Golem || NPCID.GolemHead == npc.type);
        }

        public override bool IsTriggered()
        {
            return Main.rand.Next(0, 4) == 0;
        }
    }
}
