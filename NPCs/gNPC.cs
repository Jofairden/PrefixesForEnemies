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
    public class gNPC : GlobalNPC
    {
        #region vars
        public int lives;
        public string prefixes = "";
        public string suffixes = "";

        public bool nameConfirmed = false;
        public bool MPSynced = false;
        public bool readyForChecks = false;

        public int gunbladeBurn = 0;
        public int lightSpearCount = 0;
        public int parasiteOwner = -1;
        public int parasiteTimeLeft = 0;
        public int gunbladeMeleeDebuff = 0;
        public int gunbladeMeleeDebuffTime = 0;
        public int gunbladeRangedDebuff = 0;
        public int gunbladeRangedDebuffTime = 0;
        public int voidBurn = 0;//magnitude
        public int numNeedlesHellfire = 0;
        public int numNeedlesTeravolt = 0;
        #endregion

        readonly int[] types = { 7, 10, 13, 39, 87, 95, 98, 117, 134, 402, 412, 454, 510, 513 };

        public const int AI_FRIENDLY = 7;
        //int[] zeroValTypes = {36, 68, 115, 116, 117, 128, 129, 130, 131, 395,  };
        //int[] doomsayerTypes = {2, 3, 6, 7, 31, 32, 34, 42, 62,  };//types that can recieve the doomsayer suffix, not implemented
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public NPCPrefix[] NewPrefixes { get; internal set; }

        public override void SetDefaults(NPC npc)
        {
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
            if (!(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
            {
                return;
            }

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

            npc.GivenName = npc.FullName;
            if ( 
            {
                npc.GivenName = "Rare " + npc.GivenName;
                prefixes += "Rare #";
                
            }
            /*
            if (Main.rand.Next(0, Main.hardMode ? 80 : 300) == 0 && (!npc.boss && npc.type != 36 && npc.type != 114 && !(npc.type >= 128 && npc.type <= 131) && !(npc.type >= 246 && npc.type <= 248)) && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
            {
                npc.GivenName = "Void-Touched " + npc.GivenName;
                prefixes += "Void-Touched #";
                npc.lifeMax = (int)(npc.lifeMax * 1.5);
                npc.value *= 3f;
            }
            */
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

            foreach (PrefixGroup group in PrefixVault.GetPrefixGroups()
                .Where(pg => pg.CanTrigger(npc))
                .Where(pg => pg.IsTriggered()))
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

            foreach (NPCPrefix prefix in NewPrefixes)
            {
                prefix.OnCreate(npc);
            }
            //todo dont forget to execute the oncreate somehwere!

            
        }

        private void OverrideName(NPC nPC)
        {
            string prefixName = "";
            string suffixName = "";
            foreach (string p in NewPrefixes.Where(pf => pf.IsPrefix()).Select(pf => pf.Name))
            {
                prefixName += p + " ";
            }
            foreach (string p in NewPrefixes.Where(pf => !pf.IsPrefix()).Select(pf => pf.Name))
            {
                suffixName += " " + p;
            }
            npc.GivenName = prefixName + npc.GivenName + suffixName;

            if (prefixes.Length > 1)
            {
                npc.GivenName = npc.GivenName.Replace("  the", "");
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            //base.AI(npc); why is this here?
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
        }


        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (prefixes.Contains("Void-Touched"))
            {
                MPlayer pinf = ((MPlayer)target.GetModPlayer(mod, "MPlayer"));
                pinf.voidBurn = Math.Max((int)(damage / 25f + target.statLifeMax2 / 200f + 1), pinf.voidBurn);
                target.AddBuff(mod.BuffType("VoidBurn"), 480 + 100 * damage);
            }
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
            if (lightSpearCount > 0)
            {
                npc.lifeRegen -= (int)(Math.Pow(1.05, lightSpearCount) * 30 * lightSpearCount);
                damage += (int)(Math.Pow(1.05, lightSpearCount) * 30 * lightSpearCount / 8);
                if (Main.rand.Next(0, 8) == 0)
                {
                    for (int i = 0; i < lightSpearCount; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 57, (npc.velocity.X + Main.rand.Next(-2, 3)) * .2f, (npc.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.White, 1f);
                    }
                }
                //this code may potentially cause issues, but for now it at least lets the DoT effects kill enemies
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
            if (gunbladeBurn > 0)
            {
                npc.lifeRegen -= gunbladeBurn / 4;
                damage += gunbladeBurn / 32;
                if (Main.rand.Next(0, 4) == 0)
                {
                    int d = Dust.NewDust(npc.position, npc.width, npc.height, 6, (npc.velocity.X + Main.rand.Next(-2, 3)) * .2f, (npc.velocity.Y + Main.rand.Next(-2, 3)) * .2f, 100, Color.White, 1f);
                }
                gunbladeBurn--;
                if (gunbladeBurn > 300)
                {
                    gunbladeBurn = (int)(gunbladeBurn * .995);
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
            if (voidBurn > 0)
            {
                npc.lifeRegen -= voidBurn / 2 / (prefixes.Contains("Void-Touched") ? 3 : 1);
                damage += voidBurn / 16 / (prefixes.Contains("Void-Touched") ? 3 : 1);
                if (Main.rand.Next(5) == 0) { int d = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("VoidDust"), 0, 0, 0, default(Color), 1f); }
            }
        }
        public override bool PreNPCLoot(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && prefixes.Contains("Void-Touched #"))
            {
                npc.type = -1;
            }
            return base.PreNPCLoot(npc);
        }
        public override void NPCLoot(NPC npc)
        {

            if (parasiteTimeLeft > 0)
            {
                NPC target = getClosestNPC(npc);
                if (npc.position == target.position)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, mod.ProjectileType("NebulaParasite"), 1, 0, parasiteOwner);
                }
                else if (npc.Distance(target.position) < 800)
                {
                    Vector2 vel = target.position - npc.position;
                    vel.Normalize();
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vel.X * 6, vel.Y * 6, mod.ProjectileType("NebulaParasite"), 1, 0, parasiteOwner);
                }
            }
            if (suffixes.Length > 1)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
            }
            if (prefixes.Contains("Void-Touched"))
            {
                if (npc.boss)
                {
                    if (npc.GivenName == "Void King")
                    {
                        //drop weapon
                        //drop lesser void heart
                    }
                }
                else
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VoidEssence"));
                }
            }
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
            //if (gunbladeMeleeDebuff > 0 && item.melee)
            //{
            //    damage = (int)(damage * 1 + (.1 * gunbladeMeleeDebuff));
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
            //if (gunbladeRangedDebuff > 0 && projectile.ranged)
            //{
            //    damage = (int)(damage * 1 + (.1 * gunbladeRangedDebuff));
            //}
        }

        public override void AI(NPC npc)
        {
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

            if (prefixes.Contains("Void-Touched"))
            {
                if (Main.rand.Next(0, 8) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("VoidDust"));
                    Main.dust[dust].velocity *= .2f;
                }
                if (Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    target.AddBuff(mod.BuffType("VoidTarget"), 300);
                    target.AddBuff(BuffID.Darkness, 300);
                    //voidTargetDamage = npc.damage;
                }
                #region Void King
                if (NPCID.KingSlime == npc.type)
                {
                    npc.GivenName = "Void King";
                    if (Main.rand.Next(30) == 1 && npc.life < npc.lifeMax)
                    {
                        npc.life++;
                    }
                    if (Main.rand.Next(5) == 1)
                    {
                        npc.AI();
                    }
                }
                #endregion
            }
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
                    foreach(NPCPrefix prefix in NewPrefixes)
                    {
                        prefix.Sync(netMessage);
                    }
                    netMessage.Send();
                }
                MPSynced = true;
                return true;
            }
            if (!nameConfirmed && (Main.netMode != 1 || readyForChecks))//block to ensure all enemies retain prefixes in their displayNames
            {
                if (prefixes.Length > 1)//has prefixes
                {
                    npc.GivenName = npc.FullName;
                    string[] prefixesArr = prefixes.Split('#');
                    for (int i = 0; i < prefixesArr.Length; i++)
                    {
                        if (!npc.GivenName.Contains(prefixesArr[i]))
                        {
                            npc.GivenName = prefixes[i] + npc.GivenName;
                        }
                    }
                    npc.GivenName = npc.GivenName.Replace("  the", "");
                }
                if (suffixes.Length > 1)//has suffixes
                {
                    if (prefixes.Length <= 1)
                    {
                        npc.GivenName = npc.FullName;
                    }
                    if (!npc.GivenName.Contains(suffixes))
                    {
                        npc.GivenName += suffixes;
                    }
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

        public override void PostAI(NPC npc)
        {
            //only required for gunblade mod types
            //if (gunbladeMeleeDebuffTime > 0)
            //{
            //    gunbladeMeleeDebuffTime--;
            //    if (gunbladeMeleeDebuffTime == 0)
            //    {
            //        gunbladeMeleeDebuff = 0;
            //    }
            //}
            //if (gunbladeRangedDebuffTime > 0)
            //{
            //    gunbladeRangedDebuffTime--;
            //    if (gunbladeRangedDebuffTime == 0)
            //    {
            //        gunbladeRangedDebuff = 0;
            //    }
            //}
            base.PostAI(npc);
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {

            int shaderID = 0;

            //set shaderID based on prefix here
            if (prefixes.Contains("Rare"))
            {
                shaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveGoldDye);
            }
            if (prefixes.Contains("Void-Touched"))
            {
                shaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShadowDye);
            }

            //this bit is what seems to be causing things to turn green - this and the PostDraw code. Unfortuneately, the shader doesn't apply without these lines.
            //spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(0f, 0f, 0f)));

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(0f, 0f, 0f)));

            shaderID = NewPrefixes
                .Select(p => p.GetShaderID(npc))
                .FirstOrDefault(p => p > 0);

            DrawData data = new DrawData
            {
                origin = npc.Center,
                position = npc.position - Main.screenPosition,
                scale = new Vector2(npc.scale, npc.scale),
                texture = Main.npcTexture[npc.type],
                sourceRect = npc.frame//data.texture.Frame(1, Main.npcFrameCount[npc.type], 0, npc.frame);
            };
            GameShaders.Armor.ApplySecondary(shaderID, npc, data);

            return true;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(0f, 0f, 0f)));
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
