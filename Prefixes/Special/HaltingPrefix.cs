using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class HaltingPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Halting";

        public override float Rarity => 0.7f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            target.velocity = Vector2.Zero; //todo: might be too strong!
        }
    }
}
