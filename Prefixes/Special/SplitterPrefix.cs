using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class SplitterPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Splitter";

        public override float Rarity => 0.7f;

        public override bool IsAllowed(NPC npc) => npc.type != 13 && npc.type != 439 && npc.aiStyle != 94;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void NPCLoot(NPC npc)
        {
            int x = 2 + Main.rand.Next(0, 3);
            for (int i = 0; i < x; i++)
            {
                int n = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, npc.type);
                Main.npc[n].velocity.X = Main.rand.Next(-3, 4);
                Main.npc[n].velocity.Y = Main.rand.Next(-3, 4);
                Main.npc[n].life /= 2;
                Main.npc[n].scale *= .85f;
                Main.npc[n].lifeMax /= 2;
                Main.npc[n].damage = (int)(Main.npc[n].damage * .8);
            }
        }
    }
}
