using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class NecromancerTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Necromancer";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public int TriggerTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }

        public override void AI(NPC npc)
        {
            TriggerTimer++;
            if (TriggerTimer > 480)
            {
                if (NPC.downedPlantBoss)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, 269 + Main.rand.Next(0, 17));//269-286
                }
                else if (Main.hardMode)
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.ArmoredSkeleton + Main.rand.Next(0, 1) * 33);//77 or 110
                }
                else
                {
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Skeleton);
                }
                for (int i = 0; i < 10; i++)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);
                }
              
                Main.PlaySound(SoundID.Item, npc.position, 8);
                TriggerTimer = 0;
            }
        }
    }
}
