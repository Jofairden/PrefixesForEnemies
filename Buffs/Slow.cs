using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Slow : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= .9f;
        }
    }
}
