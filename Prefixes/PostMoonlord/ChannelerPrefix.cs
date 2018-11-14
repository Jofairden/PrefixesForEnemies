using EnemyMods.NPCs;
using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.PostMoonlord
{
    public class ChannelerPrefix : NPCPrefix
    {
        public override string Type => PostMoonlordGroup.NAME;

        public override string Name => "Channeler";

        public override float Rarity => 1.0f;

        public int TriggerTimer { get; private set; }
        public int TriggerCounter { get; private set; }

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


            TriggerTimer += 1000 / distance;
            if (TriggerTimer > 600)
            {
                target.statMana = Math.Max(target.statMana - 10, 0);
                TriggerTimer++;
                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 50, target.width, target.height), new Color(0, 30, 200, 255), "" + 10);
                TriggerTimer = 0;
            }
            if (TriggerCounter >= 5)
            {
                TriggerCounter = 0;

                foreach (NPC n in NPCUtils.GetNPCsInRange(npc, 400))
                {
                    n.damage = (int)(n.damage * 1.1);
                    n.knockBackResist = (float)(n.knockBackResist / 1.1);
                    n.lifeMax = (int)(n.lifeMax * 1.1);
                    n.life = Math.Min(n.life + 200, n.lifeMax);
                    CombatText.NewText(new Rectangle((int)n.position.X, (int)n.position.Y - 20, n.width, n.height), new Color(50, 255, 50, 255), "" + 200);
                    CombatText.NewText(new Rectangle((int)n.position.X, (int)n.position.Y - 50, n.width, n.height), new Color(255, 65, 0, 255), "Status Up!");
                }
            }
        }
    }
}
