using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class ReaperTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Reaper";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
            npc.defense /= 2;
            npc.lifeMax = (int)(npc.lifeMax * .666);
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (damage < target.statLife)
            {
                damage = target.statLife - 1 + target.statDefense / 2;
            }
        }

        public override void AI(NPC npc)
        {
            if (Main.rand.Next(0, 6) == 0)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.PortalBoltTrail);
            }
        }

        public override int GetShaderID(NPC nPC) => GameShaders.Armor.GetShaderIdFromItemId(ItemID.GrimDye);
    }
}
