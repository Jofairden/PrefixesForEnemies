using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.PostMoonlord
{
    public class MaleficPrefix : NPCPrefix
    {
        public override string Type => PostMoonlordGroup.NAME;

        public override string Name => "Malefic";

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

            TriggerTimer += 800 / distance;
            if (TriggerTimer > 60)
            {
                if (target.statLife > 1)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);
                    target.statLife -= 1;
                    CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 50, target.width, target.height), new Color(255, 65, 0, 255), "" + 1);
                }
                TriggerTimer = 0;
            }
        }
    }
}
