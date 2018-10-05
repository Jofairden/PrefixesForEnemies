using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes
{
    public class CryophobicPrefix : NPCPrefix
    {
        public override string Type => DebuffGroup.NAME;

        public override string Name => "Cryophobic";

        public override float Rarity => 1.0f;

        public int DebuffTimer { get; private set; } = 0;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            npc.buffImmune[BuffID.Frostburn] = false;
            int buffindex = npc.FindBuffIndex(BuffID.Frostburn);
            if (buffindex != -1 && npc.buffTime[buffindex] > 0)
            {
                DebuffTimer++;
                if (DebuffTimer >= 30)
                {
                    npc.defense--;
                    if (npc.defense <= 0)
                    {
                        npc.defense++;
                        npc.life -= 4;
                        CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 30, npc.width, npc.height), new Color(255, 140, 0, 255), "" + 4);
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
                    }
                    DebuffTimer = 0;
                }
            }
        }
    }
}
