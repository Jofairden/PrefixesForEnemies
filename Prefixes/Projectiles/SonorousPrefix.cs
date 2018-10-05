using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace EnemyMods.Prefixes.Projectiles
{
    public class SonorousPrefix : NPCPrefix
    {
        public override string Type => ProjectilesGroup.NAME;

        public override string Name => "Sonorous";

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
                TriggerTimer = Math.Min(TriggerTimer + Main.rand.Next(1, 3), 150);
                if (TriggerTimer >= 150 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Vector2 vector82 = -target.Center + npc.Center;
                    vector82 = -vector82;
                    Vector2 vector83 = Vector2.Normalize(vector82) * 4f;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vector83.X, vector83.Y, Main.rand.Next(76, 79), (int)((npc.damage * .9) * ProjectilesGroup.DamageMultiplier), 0);
                    Main.projectile[p].damage /= 2;
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].timeLeft = 300;
                    TriggerTimer = 0;
                }
            }
        }
    }
}
