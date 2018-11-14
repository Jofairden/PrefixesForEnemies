using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class VoodooPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Voodoo";

        public override float Rarity => 1.0f;

        public int SpecialTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            if (npc.target == 255) //MAGIC NUMBER!
            {
                return;
            }
            Player target = Main.player[npc.target];

            SpecialTimer++;
            if (SpecialTimer > 60)
            {
                for (int i = 0; i < npc.buffType.Length; i++)
                {
                    if (npc.buffTime[i] > 0 && npc.buffType[i] > 0)
                    {
                        target.AddBuff(npc.buffType[i], npc.buffTime[i]);
                    }
                }
                SpecialTimer = 0;
            }
        }
    }
}
