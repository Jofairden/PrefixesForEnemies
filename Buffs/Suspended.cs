using System;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Suspended : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            //potential conditionals to keep this from breaking enemies
            npc.velocity.Y = -Math.Max(npc.buffTime[buffIndex] / 65, .5f);
            npc.velocity.X = 0;
        }
    }
}
