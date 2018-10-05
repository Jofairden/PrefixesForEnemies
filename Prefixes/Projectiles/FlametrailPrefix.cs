using EnemyMods.Prefixes.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace EnemyMods.Prefixes.Projectiles
{
    public class FlametrailPrefix : NPCPrefix
    {
        public override string Type => ProjectilesGroup.NAME;

        public override string Name => "Flametrail";

        public override float Rarity => 1.0f;

        public int TriggerTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            if (npc.target == 255 || Main.netMode == 2)
            {
                return;
            }
            Player target = Main.player[npc.target];

            TriggerTimer++;
            if (TriggerTimer > 15)
            {
                int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, 400 + Main.rand.Next(0, 3), (int)(npc.damage * ProjectilesGroup.DamageMultiplier), 0);
                Main.projectile[p].friendly = false;
                Main.projectile[p].hostile = true;
                TriggerTimer = 0;
            }
        }
    }
}
