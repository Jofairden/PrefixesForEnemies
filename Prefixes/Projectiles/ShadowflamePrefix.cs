using EnemyMods.Prefixes.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace EnemyMods.Prefixes.Projectiles
{
    public class ShadowflamePrefix : NPCPrefix
    {
        public override string Type => ProjectilesGroup.NAME;

        public override string Name => "Shadowflame";

        public override float Rarity => 1.0f;

        public int TriggerTimer { get; private set; }

        public override bool IsAllowed(NPC npc) => Main.hardMode;

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

            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (distance < 450)
            {
                TriggerTimer++;
                if (TriggerTimer > 360)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 4, -(npc.position.Y - target.position.Y) / distance * 4, 299, (int)((npc.damage) * ProjectilesGroup.DamageMultiplier), 0);
                    TriggerTimer = 0;
                }
            }
        }
    }
}
