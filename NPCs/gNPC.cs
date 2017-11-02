using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;

namespace EnemyMods.NPCs
{
    public class gNPC : GlobalNPC
    {
        #region vars
        public int specialTimer = 0;
        public int timerAI = 0;
        public int timerSuffix = 0;
        public int countSuffix = 0;
        public int lives;
        public int debuffTimer = 0;
        public int debuffTimer2 = 0;
        public string prefixes = "";
        public string suffixes = "";
        public bool rangedResist, meleeResist, magicResist, minionResist;
        public int rangedResistTimer, meleeResistTimer, magicResistTimer, minionResistTimer = 0;
        public int postMoonTimer = 0;
        public int postMoonCount = 0;
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
        int roll;
        int[] types = { 7, 10, 13, 39, 87, 95, 98, 117, 134, 402, 412, 454, 510, 513 };//worm-type heads to be specially included
        //int[] zeroValTypes = {36, 68, 115, 116, 117, 128, 129, 130, 131, 395,  };
        //int[] doomsayerTypes = {2, 3, 6, 7, 31, 32, 34, 42, 62,  };//types that can recieve the doomsayer suffix, not implemented
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public override void SetDefaults(NPC npc)
        {
            if (Main.rand == null || Main.netMode == 1 || npc == null || npc.FullName == null)//if multiplayer, but not server. 1 is client in MP, 2 is server. Prefixes are sent to client by server in MP.
            {
                return;
            }
            //hopefully temporary: prefix AI causes these enemies to immediately despawn under certain conditions
            if (npc.FullName == "DesertScourgeBody" || npc.FullName == "DesertScourgeTail" || npc.FullName == "DevourerofGodsBody" || npc.FullName == "DevourerofGodsTail")
            {
                return;
            }
            if (npc.type >= 547 && npc.type <= 578)
            {
                return;
            }
            if (npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37) ^ Array.Exists(types, element => element == npc.type)) && npc.type != 401 && npc.type != 488 && npc.type != 371 && npc.lifeMax > 1 && !(npc.aiStyle == 0 && npc.value == 0 && npc.npcSlots == 1))
            {
                npc.GivenName = npc.FullName;
                if (Main.rand.Next(0, Main.expertMode ? 80 : 100) == 0 && (npc.value != 0 || (npc.type >= 402 && npc.type <= 429)) && npc.type != 239 && npc.type != 240 && npc.type != 469 && npc.type != 238 && npc.type != 237 && npc.type != 236 && npc.type != 164 && npc.type != 165 && npc.type != 163)
                {
                    npc.GivenName = "Rare " + npc.GivenName;
                    prefixes += "Rare #";
                    npc.lifeMax *= 2;
                    npc.value *= 1.5f;
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
                #region stat mods
                if (Main.rand.Next(0, 4) == 0)//25% to start stat mods block
                {
                    npc.value *= 1.5f;
                    roll = Main.rand.Next(0, 81);
                    if (roll <= 10)//tough
                    {
                        npc.GivenName = "Tough " + npc.GivenName;
                        prefixes += "Tough #";
                        npc.lifeMax = (int)(1.5 * npc.lifeMax);
                    }
                    if (roll > 10 && roll <= 20)//dangerous
                    {
                        npc.GivenName = "Dangerous " + npc.GivenName;
                        prefixes += "Dangerous #";
                        npc.damage = (int)(1.4 * npc.damage);
                    }
                    if (roll > 20 && roll <= 30)//armored
                    {
                        npc.GivenName = "Armored " + npc.GivenName;
                        prefixes += "Armored #";
                        npc.defense = (int)(1.2 * npc.defense + 4);
                    }
                    if(npc.type == NPCID.Golem || NPCID.GolemHead == npc.type)
                    {
                        goto Debuff;
                    }
                    if (roll > 30 && roll <= 40)//small
                    {
                        npc.GivenName = "Small " + npc.GivenName;
                        prefixes += "Small #";
                        npc.scale *= 0.7f;
                    }
                    if (roll > 40 && roll <= 50)//big
                    {
                        npc.GivenName = "Big " + npc.GivenName;
                        prefixes += "Big #";
                        npc.scale *= 1.4f;
                    }
                    if (roll > 50 && roll <= 60)//persistent
                    {
                        npc.GivenName = "Persistent " + npc.GivenName;
                        prefixes += "Persistent #";
                        npc.knockBackResist = npc.knockBackResist / 1.5f;
                    }
                    if (roll > 60 && roll <= 70)
                    {
                        npc.GivenName = "Enduring " + npc.GivenName;
                        prefixes += "Enduring #";
                        npc.takenDamageMultiplier *= .8f;
                    }
                    if (roll > 70 && roll <= 75)
                    {
                        npc.GivenName = "Colossal " + npc.GivenName;
                        prefixes += "Colossal #";
                        npc.scale *= 1.8f;
                        npc.damage = (int)(npc.damage * 1.2);
                    }
                    if (roll > 75 && roll <= 80)
                    {
                        npc.GivenName = "Miniature " + npc.GivenName;
                        prefixes += "Miniature #";
                        npc.scale *= .45f;
                        npc.damage = (int)(npc.damage * .8);
                    }
                }
                #endregion
                Debuff: 
                #region debuff weaknesses
                if (Main.rand.Next(0, 5) == 0)//debuff weaknesses block
                {
                    npc.value *= 1.5f;
                    roll = Main.rand.Next(0, 4);
                    if (roll == 0)
                    {
                        npc.GivenName = "Flammable " + npc.GivenName;
                        prefixes += "Flammable #";
                    }
                    if (roll == 1)
                    {
                        npc.GivenName = "Cryophobic " + npc.GivenName;
                        prefixes += "Cryophobic #";
                    }
                    if (roll == 2)
                    {
                        npc.GivenName = "Toxiphobic " + npc.GivenName;
                        prefixes += "Toxiphobic #";
                    }
                    if (roll == 3)
                    {
                        npc.GivenName = "Pyophobic " + npc.GivenName;
                        prefixes += "Pyophobic #";
                    }

                }
                #endregion
                #region special effects
                if (Main.rand.Next(0, 6) == 0)//special effects block
                {
                    npc.value *= 1.5f;
                    roll = Main.rand.Next(0, 182);
                    if (roll <= 10)//burning
                    {
                        npc.GivenName = "Burning " + npc.GivenName;
                        prefixes += "Burning #";
                    }
                    if (roll <= 15 && roll > 10)//hellfire
                    {
                        npc.GivenName = "Hellfire " + npc.GivenName;
                        prefixes += "Hellfire #";
                    }
                    if (roll <= 22 && roll > 15)//frozen
                    {
                        npc.GivenName = "Frozen " + npc.GivenName;
                        prefixes += "Frozen #";
                        npc.coldDamage = true;
                    }
                    if (roll <= 28 && roll > 22 && Main.hardMode)//electrified
                    {
                        npc.GivenName = "Electrified " + npc.GivenName;
                        prefixes += "Electrified #";
                    }
                    if (roll <= 33 && roll > 28)//breaker
                    {
                        npc.GivenName = "Breaker " + npc.GivenName;
                        prefixes += "Breaker #";
                    }
                    if (roll <= 38 && roll > 33)//Dark
                    {
                        npc.GivenName = "Dark " + npc.GivenName;
                        prefixes += "Dark #";
                    }
                    if (roll <= 44 && roll > 38)//Confusing
                    {
                        npc.GivenName = "Trickster " + npc.GivenName;
                        prefixes += "Trickster #";
                    }
                    if (roll <= 50 && roll > 44)
                    {
                        npc.GivenName = "Hexing " + npc.GivenName;
                        prefixes += "Hexing #";
                    }
                    if (roll <= 55 && roll > 50)
                    {
                        npc.GivenName = "Slowing " + npc.GivenName;
                        prefixes += "Slowing #";
                    }
                    if (roll <= 60 && roll > 55)
                    {
                        npc.GivenName = "Venomous " + npc.GivenName;
                        prefixes += "Venomous #";
                    }
                    if (roll <= 65 && roll > 60)
                    {
                        npc.GivenName = "Petrifying " + npc.GivenName;
                        prefixes += "Petrifying #";
                    }
                    if (roll <= 75 && roll > 65)
                    {
                        npc.GivenName = "Regenerating " + npc.GivenName;
                        prefixes += "Regenerating #";
                    }
                    if (roll <= 82 && roll > 75)
                    {
                        npc.GivenName = "Martyr " + npc.GivenName;
                        prefixes += "Martyr #";
                    }
                    if (roll <= 90 && roll > 82)
                    {
                        npc.GivenName = "Vampiric " + npc.GivenName;
                        prefixes += "Vampiric #";
                    }
                    if (roll <= 97 && roll > 90)
                    {
                        npc.GivenName = "Magebane " + npc.GivenName;
                        prefixes += "Magebane #";
                    }
                    if (roll <= 107 && roll > 97)
                    {
                        npc.GivenName = "Voodoo " + npc.GivenName;
                        prefixes += "Voodoo #";
                    }
                    if (roll <= 115 && roll > 107)
                    {
                        npc.GivenName = "Vengeful " + npc.GivenName;
                        prefixes += "Vengeful #";
                    }
                    if (roll <= 122 && roll > 115)
                    {
                        npc.GivenName = "Mutilator " + npc.GivenName;
                        prefixes += "Mutilator #";
                    }
                    if (roll <= 129 && roll > 122)
                    {
                        npc.GivenName = "Executioner " + npc.GivenName;
                        prefixes += "Executioner #";
                    }
                    if (roll <= 139 && roll > 129 && npc.type != 266)
                    {
                        npc.GivenName = "Stealthy " + npc.GivenName;
                        prefixes += "Stealthy #";
                    }
                    if (roll <= 146 && roll > 139 && npc.type != 13 && npc.type != 439 && npc.aiStyle != 94)
                    {
                        npc.GivenName = "Splitter " + npc.GivenName;
                        prefixes += "Splitter #";
                    }
                    if (roll <= 153 && roll > 146)
                    {
                        npc.GivenName = "Forceful " + npc.GivenName;
                        prefixes += "Forceful #";
                    }
                    if (roll <= 160 && roll > 153)
                    {
                        npc.GivenName = "Launching " + npc.GivenName;
                        prefixes += "Launching #";
                    }
                    if (roll <= 167 && roll > 160)
                    {
                        npc.GivenName = "Halting " + npc.GivenName;
                        prefixes += "Halting #";
                    }
                    if (roll <= 174 && roll > 167)
                    {
                        npc.GivenName = "Wing Clipper " + npc.GivenName;
                        prefixes += "Wing Clipper #";
                    }
                    if (roll <= 181 && roll > 174)
                    {
                        npc.GivenName = "Cutpurse " + npc.GivenName;
                        prefixes += "Cutpurse #";
                    }
                }
                #endregion
                #region projectiles
                if (Main.rand.Next(0, 12 + (Main.hardMode ? 0 : 8)) == 0 && npc.type != NPCID.Creeper && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3))//extra projectiles block
                {
                    npc.value *= 1.5f;
                    roll = Main.rand.Next(0, 131);
                    if (roll <= 10)
                    {
                        npc.GivenName = "Volcanic " + npc.GivenName;
                        prefixes += "Volcanic #";
                    }
                    if (roll > 10 && roll <= 20)
                    {
                        npc.GivenName = "Shadowmage " + npc.GivenName;
                        prefixes += "Shadowmage #";
                    }
                    if (roll > 20 && roll <= 30 && Main.hardMode)
                    {
                        npc.GivenName = "Lightning Rod " + npc.GivenName;
                        prefixes += "Lightning Rod #";
                    }
                    if (roll > 30 && roll <= 40)
                    {
                        npc.GivenName = "Rune Mage " + npc.GivenName;
                        prefixes += "Rune Mage #";
                    }
                    if (roll > 40 && roll <= 50)
                    {
                        npc.GivenName = "Party Animal " + npc.GivenName;
                        prefixes += "Party Animal #";
                    }
                    if (roll > 50 && roll <= 60)
                    {
                        npc.GivenName = "Flametrail " + npc.GivenName;
                        prefixes += "Flametrail #";
                    }
                    if (roll > 60 && roll <= 70)
                    {
                        npc.GivenName = "Bionic " + npc.GivenName;
                        prefixes += "Bionic #";
                    }
                    if (roll > 70 && roll <= 80 && Main.hardMode)
                    {
                        npc.GivenName = "Shadowflame " + npc.GivenName;
                        prefixes += "Shadowflame #";
                    }
                    if (roll > 80 && roll <= 90 && Main.hardMode)
                    {
                        npc.GivenName = "Dune Mage " + npc.GivenName;
                        prefixes += "Dune Mage #";
                    }
                    if (roll > 90 && roll <= 100)
                    {
                        npc.GivenName = "Bubbly " + npc.GivenName;
                        prefixes += "Bubbly #";
                    }
                    if (roll > 100 && roll <= 110)
                    {
                        npc.GivenName = "Hellwing " + npc.GivenName;
                        prefixes += "Hellwing #";
                    }
                    if (roll > 110 && roll <= 120)
                    {
                        npc.GivenName = "Sonorous " + npc.GivenName;
                        prefixes += "Sonorous #";
                    }
                    if (roll > 120 && roll <= 125 && npc.value > 0)
                    {
                        npc.GivenName = "Generous " + npc.GivenName;
                        prefixes += "Generous #";
                        npc.value *= 2;
                    }
                    if (roll > 125 && roll <= 130 && npc.value > 0)
                    {
                        npc.GivenName = "Philanthropic " + npc.GivenName;
                        prefixes += "Philanthropic #";
                        npc.value *= 3;
                    }
                }
                #endregion
                #region post-Moonlord
                if (Main.rand.Next(0, 10) == 0 && NPC.downedMoonlord)//post-moonlord block
                {
                    npc.value *= 1.5f;
                    roll = Main.rand.Next(0, 61);
                    if (roll <= 10)
                    {
                        npc.GivenName = "Mirrored " + npc.GivenName;
                        prefixes += "Mirrored #";
                    }
                    if (roll > 10 && roll <= 20)
                    {
                        npc.GivenName = "Malefic " + npc.GivenName;
                        prefixes += "Malefic #";
                    }
                    if (roll > 20 && roll <= 30)
                    {
                        npc.GivenName = "Adaptive " + npc.GivenName;
                        prefixes += "Adaptive #";
                    }
                    if (roll > 30 && roll <= 40)
                    {
                        npc.GivenName = "Channeler " + npc.GivenName;
                        prefixes += "Channeler #";
                    }
                    if (roll > 40 && roll <= 50)
                    {
                        npc.GivenName = "Leeching " + npc.GivenName;
                        prefixes += "Leeching #";
                    }
                    if (roll > 50 && roll <= 60)
                    {
                        npc.GivenName = "Psychic " + npc.GivenName;
                        prefixes += "Psychic #";
                    }
                }
                #endregion
                #region suffixes
                if (Main.rand.Next(0, 100) == 0 && NPC.downedBoss3 && (!npc.boss && npc.type != 36 && npc.type != 114 && !(npc.type >= 128 && npc.type <= 131) && !(npc.type >= 246 && npc.type <= 248)))//suffixes block
                {
                    npc.value *= 5;
                    roll = Main.rand.Next(0, 7);
                    if (roll == 0)
                    {
                        npc.GivenName = npc.GivenName + " the Juggernaut";
                        suffixes += " the Juggernaut";
                        npc.defense *= 2;
                        npc.lifeMax *= 3;
                        npc.knockBackResist = 0;
                        npc.scale *= 1.3f;
                    }
                    if (roll == 1)
                    {
                        npc.GivenName = npc.GivenName + " the Reaper";
                        suffixes += " the Reaper";
                        npc.defense /= 2;
                        npc.lifeMax = (int)(npc.lifeMax * .666);
                    }
                    if (roll == 2)
                    {
                        npc.GivenName = npc.GivenName + " the Immortal";
                        suffixes += " the Immortal";
                        lives = 8;
                    }
                    if (roll == 3 && Main.hardMode)
                    {
                        npc.GivenName = npc.GivenName + " the Nullifier";
                        suffixes += " the Nullifier";
                        for (int i = 0; i < npc.buffImmune.Length; i++)
                        {
                            npc.buffImmune[i] = true;
                        }
                    }
                    if (roll == 4)
                    {
                        npc.GivenName = npc.GivenName + " the Necromancer";
                        suffixes += " the Necromancer";
                    }
                    if (roll == 5 && !npc.dontTakeDamage)
                    {
                        npc.GivenName = npc.GivenName + " the Master Ninja";
                        suffixes += " the Master Ninja";
                        npc.damage = (int)(npc.damage * 1.5);
                    }
                    if (roll == 6)
                    {
                        npc.GivenName = npc.GivenName + " the Lightning God";
                        suffixes += " the Lightning God";
                    }
                }
                #endregion
                if(prefixes.Length > 1)
                {
                    npc.GivenName = npc.GivenName.Replace("  the", "");
                }
            }
        }
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            //base.AI(npc); why is this here?
            if (prefixes.Contains("Rare "))
            {
                Lighting.AddLight(npc.position, 0.415f, 0.343f, 0.108f);
                if (Main.rand.Next(20) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, DustID.GoldCoin, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2f);
                }
            }
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
            if (prefixes.Contains("Burning "))
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            if (prefixes.Contains("Hellfire "))
            {
                target.AddBuff(BuffID.CursedInferno, 300);
            }
            if (prefixes.Contains("Frozen "))
            {
                target.AddBuff(BuffID.Frostburn, 300);
                if (Main.rand.Next(1, 12) == 1)
                {
                    target.AddBuff(BuffID.Frozen, 120);
                }
            }
            if (prefixes.Contains("Electrified "))
            {
                target.AddBuff(BuffID.Electrified, 300);
            }
            if (prefixes.Contains("Breaker "))
            {
                target.AddBuff(BuffID.BrokenArmor, 600);
            }
            if (prefixes.Contains("Dark "))
            {
                target.AddBuff(BuffID.Darkness, 300);
            }
            if (prefixes.Contains("Trickster "))
            {
                target.AddBuff(BuffID.Confused, 120);
            }
            if (prefixes.Contains("Hexing "))
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    target.AddBuff(BuffID.Cursed, 180);
                }
            }
            if (prefixes.Contains("Slowing "))
            {
                target.AddBuff(BuffID.Slow, 300);
            }
            if (prefixes.Contains("Venomous "))
            {
                target.AddBuff(BuffID.Venom, 300);
            }
            if (prefixes.Contains("Petrifying "))
            {
                if (Main.rand.Next(0, 3) == 0 && target.FindBuffIndex(BuffID.Stoned) == -1)
                {
                    target.AddBuff(BuffID.Stoned, 180);
                }
            }
            if (prefixes.Contains("Forceful "))
            {
                if (npc.Center.X <= target.Center.X)
                {
                    target.velocity.X += 15;
                }
                else target.velocity.X -= 15;
            }
            if (prefixes.Contains("Launching "))
            {
                target.velocity.Y -= 25;
            }
            if (prefixes.Contains("Halting "))
            {
                target.velocity = Vector2.Zero;
            }
            if (prefixes.Contains("Wing Clipper "))
            {
                target.wingTime = 0;
                target.rocketTime = 0;
                target.jumpAgainBlizzard = false;
                target.jumpAgainCloud = false;
                target.jumpAgainFart = false;
                target.jumpAgainSail = false;
                target.jumpAgainSandstorm = false;
                target.jumpAgainUnicorn = false;
            }
            if (prefixes.Contains("Vampiric "))
            {
                npc.life += damage;
                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 50, npc.width, npc.height), new Color(20, 120, 20, 200), "" + damage);
                if (npc.life > npc.lifeMax)
                {
                    npc.life = npc.lifeMax;
                }
            }
            if (suffixes.Contains(" the Nullifier"))
            {
                for (int i = 0; i < target.buffType.Length; i++)
                {
                    target.DelBuff(i);
                }
            }
            if (prefixes.Contains("Cutpurse "))
            {
                for (int i = 0; i < 59; i++)
                {
                    if (target.inventory[i].type >= 71 && target.inventory[i].type <= 74)
                    {
                        int num2 = Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, target.inventory[i].type, 1, false, 0, false, false);
                        int num3 = (int)(target.inventory[i].stack * .9);
                        num3 = target.inventory[i].stack - num3;
                        target.inventory[i].stack -= num3;
                        if (target.inventory[i].stack <= 0)
                        {
                            target.inventory[i] = new Item();
                        }
                        Main.item[num2].stack = num3;
                        Main.item[num2].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[num2].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[num2].noGrabDelay = 100;
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
                        }
                        if (i == 58)
                        {
                            Main.mouseItem = target.inventory[i].Clone();
                        }
                    }
                }
            }
        }
        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            if (prefixes.Contains("Magebane "))
            {
                damage += target.statMana / 4;
                target.statMana /= 2;
                CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 50, target.width, target.height), new Color(20, 20, 120, 200), "" + target.statMana);
            }
            if (suffixes.Contains(" the Reaper"))
            {
                if (damage < target.statLife)
                {
                    damage = target.statLife - 1 + target.statDefense / 2;
                }
            }
            if (prefixes.Contains("Mutilator "))
            {
                if (target.statLife == target.statLifeMax2)
                {
                    damage *= 2;
                }
            }
            if (prefixes.Contains("Executioner "))
            {
                if (target.statLife <= target.statLifeMax2 / 5)
                {
                    damage *= 2;
                }
            }
            if (prefixes.Contains("Vengeful "))
            {
                damage += (int)((npc.life / (npc.life + npc.lifeMax)) * damage);
            }
        }
        public override bool CheckDead(NPC npc)
        {
            if (lives > 0 && suffixes.Contains(" the Immortal"))
            {
                lives--;
                npc.damage = (int)(npc.damage * 1.1);
                npc.life = npc.lifeMax;
                Main.PlaySound(15, npc.position, 0);
                return false;
            }
            return true;
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
            if(voidBurn > 0)
            {
                npc.lifeRegen -= voidBurn / 2 / (prefixes.Contains("Void-Touched") ? 3 : 1);
                damage += voidBurn / 16 / (prefixes.Contains("Void-Touched") ? 3 : 1);
                if (Main.rand.Next(5) == 0) { int d = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("VoidDust"), 0, 0, 0, default(Color), 1f); }
            }
            if (prefixes.Contains("Regenerating ") && npc.FindBuffIndex(BuffID.OnFire) == -1 && npc.FindBuffIndex(BuffID.Poisoned) == -1 && npc.FindBuffIndex(BuffID.Venom) == -1 && npc.FindBuffIndex(BuffID.CursedInferno) == -1)
            {
                npc.lifeRegen += (int)Math.Sqrt(npc.lifeMax - npc.life) / 2 + 1;
            }
        }
        public override bool PreNPCLoot(NPC npc)
        {
            if(npc.type == NPCID.KingSlime && prefixes.Contains("Void-Touched #"))
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
            if (prefixes.Contains("Rare "))
            {
                if (NPC.downedAncientCultist)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AmberToken"));
                }
                else if (NPC.downedPlantBoss)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RubyToken"));
                }
                else if (NPC.downedMechBossAny)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EmeraldToken"));
                }
                else if (Main.hardMode)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SapphireToken"));
                }
                else if (NPC.downedBoss3 || NPC.downedQueenBee)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TopazToken"));
                }
                else
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AmethystToken"));
                }
            }
            if (prefixes.Contains("Martyr "))
            {
                int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("MartyrBomb"), (int)(npc.damage * 1.5), 10);
            }
            if (prefixes.Contains("Splitter "))
            {
                int x = 2 + Main.rand.Next(0, 3);
                for (int i = 0; i < x; i++)
                {
                    int n = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, npc.type);
                    Main.npc[n].velocity.X = Main.rand.Next(-3, 4);
                    Main.npc[n].velocity.Y = Main.rand.Next(-3, 4);
                    Main.npc[n].life /= 2;
                    Main.npc[n].scale *= .85f;
                    Main.npc[n].lifeMax /= 2;
                    Main.npc[n].damage = (int)(Main.npc[n].damage * .8);
                }
            }
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (prefixes.Contains("Mirrored "))
            {
                npc.reflectingProjectiles = true;
                postMoonTimer = 20;
            }
            if (prefixes.Contains("Stealthy "))
            {
                npc.alpha -= 100;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }
            if (suffixes.Contains(" the Master Ninja"))
            {
                if (countSuffix == -1)
                {
                    npc.alpha = 250;
                    npc.dontTakeDamage = true;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, 75, npc.damage, 0);
                    Main.projectile[p].Kill();
                }
                else
                {
                    npc.alpha = 0;
                    countSuffix++;
                }
            }
        }
        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (prefixes.Contains("Stealthy "))
            {
                npc.alpha -= 100;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }
            if (suffixes.Contains(" the Master Ninja"))
            {
                if (countSuffix == -1)
                {
                    npc.alpha = 250;
                    npc.dontTakeDamage = true;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, 75, (int)(npc.damage * .7), 0);
                    Main.projectile[p].Kill();
                }
                else
                {
                    npc.alpha = 0;
                    countSuffix++;
                }
            }
        }
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (prefixes.Contains("Pyophobic "))
            {
                int buffindex = npc.FindBuffIndex(69);
                if (buffindex != -1)
                    if (npc.buffTime[buffindex] > 0)
                    {
                        damage *= 1.3;
                    }
            }
            return true;
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (prefixes.Contains("Adaptive "))
            {
                if (item.melee)
                {
                    if (meleeResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    meleeResist = true;
                    meleeResistTimer = 180;
                }
                if (item.ranged)
                {
                    if (rangedResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    rangedResist = true;
                    rangedResistTimer = 180;
                }
                if (item.magic)
                {
                    if (magicResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    magicResist = true;
                    magicResistTimer = 180;
                }
                if (item.summon)
                {
                    if (minionResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    minionResist = true;
                    minionResistTimer = 180;
                }
            }
            if (npc.FindBuffIndex(mod.BuffType("Suspended")) >= 0)
            {
                damage = (int)(damage * 1.3);
            }
            if (gunbladeMeleeDebuff > 0 && item.melee)
            {
                damage = (int)(damage * 1 + (.1 * gunbladeMeleeDebuff));
            }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (prefixes.Contains("Adaptive "))
            {
                if (projectile.melee)
                {
                    if (meleeResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    meleeResist = true;
                    meleeResistTimer = 180;
                }
                if (projectile.ranged)
                {
                    if (rangedResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    rangedResist = true;
                    rangedResistTimer = 180;
                }
                if (projectile.magic)
                {
                    if (magicResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    magicResist = true;
                    magicResistTimer = 180;
                }
                if (projectile.minion)
                {
                    if (minionResist)
                    {
                        damage = (int)(damage * .3);
                    }
                    minionResist = true;
                    minionResistTimer = 180;
                }
            }
            if (npc.FindBuffIndex(mod.BuffType("Suspended")) >= 0)
            {
                damage = (int)(damage * 1.3);
            }
            if (gunbladeRangedDebuff > 0 && projectile.ranged)
            {
                damage = (int)(damage * 1 + (.1 * gunbladeRangedDebuff));
            }
        }

        public override void AI(NPC npc)
        {

            if ((npc.type >= 547 && npc.type <= 578))
            {
                return;
            }

            float damageMult = (Main.hardMode ? 1f : .8f) / (Main.expertMode ? Main.expertDamage : 1);//20% reduction PreHM, removed additional damage mult for expert
            damageMult /= 2;//removes 2x mult for hostile projectiles
            Player target = Main.player[npc.target];
            MPlayer targetInfo;
            if (npc.target == 255)
            {
                targetInfo = null;
            }
            else
            {
                targetInfo = ((MPlayer)target.GetModPlayer(mod, "MPlayer"));
            }
            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (prefixes.Contains("Flametrail ") && Main.netMode != 2)
            {
                timerAI += 1;
                if (timerAI > 15)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, 400 + Main.rand.Next(0, 3), (int)(npc.damage * damageMult), 0);
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].hostile = true;
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Adaptive "))
            {
                minionResistTimer--;
                if (minionResistTimer < 0)
                    minionResist = false;
                magicResistTimer--;
                if (magicResistTimer < 0)
                    magicResist = false;
                meleeResistTimer--;
                if (meleeResistTimer < 0)
                    meleeResist = false;
                rangedResistTimer--;
                if (rangedResistTimer < 0)
                    rangedResist = false;
            }
            if (prefixes.Contains("Leeching "))
            {
                postMoonTimer += 1;
                if (postMoonTimer >= 250)
                {
                    postMoonTimer = 0;
                    ArrayList toBuff = getNPCsInRange(npc, 300);
                    for (int i = 0; i < toBuff.Count; i++)
                    {
                        NPC n = (NPC)toBuff[i];
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
            if (prefixes.Contains("Mirrored "))
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.SilverCoin);
                }
                if (npc.reflectingProjectiles)
                {
                    if (Main.rand.Next(0, 4) == 0)
                    {
                        int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.SilverCoin);
                    }
                    postMoonTimer--;
                    if (postMoonTimer <= 0)
                    {
                        npc.reflectingProjectiles = false;
                    }
                }
            }
            if (suffixes.Contains(" the Necromancer"))
            {
                timerSuffix += 1;
                if (timerSuffix > 480)
                {
                    if (NPC.downedPlantBoss)
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, 269 + Main.rand.Next(0, 17));//269-286
                    }
                    else if (Main.hardMode)
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.ArmoredSkeleton + Main.rand.Next(0, 1) * 33);//77 or 110
                    }
                    else
                    {
                        NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Skeleton);
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);
                    }
                    Main.PlaySound(2, npc.position, 8);
                    timerSuffix = 0;
                }
            }
            if (prefixes.Contains("Flammable "))
            {
                npc.buffImmune[BuffID.OnFire] = false;
                npc.buffImmune[BuffID.CursedInferno] = false;
                int buffindex = npc.FindBuffIndex(24);
                if (buffindex != -1)
                    if (npc.buffTime[buffindex] > 0)
                    {
                        debuffTimer++;
                        if (debuffTimer >= 15)
                        {
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
                            debuffTimer = 0;
                        }
                    }
                buffindex = npc.FindBuffIndex(39);
                if (buffindex != -1)
                    if (npc.buffTime[buffindex] > 0)
                    {
                        debuffTimer++;
                        if (debuffTimer >= 15)
                        {
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
                            debuffTimer = 0;
                        }
                    }
            }
            if (prefixes.Contains("Toxiphobic "))
            {
                npc.buffImmune[BuffID.Poisoned] = false;
                npc.buffImmune[BuffID.Venom] = false;
                int buffindex = npc.FindBuffIndex(20);
                if (buffindex != -1)
                    if (npc.buffTime[buffindex] > 0)
                    {
                        debuffTimer++;
                        if (debuffTimer >= 15)
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
                            debuffTimer = 0;
                        }
                    }
                buffindex = npc.FindBuffIndex(70);
                if (buffindex != -1)
                    if (npc.buffTime[buffindex] > 0)
                    {
                        debuffTimer2++;
                        if (debuffTimer2 >= 15)
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
                            debuffTimer2 = 0;
                        }
                    }
            }
            if (prefixes.Contains("Cryophobic "))
            {
                npc.buffImmune[BuffID.Frostburn] = false;
                int buffindex = npc.FindBuffIndex(44);
                if (buffindex != -1)
                    if (npc.buffTime[buffindex] > 0)
                    {
                        debuffTimer++;
                        if (debuffTimer >= 30)
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
                            debuffTimer = 0;
                        }
                    }
            }
            if (prefixes.Contains("Pyophobic "))
            {
                npc.buffImmune[BuffID.Ichor] = false;
            }
            if (prefixes.Contains("Stealthy "))
            {
                npc.alpha++;
                if (npc.alpha > 240)
                {
                    npc.alpha = 240;
                }
            }
            if (suffixes.Contains(" the Master Ninja"))
            {
                if (countSuffix > 0)
                {
                    timerSuffix += countSuffix;
                    if (timerSuffix >= 360)
                    {
                        countSuffix = -1;
                        timerSuffix = 120;
                    }
                }
                else if (countSuffix == -1)
                {
                    timerSuffix--;
                    if (timerSuffix <= 0)
                    {
                        countSuffix = 0;
                        npc.dontTakeDamage = false;
                    }
                }
                else
                {
                    npc.alpha += 10;
                    if (npc.alpha > 250)
                    {
                        npc.alpha = 250;
                    }
                }
            }
            if (suffixes.Contains(" the Reaper"))
            {
                if (Main.rand.Next(0, 6) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.PortalBoltTrail);
                }
            }
            //After this point, all prefixes must have a target to function.
            if (targetInfo == null)
            {
                return;
            }

            if (prefixes.Contains("Volcanic ") && distance < 450 && Main.netMode != 2)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, 262);
                }
                timerAI = Math.Min(timerAI + 1, 300);
                if (timerAI >= 300 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 4, -(npc.position.Y - target.position.Y) / distance * 4, 467, (int)((npc.damage * 1.2) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Shadowmage ") && distance < 450 && Main.netMode != 2)
            {
                timerAI = Math.Min(timerAI + 1, 300);
                if (timerAI >= 300 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 4, -(npc.position.Y - target.position.Y) / distance * 4, 468, (int)((npc.damage * 1.2) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Psychic ") && distance < 600)
            {
                postMoonTimer = Math.Min(timerAI + 1, 100);
                if ((postMoonTimer >= 90 || Main.rand.Next(0, 100) == 0) && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    target.velocity += new Vector2(Main.rand.Next(-25, 26) / 2, Main.rand.Next(-25, 26) / 2);
                    postMoonTimer = 0;
                }
            }
            if (prefixes.Contains("Bionic ") && distance < 450 && Main.netMode != 2)
            {
                timerAI = Math.Min(timerAI + 1, 240);
                if (timerAI >= 240 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 9, -(npc.position.Y - target.position.Y) / distance * 9, ProjectileID.DeathLaser, (int)((npc.damage) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Generous ") && distance < 450)
            {
                timerAI = Math.Min(timerAI + 1, 240);
                if (timerAI >= 240 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height) && npc.value > 0)
                {
                    int rand = Main.rand.Next(0, 100);
                    int type;
                    float mult = .5f;
                    if (rand >= 90 && npc.value >= 1000000)
                    {
                        mult = 3;
                        type = 161;
                        npc.value -= 1000000;
                    }
                    else if (rand >= 75 && npc.value >= 10000)
                    {
                        mult = 2f;
                        type = 160;
                        npc.value -= 10000;
                    }
                    else if (rand >= 40 && npc.value >= 100)
                    {
                        mult = 1f;
                        type = 159;
                        npc.value -= 100;
                    }
                    else
                    {
                        type = 158;
                        npc.value -= 1;
                    }
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 9, -(npc.position.Y - target.position.Y) / distance * 9, type, (int)((npc.damage * mult) * damageMult), 0);
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    if (Main.netMode == 2)
                    {
                        Main.projectile[p].alpha = 255;
                    }
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Philanthropic ") && distance < 450)
            {
                timerAI = Math.Min(timerAI + 1, 360);
                if (timerAI >= 360 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height) && npc.value > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        float vX = -(npc.position.X - target.position.X) / distance * 9 + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;
                        float vY = -(npc.position.Y - target.position.Y) / distance * 9 + (float)Main.rand.Next(-10, 10 + 1) * 0.3f;
                        int rand = Main.rand.Next(0, 100);
                        int type;
                        float mult = .5f;
                        if (rand >= 90 && npc.value >= 1000000)
                        {
                            mult = 3;
                            type = 161;
                            npc.value -= 1000000;
                        }
                        else if (rand >= 75 && npc.value >= 10000)
                        {
                            mult = 2f;
                            type = 160;
                            npc.value -= 10000;
                        }
                        else if (rand >= 40 && npc.value >= 100)
                        {
                            mult = 1f;
                            type = 159;
                            npc.value -= 100;
                        }
                        else
                        {
                            type = 158;
                            npc.value -= 1;
                        }
                        int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vX, vY, type, (int)((npc.damage * mult * .8) * damageMult), 0);
                        Main.projectile[p].hostile = true;
                        Main.projectile[p].friendly = false;
                        if (Main.netMode == 2)
                        {
                            Main.projectile[p].alpha = 255;
                        }
                        timerAI = 0;
                    }
                }
            }
            if (prefixes.Contains("Shadowflame ") && distance < 450 && Main.netMode != 2)
            {
                timerAI += 1;
                if (timerAI > 360)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 4, -(npc.position.Y - target.position.Y) / distance * 4, 299, (int)((npc.damage) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Dune Mage ") && distance < 300 && Main.netMode != 2)
            {
                timerAI += 1;
                if (timerAI > 180)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 1.5f, -(npc.position.Y - target.position.Y) / distance * 1.5f, 596, (int)((npc.damage * 1.3) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Bubbly ") && distance < 250 && Main.netMode != 2)
            {
                timerAI = Math.Min(timerAI + Main.rand.Next(1, 3), 90);
                if (timerAI >= 90 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Vector2 vector82 = -target.Center + npc.Center;
                    vector82 = -vector82;
                    Vector2 vector83 = Vector2.Normalize(vector82) * 9f;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vector83.X, vector83.Y, 410, (int)((npc.damage * .7) * damageMult), 0);
                    Main.projectile[p].scale *= 1.4f;
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].timeLeft = 90;
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Sonorous ") && distance < 450 && Main.netMode != 2)
            {
                timerAI = Math.Min(timerAI + Main.rand.Next(1, 3), 150);
                if (timerAI >= 150 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Vector2 vector82 = -target.Center + npc.Center;
                    vector82 = -vector82;
                    Vector2 vector83 = Vector2.Normalize(vector82) * 4f;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vector83.X, vector83.Y, Main.rand.Next(76, 79), (int)((npc.damage * .9) * damageMult), 0);
                    Main.projectile[p].damage /= 2;
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].timeLeft = 300;
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Hellwing ") && distance < 350 && Main.netMode != 2)
            {
                timerAI = Math.Min(timerAI + 1, 90);
                if (timerAI >= 90 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Vector2 vector82 = -target.Center + npc.Center;
                    vector82 = -vector82;
                    Vector2 vector83 = Vector2.Normalize(vector82) * 7f;
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, vector83.X, vector83.Y, 485, (int)((npc.damage * .9) * damageMult), 0, Main.myPlayer, vector82.ToRotation());
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].timeLeft = 90;
                    Main.projectile[p].ai[0] = Main.rand.Next(-6, 13) / 2;
                    Main.projectile[p].ai[1] = Main.rand.Next(-6, 13) / 2;
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Lightning Rod ") && distance < 450 && Main.netMode != 2)
            {
                if (Main.rand.Next(0, 10) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
                }
                timerAI = Math.Min(timerAI + 1, 180);
                if (timerAI >= 180 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 12, -(npc.position.Y - target.position.Y) / distance * 12, 435, (int)((npc.damage) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (suffixes.Contains(" the Lightning God") && distance < 800 && Main.netMode != 2)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
                }
                timerSuffix = Math.Min(timerSuffix + 1, 90);
                if (Main.rand.Next(0, 80) == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, npc.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
                    Vector2 vector82 = new Vector2(Main.rand.Next(-50, 51), 800);
                    float ai = Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 8;
                    Projectile.NewProjectile(target.position.X + Main.rand.Next(-100, 101), target.position.Y - 900, vector83.X, vector83.Y, 466, (int)(npc.damage * damageMult), 0f, target.whoAmI, vector82.ToRotation(), ai);
                }
                if (timerSuffix >= 60 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Main.PlaySound(SoundLoader.customSoundType, npc.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
                    Vector2 vector82 = target.position - npc.position;
                    float ai = Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 8;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector83.X, vector83.Y, 580, (int)(npc.damage * damageMult), 0f, target.whoAmI, vector82.ToRotation(), ai);
                    timerSuffix = 0;
                }
            }
            if (prefixes.Contains("Rune Mage ") && distance < 450 && Main.netMode != 2)
            {
                timerAI += 1;
                if (timerAI > 480)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 4, -(npc.position.Y - target.position.Y) / distance * 4, 129, (int)((npc.damage) * damageMult), 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Party Animal ") && distance < 400 && Main.netMode != 2)
            {
                timerAI += 1;
                if (timerAI > 180)
                {
                    int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, -(npc.position.X - target.position.X) / distance * 8, -(npc.position.Y - target.position.Y) / distance * 8, 289, npc.damage, 0);
                    timerAI = 0;
                }
            }
            if (prefixes.Contains("Voodoo "))
            {
                specialTimer++;
                if (specialTimer > 60)
                {
                    for (int i = 0; i < npc.buffType.Length; i++)
                    {
                        if (npc.buffTime[i] > 0 && npc.buffType[i] > 0)
                        {
                            target.AddBuff(npc.buffType[i], npc.buffTime[i]);
                        }
                    }
                    specialTimer = 0;
                }
            }

            if (prefixes.Contains("Malefic "))
            {
                postMoonTimer += 800 / distance;
                if (postMoonTimer > 60)
                {
                    if (target.statLife > 1)
                    {
                        int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame);
                        target.statLife -= 1;
                        CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 50, target.width, target.height), new Color(255, 65, 0, 255), "" + 1);

                    }
                    postMoonTimer = 0;
                }
            }
            if (prefixes.Contains("Channeler "))
            {
                postMoonTimer += 1000 / distance;
                if (postMoonTimer > 600)
                {
                    target.statMana = Math.Max(target.statMana - 10, 0);
                    postMoonCount++;
                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y - 50, target.width, target.height), new Color(0, 30, 200, 255), "" + 10);
                    postMoonTimer = 0;
                }
                if (postMoonCount >= 5)
                {
                    postMoonCount = 0;
                    ArrayList toBuff = getNPCsInRange(npc, 400);
                    for (int i = 0; i < toBuff.Count; i++)
                    {
                        NPC n = (NPC)toBuff[i];
                        n.damage = (int)(n.damage * 1.1);
                        n.knockBackResist = (float)(n.knockBackResist / 1.1);
                        n.lifeMax = (int)(n.lifeMax * 1.1);
                        n.life = Math.Min(n.life + 200, n.lifeMax);
                        CombatText.NewText(new Rectangle((int)n.position.X, (int)n.position.Y - 20, n.width, n.height), new Color(50, 255, 50, 255), "" + 200);
                        CombatText.NewText(new Rectangle((int)n.position.X, (int)n.position.Y - 50, n.width, n.height), new Color(255, 65, 0, 255), "Status Up!");
                    }
                }
            }

            if (suffixes.Contains(" the Nullifier"))
            {
                if (Main.rand.Next(0, 6) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.CrystalPulse2);
                }
                if (distance < 250)
                {
                    timerSuffix++;
                    if (timerSuffix > 15)
                    {
                        target.AddBuff(BuffID.Cursed, 20);
                        timerSuffix = 0;
                    }
                }
            }
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
                if(NPCID.KingSlime == npc.type)
                {
                    npc.GivenName = "Void King";
                    if(Main.rand.Next(30) == 1 && npc.life < npc.lifeMax)
                    {
                        npc.life++;
                    }
                    if(Main.rand.Next(5) == 1)
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
                    bool hasPrefix = prefixes.Length > 0;
                    netMessage.Write(hasPrefix);
                    if (hasPrefix)
                    {
                        netMessage.Write(prefixes);
                    }
                    bool hasSuffix = suffixes.Length > 0;
                    netMessage.Write(hasSuffix);
                    if (hasSuffix)
                    {
                        netMessage.Write(suffixes);
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
                    if(prefixes.Length <= 1)
                    {
                        npc.GivenName = npc.FullName;
                    }
                    if (!npc.GivenName.Contains(suffixes))
                    {
                        npc.GivenName += suffixes;
                    }
                }
                //removes Rare from statue-spawned enemies
                if (prefixes.Contains("Rare ") && npc.value == 0f && npc.npcSlots == 0f)
                {
                    prefixes = prefixes.Replace("Rare ", "");
                    npc.GivenName = npc.GivenName.Replace("Rare ", "");
                    npc.lifeMax /= 2;
                    npc.life /= 2;
                }
                nameConfirmed = true;
            }
            //EXPERIMENTAL STUN DEBUFF
            if (npc.FindBuffIndex(mod.BuffType("Stunned")) >= 0 || npc.FindBuffIndex(mod.BuffType("Suspended")) >= 0)
            {
                return false;
            }
            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
            if (gunbladeMeleeDebuffTime > 0)
            {
                gunbladeMeleeDebuffTime--;
                if (gunbladeMeleeDebuffTime == 0)
                {
                    gunbladeMeleeDebuff = 0;
                }
            }
            if (gunbladeRangedDebuffTime > 0)
            {
                gunbladeRangedDebuffTime--;
                if (gunbladeRangedDebuffTime == 0)
                {
                    gunbladeRangedDebuff = 0;
                }
            }
            base.PostAI(npc);
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {

            int shaderID = 0;

            //set shaderID based on prefix here
            if (suffixes.Contains("Reaper"))
            {
                shaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemID.GrimDye);
            }
            if (suffixes.Contains("Nullifier"))
            {
                shaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemID.PurpleOozeDye);
            }
            if (prefixes.Contains("Mirrored"))
            {
                shaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveSilverDye);
            }
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

            DrawData data = new DrawData();
            data.origin = npc.Center;
            data.position = npc.position - Main.screenPosition;
            data.scale = new Vector2(npc.scale, npc.scale);
            data.texture = Main.npcTexture[npc.type];
            data.sourceRect = npc.frame;//data.texture.Frame(1, Main.npcFrameCount[npc.type], 0, npc.frame);
            GameShaders.Armor.ApplySecondary(shaderID, npc, data);

            return true;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateScale(1f, 1f, 1f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(0f, 0f, 0f)));
        }

        //gets all NPCs that can have prefixes within given distance of given NPC
        private ArrayList getNPCsInRange(NPC focus, int distance)
        {
            ArrayList NPCsInRange = new ArrayList();
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.Distance(focus.position) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && ((npc.aiStyle != 6 && npc.aiStyle != 37) ^ Array.Exists(types, element => element == npc.type)) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc != focus)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
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
        public static int spawnNPC(int type, int plr)
        {
            if (Main.netMode == 1)
            {
                return -1;
            }
            else
            {
                int spawnRangeX = (int)((NPC.sWidth / 16) * 0.7);
                int spawnRangeY = (int)((NPC.sHeight / 16) * 0.7);
                if (type != 398)
                {
                    bool flag = false;
                    int num9 = 0;
                    int num10 = 0;
                    int num11 = (int)(Main.player[plr].position.X / 16f) - spawnRangeX * 2;
                    int num12 = (int)(Main.player[plr].position.X / 16f) + spawnRangeX * 2;
                    int num13 = (int)(Main.player[plr].position.Y / 16f) - spawnRangeY * 2;
                    int num14 = (int)(Main.player[plr].position.Y / 16f) + spawnRangeY * 2;
                    int num15 = (int)(Main.player[plr].position.X / 16f) - NPC.safeRangeX;
                    int num16 = (int)(Main.player[plr].position.X / 16f) + NPC.safeRangeX;
                    int num17 = (int)(Main.player[plr].position.Y / 16f) - NPC.safeRangeY;
                    int num18 = (int)(Main.player[plr].position.Y / 16f) + NPC.safeRangeY;
                    if (num11 < 0)
                    {
                        num11 = 0;
                    }
                    if (num12 > Main.maxTilesX)
                    {
                        num12 = Main.maxTilesX;
                    }
                    if (num13 < 0)
                    {
                        num13 = 0;
                    }
                    if (num14 > Main.maxTilesY)
                    {
                        num14 = Main.maxTilesY;
                    }
                    for (int n = 0; n < 1000; n++)
                    {
                        int num19 = 0;
                        while (num19 < 100)
                        {
                            int num20 = Main.rand.Next(num11, num12);
                            int num21 = Main.rand.Next(num13, num14);
                            if (Main.tile[num20, num21].nactive() && Main.tileSolid[(int)Main.tile[num20, num21].type])
                            {
                                goto IL_7E8;
                            }
                            if ((!Main.wallHouse[(int)Main.tile[num20, num21].wall] || n >= 999) && (type != 50 || n >= 500 || Main.tile[num21, num21].wall <= 0))
                            {
                                int num22 = num21;
                                while (num22 < Main.maxTilesY)
                                {
                                    if (Main.tile[num20, num22].nactive() && Main.tileSolid[(int)Main.tile[num20, num22].type])
                                    {
                                        if (num20 < num15 || num20 > num16 || num22 < num17 || num22 > num18 || n == 999)
                                        {
                                            ushort arg_66F_0 = Main.tile[num20, num22].type;
                                            num9 = num20;
                                            num10 = num22;
                                            flag = true;
                                            break;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        num22++;
                                    }
                                }
                                if (flag && type == 50 && n < 900)
                                {
                                    int num23 = 20;
                                    if (!Collision.CanHit(new Vector2((float)num9, (float)(num10 - 1)) * 16f, 16, 16, new Vector2((float)num9, (float)(num10 - 1 - num23)) * 16f, 16, 16) || !Collision.CanHit(new Vector2((float)num9, (float)(num10 - 1 - num23)) * 16f, 16, 16, Main.player[plr].Center, 0, 0))
                                    {
                                        num9 = 0;
                                        num10 = 0;
                                        flag = false;
                                    }
                                }
                                if (!flag || n >= 999)
                                {
                                    goto IL_7E8;
                                }
                                int num24 = num9 - 3 / 2;
                                int num25 = num9 + 3 / 2;
                                int num26 = num10 - 3;
                                int num27 = num10;
                                if (num24 < 0)
                                {
                                    flag = false;
                                }
                                if (num25 > Main.maxTilesX)
                                {
                                    flag = false;
                                }
                                if (num26 < 0)
                                {
                                    flag = false;
                                }
                                if (num27 > Main.maxTilesY)
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    for (int num28 = num24; num28 < num25; num28++)
                                    {
                                        for (int num29 = num26; num29 < num27; num29++)
                                        {
                                            if (Main.tile[num28, num29].nactive() && Main.tileSolid[(int)Main.tile[num28, num29].type])
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                    goto IL_7E8;
                                }
                                goto IL_7E8;
                            }
                            IL_7F0:
                            num19++;
                            continue;
                            IL_7E8:
                            if (!flag && !flag)
                            {
                                goto IL_7F0;
                            }
                            break;
                        }
                        if (flag && n < 999)
                        {
                            Rectangle rectangle = new Rectangle(num9 * 16, num10 * 16, 16, 16);
                            for (int num30 = 0; num30 < 255; num30++)
                            {
                                if (Main.player[num30].active)
                                {
                                    Rectangle rectangle2 = new Rectangle((int)(Main.player[num30].position.X + (float)(Main.player[num30].width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(Main.player[num30].position.Y + (float)(Main.player[num30].height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                                    if (rectangle.Intersects(rectangle2))
                                    {
                                        flag = false;
                                    }
                                }
                            }
                        }
                        if (flag)
                        {
                            break;
                        }
                    }
                    int num31 = -1;
                    if (flag)
                    {
                        num31 = NPC.NewNPC(num9 * 16 + 8, num10 * 16, type, 1, 0f, 0f, 0f, 0f, 255);
                        Main.npc[num31].target = plr;
                        Main.npc[num31].timeLeft *= 20;
                        if (Main.netMode == 2 && num31 < 200)
                        {
                            NetMessage.SendData(23, -1, -1, null, num31, 0f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    return num31;
                }
            }
            return -1;
        }
    }
    }
