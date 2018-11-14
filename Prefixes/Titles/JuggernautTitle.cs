using EnemyMods.Prefixes.Groups;
using Terraria;

namespace EnemyMods.Prefixes.Stats
{
    public class JuggernautTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Juggernaut";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
            npc.defense *= 2;
            npc.lifeMax *= 3;
            npc.knockBackResist = 0;
            npc.scale *= 1.3f;
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }
    }
}
