using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Stunned : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            //potential conditionals to keep this from breaking enemies
            npc.velocity.Y = 0;
            npc.velocity.X = 0;
            if (npc.buffTime[buffIndex] % 4 < 2)
            {
                npc.position.X += -4;
            }
            else
            {
                npc.position.X += 4;
            }
        }
    }
}
