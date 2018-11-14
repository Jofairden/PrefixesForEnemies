using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.Rare
{
    public class RarePrefix : NPCPrefix
    {
        public override string Type => RareGroup.NAME;

        public override string Name => "Rare";

        public override float Rarity => 1.0f;

        public override void OnCreate(NPC npc)
        {
            npc.lifeMax *= 2;
            npc.value *= 1.5f;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            Lighting.AddLight(npc.position, 0.415f, 0.343f, 0.108f);
            if (Main.rand.Next(20) == 0)
            {
                int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, DustID.GoldCoin, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2f);
            }
        }

        public override void NPCLoot(NPC npc)
        {
            //if (prefixes.Contains("Rare "))
            //{
            //    if (NPC.downedAncientCultist)
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AmberToken"));
            //    }
            //    else if (NPC.downedPlantBoss)
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RubyToken"));
            //    }
            //    else if (NPC.downedMechBossAny)
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EmeraldToken"));
            //    }
            //    else if (Main.hardMode)
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SapphireToken"));
            //    }
            //    else if (NPC.downedBoss3 || NPC.downedQueenBee)
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TopazToken"));
            //    }
            //    else
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AmethystToken"));
            //    }
            //}
        }

        public override int GetShaderID(NPC nPC) => GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveGoldDye);
    }
}
