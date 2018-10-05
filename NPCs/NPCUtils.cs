using System;
using System.Collections.Generic;
using Terraria;

namespace EnemyMods.NPCs
{
    public static class NPCUtils
    {
        //worm-type heads to be specially included
        private static readonly int[] types = { 7, 10, 13, 39, 87, 95, 98, 117, 134, 402, 412, 454, 510, 513 }; 

        public static List<NPC> GetNPCsInRange(NPC focus, int distance)
        {
            List<NPC> NPCsInRange = new List<NPC>();
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.Distance(focus.position) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37) ^ Array.Exists(types, element => element == npc.type)) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc != focus)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
        }

    }
}
