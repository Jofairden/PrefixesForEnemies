using EnemyMods.Graphics;
using EnemyMods.Prefixes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EnemyMods.NPCs
{
    public class PrefixNPC : GlobalNPC
    {

        public bool nameConfirmed = false;
        public bool MPSynced = false;
        public bool readyForChecks = false;

        public int parasiteOwner = -1;
        public int parasiteTimeLeft = 0;
        public int voidBurn = 0;//magnitude

        private static readonly int[] types = { 7, 10, 13, 39, 87, 95, 98, 117, 134, 402, 412, 454, 510, 513 };

        public const int AI_FRIENDLY = 7;
        //int[] zeroValTypes = {36, 68, 115, 116, 117, 128, 129, 130, 131, 395,  };
        //int[] doomsayerTypes = {2, 3, 6, 7, 31, 32, 34, 42, 62,  };//types that can recieve the doomsayer suffix, not implemented
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public NPCPrefix[] NewPrefixes { get; internal set; } = Enumerable.Empty<NPCPrefix>().ToArray();

        public override void SetDefaults(NPC npc)
        {
            if (Main.gameMenu || !npc.active)
            {//BECAUSE FUCK THORIUM
                return;
            }
            if (Main.netMode == 1 || npc == null || npc.FullName == null)//if multiplayer, but not server. 1 is client in MP, 2 is server. Prefixes are sent to client by server in MP.
            {
                return;
            }
            //hopefully temporary: prefix AI causes these enemies to immediately despawn under certain conditions
            if (npc.FullName == "DesertScourgeBody" || npc.FullName == "DesertScourgeTail" || npc.FullName == "DevourerofGodsBody" || npc.FullName == "DevourerofGodsTail")
            {
                return;
            }

            //default npc
            if (npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1)
            {
                return;
            }
            //TODO: double check the types of allowances
            //no prefixes for friendlies and critters
            if (npc.aiStyle == AI_FRIENDLY || npc.catchItem > 0 || npc.lifeMax <= 1)
            {
                return;
            }
            //things we shouldn't prefix
            if (npc.type >= 547 && npc.type <= 578 || npc.type == 401 || npc.type == 488 || npc.type == 371)
            {
                return;
            }
            //not sure why these aren't allowed, but they aren't
            if (!((npc.aiStyle != 6 && npc.aiStyle != 37) ^ Array.Exists(types, element => element == npc.type)))
            {
                return;
            }

            List<NPCPrefix> prefixList = new List<NPCPrefix>();

            /*
            if (Main.rand.Next(0, Main.hardMode ? 80 : 300) == 0 && (!npc.boss && npc.type != 36 && npc.type != 114 && !(npc.type >= 128 && npc.type <= 131) && !(npc.type >= 246 && npc.type <= 248)) && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
            {
                npc.GivenName = "Void-Touched " + npc.GivenName;
                prefixes += "Void-Touched #";
                npc.lifeMax = (int)(npc.lifeMax * 1.5);
                npc.value *= 3f;
            }
            
            #region ?????
            if (Main.rand.Next(0, 1000) == 0)//?????
            {
                npc.value *= 1.5f;
                if (npc.GivenName.Equals("Zombie"))
                {
                    npc.GivenName = "Actually-A-Penguin " + npc.GivenName;
                    prefixes += "Actually-A-Penguin #";
                    npc.catchItem = 2205;
                }
                if (npc.GivenName.Equals("Demon Eye"))
                {
                    npc.GivenName = "Actually-A-Bird " + npc.GivenName;
                    prefixes += "Actually-A-Bird #";
                    npc.catchItem = 2015;
                }
            }
            #endregion
            */

            foreach (PrefixGroup group in PrefixVault.GetPrefixGroups()
                .Where(pg => pg.CanTrigger(npc) && pg.IsTriggered()))
            {
                WeightedRandom<NPCPrefix> weighted = new WeightedRandom<NPCPrefix>();
                foreach (NPCPrefix prefix in PrefixVault.GetPrefixes(group.ID)
                    .Where(px => px.IsAllowed(npc)))
                {
                    weighted.Add(prefix, prefix.Rarity);
                }
                prefixList.Add((NPCPrefix)weighted.Get().Clone());
            }

            NewPrefixes = prefixList.ToArray();

            OverrideName(npc);
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.OnCreate(npc);
            }
        }

        public void OverrideName(NPC npc)
        {
            if (npc.whoAmI <= 0) return;

            string baseName = npc.GetFullNetName().ToString();

            string prefixName = NewPrefixes.Where(pf => pf.IsPrefix())
               .Select(pf => pf.Name)
               .Aggregate("", (current, next) => current + $"{next} ");

            string suffixName = NewPrefixes.Where(pf => !pf.IsPrefix())
                .Select(pf => pf.Name)
                .Aggregate("", (current, next) => current + $" {next}");

            npc.GivenName = $"{prefixName}{baseName}{suffixName}";
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            //base.AI(npc); why is this here?

            /*
            if (parasiteTimeLeft > 0)
            {
                Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 1f, .1f, 0.6f);
                if (Main.rand.Next(0, 12) == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkCrystalShard);
                    }
                }
                parasiteTimeLeft--;
            }
            */
        }


        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            //if (prefixes.Contains("Void-Touched"))
            //{
            //    MPlayer pinf = ((MPlayer)target.GetModPlayer(mod, "MPlayer"));
            //    pinf.voidBurn = Math.Max((int)(damage / 25f + target.statLifeMax2 / 200f + 1), pinf.voidBurn);
            //    target.AddBuff(mod.BuffType("VoidBurn"), 480 + 100 * damage);
            //}
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.OnHitPlayer(npc, target, damage, crit);
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.ModifyHitPlayer(npc, target, ref damage, ref crit);
            }
        }

        public override bool CheckDead(NPC npc)
        {
            return NewPrefixes.All(p => p.CheckDead(npc));
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            /*
            if ((parasiteTimeLeft + 1) / 15 > 1)
            {
                npc.lifeRegen -= (int)(125 * Main.player[parasiteOwner].magicDamage * (1 + Main.player[parasiteOwner].magicCrit / 100.0));
                damage += (int)(125 * Main.player[parasiteOwner].magicDamage * (1 + Main.player[parasiteOwner].magicCrit / 100.0) / 8);
                if (Main.rand.Next(0, 8) == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.PinkCrystalShard, 0, 0, 0, default(Color), 1f);
                    }
                }
                if (npc.realLife != -1) { npc = Main.npc[npc.realLife]; }
                if (npc.life <= 0)
                {
                    npc.life = 1;
                    if (Main.netMode >= 1)
                    {
                        npc.StrikeNPC(9999, 0f, 0, false, false);
                        if (Main.netMode == 2) { NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 1f, 0f, 0f, 9999); }
                    }
                }
            }
            */

            //if (voidBurn > 0)
            //{
            //    npc.lifeRegen -= voidBurn / 2 / (prefixes.Contains("Void-Touched") ? 3 : 1);
            //    damage += voidBurn / 16 / (prefixes.Contains("Void-Touched") ? 3 : 1);
            //    if (Main.rand.Next(5) == 0) {
            //        int d = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("VoidDust"), 0, 0, 0, default(Color), 1f);
            //    }
            //}
        }
        public override bool PreNPCLoot(NPC npc)
        {
            //if (npc.type == NPCID.KingSlime && prefixes.Contains("Void-Touched #"))
            //{
            //    npc.type = -1;
            //}
            return base.PreNPCLoot(npc);
        }
        public override void NPCLoot(NPC npc)
        {
            base.NPCLoot(npc);
            //if (parasiteTimeLeft > 0)
            //{
            //    NPC target = getClosestNPC(npc);
            //    if (npc.position == target.position)
            //    {
            //        int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, mod.ProjectileType("NebulaParasite"), 1, 0, parasiteOwner);
            //    }
            //    else if (npc.Distance(target.position) < 800)
            //    {
            //        Vector2 vel = target.position - npc.position;
            //        vel.Normalize();
            //        int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vel.X * 6, vel.Y * 6, mod.ProjectileType("NebulaParasite"), 1, 0, parasiteOwner);
            //    }
            //}
            //if (prefixes.Contains("Void-Touched"))
            //{
            //    if (npc.boss)
            //    {
            //        if (npc.GivenName == "Void King")
            //        {
            //            //drop weapon
            //            //drop lesser void heart
            //        }
            //    }
            //    else
            //    {
            //        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"));
            //    }
            //}
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.OnHitByProjectile(npc, projectile, damage, knockback, crit);
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.OnHitByItem(npc, player, item, damage, knockback, crit);
            }
        }
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            bool strikeResult = true; //only if all prefixes agree that the hit was done, it should actually hit.
            for (int i = 0; i < NewPrefixes.Length; i++)
            {
                strikeResult &= NewPrefixes[i].StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
            }
            return strikeResult;
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
            }
            //only required by sub-mods
            //if (npc.FindBuffIndex(mod.BuffType("Suspended")) >= 0)
            //{
            //    damage = (int)(damage * 1.3);
            //}
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
            }
            //only required by sub-mods
            //if (npc.FindBuffIndex(mod.BuffType("Suspended")) >= 0)
            //{
            //    damage = (int)(damage * 1.3);
            //}
        }

        public override void AI(NPC npc)
        {
            base.AI(npc);
            if ((npc.type >= 547 && npc.type <= 578))
            {
                return;
            }

            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.AI(npc);
            }

            float damageMult = (Main.hardMode ? 0.5f : .4f) / (Main.expertMode ? Main.expertDamage : 1);//20% reduction PreHM, removed additional damage mult for expert
            Player target = Main.player[npc.target];

            if (npc.target == 255)
            {
                return;
            }
            // MPlayer targetInfo = ((MPlayer)target.GetModPlayer(mod, "MPlayer"));
            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            //if (prefixes.Contains("Void-Touched"))
            //{
            //    if (Main.rand.Next(0, 8) == 0)
            //    {
            //        int dust = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("VoidDust"));
            //        Main.dust[dust].velocity *= .2f;
            //    }
            //    if (Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
            //    {
            //        target.AddBuff(mod.BuffType("VoidTarget"), 300);
            //        target.AddBuff(BuffID.Darkness, 300);
            //        //voidTargetDamage = npc.damage;
            //    }
            //    #region Void King
            //    if (NPCID.KingSlime == npc.type)
            //    {
            //        npc.GivenName = "Void King";
            //        if (Main.rand.Next(30) == 1 && npc.life < npc.lifeMax)
            //        {
            //            npc.life++;
            //        }
            //        if (Main.rand.Next(5) == 1)
            //        {
            //            npc.AI();
            //        }
            //    }
            //    #endregion
            //}
            /*
            else if (npc.type == NPCID.BlueSlime || npc.type == NPCID.SlimeSpiked)
            {
                for(int i=0;  i < 100; i++)
                {
                    if(NPCID.KingSlime == Main.npc[i].type)
                    {
                        if (bossprefixes.Contains("Void-Touched"))
                        {
                            prefixes += "Void-Touched #";
                        }
                        break;
                    }
                }
            }*/
        }

        public override bool PreAI(NPC npc)
        {
            if (!MPSynced)
            {
                if (Main.netMode == 2)
                {
                    //send packet containing npc.whoAmI and all prefixes/suffixes.
                    var netMessage = mod.GetPacket();
                    netMessage.Write("Prefixes");
                    netMessage.Write(npc.whoAmI);
                    netMessage.Write(NewPrefixes.Length);
                    foreach (NPCPrefix prefix in NewPrefixes)
                    {
                        prefix.Sync(netMessage);
                    }
                    netMessage.Send();
                }
                MPSynced = true;
            }
            if (!nameConfirmed && (Main.netMode != 1 || readyForChecks))//block to ensure all enemies retain prefixes in their displayNames
            {
                if (NewPrefixes.Length > 0)
                {
                    OverrideName(npc);
                }
                nameConfirmed = true;
            }
            //EXPERIMENTAL STUN DEBUFF
            //needed for other mods, not this one
            //if (npc.FindBuffIndex(mod.BuffType("Stunned")) >= 0 || npc.FindBuffIndex(mod.BuffType("Suspended")) >= 0)
            //{
            //    return false;
            //}
            return base.PreAI(npc);
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {

            //set shaderID based on prefix here
            //if (prefixes.Contains("Void-Touched"))
            //{
            //    shaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShadowDye);
            //}

            int shaderID = NewPrefixes
                .Select(p => p.GetShaderID(npc))
                .FirstOrDefault(p => p > 0);

            if (shaderID > 0)
            {
                GraphicsUtils.BeginShaderBatch(spriteBatch);
                DrawData data = new DrawData
                {
                    origin = npc.Center,
                    position = npc.position - Main.screenPosition,
                    scale = new Vector2(npc.scale, npc.scale),
                    texture = Main.npcTexture[npc.type],
                    sourceRect = npc.frame//data.texture.Frame(1, Main.npcFrameCount[npc.type], 0, npc.frame);
                };
                GameShaders.Armor.ApplySecondary(shaderID, npc, data);

                GraphicsUtils.ResetBatch(spriteBatch);
            }
            return base.PreDraw(npc, spriteBatch, drawColor);
        }

        private NPC getClosestNPC(NPC projectile)
        {
            float lowestD = 99999;
            NPC closest = Main.npc[0];
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - projectile.Center.X) * (npc.Center.X - projectile.Center.X) + (npc.Center.Y - projectile.Center.Y) * (npc.Center.Y - projectile.Center.Y));
                if (lowestD > distance && !npc.townNPC && npc.life > 0)
                {
                    closest = npc;
                    lowestD = distance;
                }
            }
            return closest;
        }
        //as SpawnOnPlayer, but returns an int identifying the NPC spawned
    }
}
