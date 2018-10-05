using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes
{
    public class ToxiphobicPrefix : NPCPrefix
    {
        public override string Type => DebuffGroup.NAME;

        public override string Name => "Toxiphobic";

        public override float Rarity => 1.0f;

        public int PoisonTimer { get; private set; }
        public int VenomTimer { get; private set; }


        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            npc.buffImmune[BuffID.Poisoned] = false;
            npc.buffImmune[BuffID.Venom] = false;
            int buffindex = npc.FindBuffIndex(BuffID.Poisoned);
            if (buffindex != -1 && npc.buffTime[buffindex] > 0)
            {
                PoisonTimer++;
                if (PoisonTimer >= 15)
                {
                    npc.life -= (int)(Math.Sqrt(npc.lifeMax - npc.life) / 16) + 2;
                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 30, npc.width, npc.height), new Color(255, 140, 0, 255), "" + ((int)(Math.Sqrt(npc.lifeMax - npc.life) / 16) + 2));
                    if (npc.realLife != -1) { npc = Main.npc[npc.realLife]; }
                    if (npc.life <= 0)
                    {
                        npc.life = 1;
                        if (Main.netMode != 1)
                        {
                            npc.StrikeNPC(9999, 0f, 0, false, false);
                            if (Main.netMode == 2) { NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 1f, 0f, 0f, 9999); }
                        }
                    }
                    PoisonTimer = 0;
                }
            }
            buffindex = npc.FindBuffIndex(BuffID.Venom);
            if (buffindex != -1 && npc.buffTime[buffindex] > 0)
            {
                VenomTimer++;
                if (VenomTimer >= 15)
                {
                    npc.life -= (int)(Math.Sqrt(npc.lifeMax - npc.life) / 8) + 1;
                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 30, npc.width, npc.height), new Color(255, 140, 0, 255), "" + ((int)(Math.Sqrt(npc.lifeMax - npc.life) / 8) + 1));
                    if (npc.realLife != -1) { npc = Main.npc[npc.realLife]; }
                    if (npc.life <= 0)
                    {
                        npc.life = 1;
                        if (Main.netMode != 1)
                        {
                            npc.StrikeNPC(9999, 0f, 0, false, false);
                            if (Main.netMode == 2) { NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 1f, 0f, 0f, 9999); }
                        }
                    }
                    VenomTimer = 0;
                }
            }
        }
    }
}
