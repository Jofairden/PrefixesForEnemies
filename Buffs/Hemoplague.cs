using System;
using System.Collections;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Hemoplague : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 10 + ((npc.FindBuffIndex(mod.BuffType("Bloodied"))>=0) ? 6 : 0);
            //todo visual effects
            if (npc.buffTime[buffIndex] % 15 == 0)
            {
                ArrayList npcs = getNPCsInRange(npc, 200);
                foreach (NPC n in npcs)
                {
                    n.AddBuff(mod.BuffType("Hemoplague"), npc.buffTime[buffIndex]);
                }
            }
            if(npc.buffTime[buffIndex] == 1)
            {
                int damage = Math.Min(300, (int)((npc.lifeMax - npc.life)*.1));//10% missing health, max 300
                damage += npc.defense;//deals some bonus damage to high defense enemies
                npc.StrikeNPC(damage, 0, 0);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, damage, 0, 0, 0);
                //todo visual effects
            }
        }
        private ArrayList getNPCsInRange(NPC focus, int distance)
        {
            ArrayList NPCsInRange = new ArrayList();
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.Distance(focus.position) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc.active)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
        }
    }
}
