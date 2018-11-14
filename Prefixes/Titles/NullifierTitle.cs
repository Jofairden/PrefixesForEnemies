using EnemyMods.Prefixes.Groups;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class NullifierTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Nullifier";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public override bool IsAllowed(NPC npc) => Main.hardMode;

        public int TriggerTimer { get; private set; }

        public override int GetShaderID(NPC nPC) => GameShaders.Armor.GetShaderIdFromItemId(ItemID.PurpleOozeDye);

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }
        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            for (int i = 0; i < target.buffType.Length; i++)
            {
                target.DelBuff(i);
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.target == 255)
            {
                return;
            }
            Player target = Main.player[npc.target];

            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (Main.rand.Next(0, 6) == 0)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.CrystalPulse2);
            }
            if (distance < 250)
            {
                TriggerTimer++;
                if (TriggerTimer > 15)
                {
                    target.AddBuff(BuffID.Cursed, 20);
                    TriggerTimer = 0;
                }
            }
        }
    }
}
