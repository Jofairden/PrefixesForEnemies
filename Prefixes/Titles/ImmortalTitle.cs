using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class ImmortalTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Immortal";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public int Lives { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
            Lives = 8;
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }

        public override bool CheckDead(NPC npc)
        {
            if (Lives > 0)
            {
                Lives--;
                npc.damage = (int)(npc.damage * 1.1);
                npc.life = npc.lifeMax;
                Main.PlaySound(15, npc.position, 0);
                return false;
            }
            return true;
        }
    }
}
