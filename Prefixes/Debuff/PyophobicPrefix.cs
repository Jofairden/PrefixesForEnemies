using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Weakness
{
    public class PyophobicPrefix : NPCPrefix
    {
        public override string Type => DebuffGroup.NAME;

        public override string Name => "Pyophobic";

        public override float Rarity => 1.0f;


        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }


        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            int buffindex = npc.FindBuffIndex(69);
            if (buffindex != -1 && npc.buffTime[buffindex] > 0)
            {
                damage *= 1.3;
            }
            return true;
        }

        public override void AI(NPC npc)
        {
            npc.buffImmune[BuffID.Ichor] = false;
        }
    }
}
