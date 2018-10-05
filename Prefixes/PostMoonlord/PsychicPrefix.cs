using EnemyMods.NPCs;
using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.PostMoonlord
{
    public class PsychicPrefix : NPCPrefix
    {
        public override string Type => PostMoonlordGroup.NAME;

        public override string Name => "Psychic";

        public override float Rarity => 1.0f;

        public int TriggerTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            if (npc.target == 255)
            {
                return;
            }
            Player target = Main.player[npc.target];

            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (distance < 600)
            {
                TriggerTimer = Math.Min(TriggerTimer + 1, 100);
                if ((TriggerTimer >= 90 || Main.rand.Next(0, 100) == 0) && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    target.velocity += new Vector2(Main.rand.Next(-25, 26) / 2, Main.rand.Next(-25, 26) / 2);
                    TriggerTimer = 0;
                }
            }
        }
    }
}
