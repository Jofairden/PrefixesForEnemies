using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class WingClipperPrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Wing Clipper";

        public override float Rarity => 0.7f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            target.wingTime = 0;
            target.rocketTime = 0;
            target.jumpAgainBlizzard = false;
            target.jumpAgainCloud = false;
            target.jumpAgainFart = false;
            target.jumpAgainSail = false;
            target.jumpAgainSandstorm = false;
            target.jumpAgainUnicorn = false;
        }
    }
}
