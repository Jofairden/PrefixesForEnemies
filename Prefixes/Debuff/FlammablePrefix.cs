using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Weakness
{
    public class FlammablePrefix : NPCPrefix
    {
        public override string Type => DebuffGroup.NAME;

        public override string Name => "Flammable";

        public override float Rarity => 1.0f;

        public int DebuffTimer { get; private set; } = 0;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void AI(NPC npc)
        {
            //todo: shouldn't these be in the OnCreate?
            npc.buffImmune[BuffID.OnFire] = false;
            npc.buffImmune[BuffID.CursedInferno] = false;

            //todo: should this really trigger twice, if both buffs apply?
            int buffindex = npc.FindBuffIndex(BuffID.OnFire);
            if (buffindex != -1 && npc.buffTime[buffindex] > 0)
            {
                DebuffTrigger(npc);
            }
            buffindex = npc.FindBuffIndex(BuffID.CursedInferno);
            if (buffindex != -1 && npc.buffTime[buffindex] > 0)
            {
                DebuffTrigger(npc);
            }
        }

        private void DebuffTrigger(NPC npc)
        {
            DebuffTimer++;
            if (DebuffTimer >= 15)
            {
                npc.life -= 4;
                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 30, npc.width, npc.height), new Color(255, 140, 0, 255), "" + 4);
                if (npc.realLife != -1)
                {
                    npc = Main.npc[npc.realLife];
                }
                if (npc.life <= 0)
                {
                    npc.life = 1;
                    if (Main.netMode != 1) //if not client
                    {
                        npc.StrikeNPC(9999, 0f, 0, false, false);
                        if (Main.netMode == 2)
                        {//is this still necessary?
                            NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 1f, 0f, 0f, 9999);
                        }
                    }
                }
                DebuffTimer = 0;
            }
        }
    }
}
