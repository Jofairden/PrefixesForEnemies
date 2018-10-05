using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class MagebanePrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Magebane";

        public override float Rarity => 0.7f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            damage += target.statMana / 4;
            target.statMana /= 2;
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 50, target.width, target.height), new Color(20, 20, 120, 200), "" + target.statMana);
        }
    }
}
