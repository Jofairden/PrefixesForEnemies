using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class VampiricPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Vampiric";

        public override float Rarity => 0.8f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            npc.life += damage; //this could be more.... npc have biiig hitpoints
            CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 50, npc.width, npc.height), new Color(20, 120, 20, 200), "" + damage);
            if (npc.life > npc.lifeMax)
            {
                npc.life = npc.lifeMax;
            }
        }
    }
}
