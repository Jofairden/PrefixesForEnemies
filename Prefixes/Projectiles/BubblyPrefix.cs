using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace EnemyMods.Prefixes.Projectiles
{
    public class BubblyPrefix : NPCPrefix
    {
        public override string Type => ProjectilesGroup.NAME;

        public override string Name => "Bubbly";

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

            if (distance < 250)
            {
                TriggerTimer = Math.Min(TriggerTimer + Main.rand.Next(1, 3), 90);
                if (TriggerTimer >= 90 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Vector2 vector82 = -target.Center + npc.Center;
                    vector82 = -vector82;
                    Vector2 vector83 = Vector2.Normalize(vector82) * 9f;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vector83.X, vector83.Y, 410, (int)((npc.damage * .7) * ProjectilesGroup.DamageMultiplier), 0);
                    Main.projectile[p].scale *= 1.4f;
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].timeLeft = 90;
                    TriggerTimer = 0;
                }
            }
        }
    }
}
