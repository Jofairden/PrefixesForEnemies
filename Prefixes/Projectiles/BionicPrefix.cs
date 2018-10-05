using EnemyMods.Prefixes.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Projectiles
{
    public class BionicPrefix : NPCPrefix
    {
        public override string Type => ProjectilesGroup.NAME;

        public override string Name => "Bionic";

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

            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (distance < 450)
            {
                TriggerTimer++;
                if (TriggerTimer >= 240 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 9, -(npc.position.Y - target.position.Y) / distance * 9, ProjectileID.DeathLaser, (int)((npc.damage) * ProjectilesGroup.DamageMultiplier), 0);
                    TriggerTimer = 0;
                }
            }
        }
    }
}
