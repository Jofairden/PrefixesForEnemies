using EnemyMods.NPCs;
using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.PostMoonlord
{
    public class LeechingPrefix : NPCPrefix
    {
        public override string Type => PostMoonlordGroup.NAME;

        public override string Name => "Leeching";

        public override float Rarity => 1.0f;

        public int TriggerTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            TriggerTimer += 1;
            if (TriggerTimer >= 250)
            {
                TriggerTimer = 0;

                foreach (NPC n in NPCUtils.GetNPCsInRange(npc, 300))
                {
                    int damC = (int)(n.damage * .1);
                    n.damage = (int)(n.damage * .9);
                    int lifeMaxC = (int)(n.lifeMax * .1);
                    n.lifeMax = (int)(n.lifeMax * .9);
                    int lifeC = (int)(n.life * .1);
                    n.life = (int)(n.life * .9);
                    n.life = Math.Max(1, n.life);
                    npc.lifeMax += lifeMaxC;
                    npc.life += lifeC;
                    npc.damage += Math.Max(damC, 1);
                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 20, npc.width, npc.height), new Color(50, 255, 50, 255), "" + lifeC);
                    CombatText.NewText(new Rectangle((int)n.position.X, (int)n.position.Y - 20, n.width, n.height), new Color(255, 140, 0, 255), "" + lifeC);
                }
            }
        }
    }
}
