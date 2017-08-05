using EnemyMods.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EnemyMods
{
    public class MPlayer : ModPlayer
    {
        public bool embellishedRegen = false;
        public bool fireSpirit = false;
        public bool waterSpirit = false;
        public bool soulMinion = false;
        public bool gunTurret = false;
        public bool bowTurret = false;
        public bool rocketTurret = false;
        public bool shotgunTurret = false;
        public bool chargeBangle = false;
        public bool moonIdol = false;
        public bool dyingStar = false;
        public bool undying = false;
        public bool bloodWell = false;
        public bool killWell = true;
        public int bloodArmor = 0;
        public bool reducedCounterCD = false;
        public bool increasedCounterLength = false;
        public bool counterPlus = false;
        public int gunbladeMeleeDebuff = 0;
        public int gunbladeRangedDebuff = 0;
        public int voidTargetDamage = 0;
        public int voidBurn = 0;
        public float voidAffinity = 1f;
        public int typeNeedles = 0;
        public int needleTime = 0;
        public int needleDamage = 0;
        //potions
        public bool duelistDraught = false;
        public int duelistTarget = 0;
        public bool tenacity = false;
        public int tenacityLifeCount = 0;
        public bool shockTonic = false;
        public float shockCharge = 0;
        public bool flightElixir = false;
        public bool earthenDraught = false;
        public bool steelElixir = false;
        public int steelDefense = 0;
        public int steelTime = 0;
        public bool reconstruction = false;
        public float reconstructionHeal = 0;
        public int reconstructionTime = 0;
        public bool battleDance = false;
        public int battleDanceDamage = 0;
        //essences
        public bool vengeful = false;
        //cooldowns and charges by index of spell, by tier then alphabetically
        public int[] cooldowns = new int[] { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 };
        public int[] charges = new int[19];
        private int[] maxCharges = new int[] {2, 2, 1, 1, 1, 2, 1, 2, 2, 2, 2, 1, 1, 1, 1, 2, 1, 1, 2};
        public int[] maxCooldowns = new int[] {600, 900, 1500, 1500, 2100, 900, 1500, 480, 720, 1500, 600, 2100, 1800, 1500, 1800, 720, 1200, 1800, 480};
        private string[] chargeText = new string[] {"Ice Shard", "Scourge", "Acid Rain", "Razorwind", "Death Knell", "Deep Scourge", "Fire Rain", "Lightning", "Light Spear", "Shadowflame", "Shattershard", "Banshee Howl", "Petalstorm", "Soul Well", "Undying", "Holy Lance", "Nebula Parasite", "Solar Shower", "Vortex Lightning" };
        private bool[] noText = new bool[19];

        public override void Initialize()
        {
            cooldowns = new int[] { -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2, -2 };
            charges = new int[19];
            noText = new bool[19];
        }

        public override void ResetEffects()
        {
            gunTurret = false;
            bowTurret = false;
            rocketTurret = false;
            shotgunTurret = false;
            embellishedRegen = false;
            fireSpirit = false;
            waterSpirit = false;
            soulMinion = false;
            chargeBangle = false;
            moonIdol = false;
            dyingStar = false;
            undying = false;
            killWell = true;
            reducedCounterCD = false;
            increasedCounterLength = false;
            counterPlus = false;
            gunbladeMeleeDebuff = 0;
            gunbladeRangedDebuff = 0;
            //potions
            duelistDraught = false;
            tenacity = false;
            tenacityLifeCount = player.lifeRegenTime;
            shockTonic = false;
            flightElixir = false;
            earthenDraught = false;
            steelElixir = false;
            reconstruction = false;
            battleDance = false;
            //essences
            vengeful = false;
        }
        public override void PreUpdate()
        {
        }
        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            if(b >= 0)
            {
                player.buffTime[b] += (int)(damage * 30);
            }
            if (tenacity)
            {
                player.lifeRegenTime = tenacityLifeCount - 300;
                if (player.lifeRegenTime < 0)
                {
                    player.lifeRegenTime = 0;
                }
            }
            if (steelElixir)
            {
                steelDefense = Math.Min(20, steelDefense + 4);
                steelTime = 900;
            }
            if (reconstruction)
            {
                reconstructionHeal = (float)damage;
                reconstructionTime = 600;
            }
            battleDanceDamage = 0;
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (undying)
            {
                player.statLife = 1;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 50, player.width, player.height), new Color(255, 100, 100, 255), "UNDYING");
                return false;
            }
            return true;
        }
        public override void PreUpdateBuffs()
        {
            steelTime--;
            if (steelTime <= 0)
            {
                steelTime = 0;
                steelDefense = 0;
            }
            reconstructionTime--;
            if (reconstructionTime <= 0)
            {
                reconstructionTime = 0;
            }
            for (int k = 3; k < 8 + player.extraAccessorySlots; k++)
            {
                if (player.armor[k].type == mod.ItemType("ChargeBangle"))
                {
                    chargeBangle = true;
                }
                if (player.armor[k].type == mod.ItemType("MoonIdol"))
                {
                    moonIdol = true;
                }
                if (player.armor[k].type == mod.ItemType("BloodMagePact"))
                {
                    killWell = false;
                }
            }
            if (killWell)
            {
                int p = player.FindBuffIndex(mod.BuffType("BloodWell"));
                if(p >= 0)
                {
                    player.DelBuff(p);
                    p--;
                }
            }
        }
        public override void PostUpdateBuffs()
        {
            if(battleDance && battleDanceDamage >= 20)
            {
                player.AddBuff(BuffID.Hunter, 2);
            }
        }
        public override void PostUpdateEquips()
        {
            player.statDefense += steelDefense;
            player.meleeDamage += (battleDanceDamage / 100f);
            player.rangedDamage += (battleDanceDamage / 100f);
            player.magicDamage += (battleDanceDamage / 100f);
            player.thrownDamage += (battleDanceDamage / 100f);
            player.minionDamage += (battleDanceDamage / 100f);
            if (player.FindBuffIndex(mod.BuffType("BloodArmor")) == -1)
            {
                bloodArmor = 0;
            }
            if (bloodArmor > 0)
            {
                player.endurance += .1f;
                player.statDefense += bloodArmor;
            }
            //throw charged needles
            if(needleTime > 0)
            {
                if (needleTime % 3 == 0)
                {
                    Vector2 vel = Main.MouseWorld - player.Center;
                    vel.Normalize();
                    vel *= 12 * player.thrownVelocity;
                    int p = Projectile.NewProjectile(player.Center.X, player.Center.Y, vel.X, vel.Y, typeNeedles, needleDamage, .5f, player.whoAmI);
                    Main.PlaySound(2, player.position, 1);
                }
                needleTime--;
            }
            //decrement cooldowns and add charges
            for (int i=0; i<cooldowns.Length; i++)
            {
                if (cooldowns[i] == -2)
                {
                    charges[i] = maxCharges[i];
                    cooldowns[i] = -1;
                }
                if (maxCharges[i] == 1)
                {
                    if (cooldowns[i] == -1 && charges[i] < maxCharges[i])
                    {
                        cooldowns[i] = maxCooldowns[i];
                        noText[i] = true;
                    }
                }
                else if (cooldowns[i] == -1 && charges[i] < maxCharges[i] + (chargeBangle ? 1 : 0))
                {
                    cooldowns[i] = maxCooldowns[i];
                    noText[i] = true;
                }
                if (cooldowns[i] == 0)
                {
                    charges[i]++;
                    if (!noText[i])
                    {
                        if(maxCharges[i]!=1)
                            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 50, player.width, player.height), new Color(255, 255, 255, 255), chargeText[i] + " " + charges[i] + "/" + (maxCharges[i] + (chargeBangle ? 1 : 0)));
                        else
                            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 50, player.width, player.height), new Color(255, 255, 255, 255), chargeText[i] + " " + charges[i] + "/" + maxCharges[i]);
                    }
                    noText[i] = false;
                    if (charges[i] >= maxCharges[i] && (maxCharges[i] == 1 || charges[i] == maxCharges[i] + (chargeBangle ? 1 : 0)))
                    {
                        cooldowns[i] = -1;
                    }
                    else
                    {
                        cooldowns[i] = maxCooldowns[i];
                    }
                }
                if (cooldowns[i] > 0)
                {
                    cooldowns[i]--;
                    if(embellishedRegen && cooldowns[i] % 2 == 1)
                    {
                        cooldowns[i]--;
                    }
                }
                if(!chargeBangle && charges[i] > maxCharges[i])
                {
                    charges[i]--;
                }
            }
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (moonIdol && crit && Main.rand.Next(0, 5)==0)
            {
                damage *= 2;
            }
            if(gunbladeMeleeDebuff>0 && item.melee && crit)
            {
                gNPC info = target.GetGlobalNPC<gNPC>();
                info.gunbladeMeleeDebuff = gunbladeMeleeDebuff;
                info.gunbladeMeleeDebuffTime = 180;
            }
            if (duelistDraught)
            {
                if(duelistTarget == target.whoAmI)
                {
                    damage = (int)(damage * 1.1);
                }
                duelistTarget = target.whoAmI;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (moonIdol && crit && Main.rand.Next(0, 5) == 0)
            {
                damage *= 2;
            }
            if (gunbladeRangedDebuff > 0 && proj.ranged && crit)
            {
                gNPC info = target.GetGlobalNPC<gNPC>();
                info.gunbladeRangedDebuff = gunbladeRangedDebuff;
                info.gunbladeRangedDebuffTime = 180;
            }
            if (duelistDraught)
            {
                if (duelistTarget == target.whoAmI)
                {
                    damage = (int)(damage * 1.1);
                }
                duelistTarget = target.whoAmI;
            }
        }
        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (duelistDraught)
            {
                if (duelistTarget == npc.whoAmI)
                {
                    damage = (int)(damage * .9);
                }
            }
            if (player.FindBuffIndex(mod.BuffType("CounterStanceRapier")) >= 0)
            {
                player.AddBuff(mod.BuffType("Counter"), 300);
                damage = 0;
                player.immune = true;
                player.immuneTime = 60;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceEstoc")) >= 0)
            {
                player.AddBuff(mod.BuffType("Counter"), 300);
                damage = 0;
                player.immune = true;
                player.immuneTime = 60;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceEpee")) >= 0 || player.FindBuffIndex(mod.BuffType("CounterStanceEpee2")) >= 0 || player.FindBuffIndex(mod.BuffType("CounterStanceEpee3")) >= 0)
            {
                if(npc.aiStyle == 9)// this style behaves like a projectile, but can't be reflected, so we kill it instead
                {
                    npc.StrikeNPC(999, 0, 0);
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 999, 0, 0, 0);
                }
                damage = 0;
                player.immune = true;
                player.immuneTime = 60;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceFoil")) >= 0)
            {
                int direction = (player.position.X >= npc.position.X) ? -1 : 1;
                int dam = (int)(10 * player.meleeDamage * (Main.expertMode ? .7 : 1)) + damage;
                dam = (int)(dam * Main.rand.Next(90, 111)/100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, direction, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                player.addDPS(2 * dam);
                player.immune = true;
                player.immuneTime = 60;
                damage = 0;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceFoil2")) >= 0)
            {
                int direction = (player.position.X >= npc.position.X) ? -1 : 1;
                int dam = (int)(52 * player.meleeDamage * (Main.expertMode ? .7 : 1)) + (damage*3)/2;
                dam = (int)(dam * Main.rand.Next(90, 111) / 100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, direction, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                player.addDPS(2 * dam);
                player.immune = true;
                player.immuneTime = 60;
                damage = 0;

                npc.AddBuff(BuffID.CursedInferno, 600);
                for(int i=0; i<12; i++)
                {
                    int d = Dust.NewDust(player.position, 38, 38, 75);
                    Main.dust[d].velocity *= 3;
                    Main.dust[d].scale = 1.3f;
                }
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceFoil3")) >= 0)
            {
                int dam = (int)(80 * player.meleeDamage) + (int)(damage* 2.25 * (Main.expertMode ? .7 : 1));
                if (npc.boss || (npc.aiStyle == 6 && !npc.FullName.Contains("Head")))
                {
                    dam = (int)(dam*1.6);//bonus damage to compensate for no stun
                }
                else
                {
                    npc.AddBuff(mod.BuffType("Stunned"), 180);
                }
                dam = (int)(dam * Main.rand.Next(90, 111) / 100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, 0, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, 0, 1);
                player.addDPS(2 * dam);
                player.immune = true;
                player.immuneTime = 60;
                damage = 0;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceFoil4")) >= 0)
            {
                int direction = (player.position.X >= npc.position.X) ? -1 : 1;
                int dam = (int)(140 * player.meleeDamage * (Main.expertMode ? .7f : 1)) + damage * 3;
                if (npc.boss || (npc.aiStyle == 6 && !npc.FullName.Contains("Head")))
                {
                    dam = (int)(dam * 2);
                }
                else
                {
                    npc.AddBuff(mod.BuffType("Suspended"), 180);
                }
                dam = (int)(dam * Main.rand.Next(90, 111) / 100.0);
                if (counterPlus)
                {
                    dam = (int)(dam * 1.2);
                }
                npc.StrikeNPC(dam, 10, direction, true);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                player.addDPS(2 * dam);
                player.immune = true;
                player.immuneTime = 60;
                damage = 0;
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            if (player.FindBuffIndex(mod.BuffType("CounterStanceRapier")) >= 0)
            {
                player.AddBuff(mod.BuffType("Counter"), 600);
                damage = 0;
                player.immune = true;
                player.immuneTime = 60;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceEstoc")) >= 0)
            {
                player.AddBuff(mod.BuffType("Counter"), 600);
                damage = 0;
                player.immune = true;
                player.immuneTime = 60;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceEpee")) >= 0)
            {
                //reflects projectile, generally makes it damage eneimes and ignore players
                proj.velocity = -proj.velocity;
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = (int)((proj.damage+10) * 4 * player.meleeDamage);
                if (counterPlus)
                {
                    proj.damage = (int)(proj.damage * 1.2);
                }
                damage = 0;
                //leaves the player with very brief immunity so they can reflect further projectiles
                player.immune = true;
                player.immuneTime = 6;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceEpee2")) >= 0)
            {
                proj.velocity = -proj.velocity * 1.5f;
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = (int)((proj.damage+20) * 6 * player.meleeDamage);
                if (counterPlus)
                {
                    proj.damage = (int)(proj.damage * 1.2);
                }
                player.buffTime[player.FindBuffIndex(mod.BuffType("CounterCooldown"))] -= 30;//reduces counter cooldown by .5 sec for each projectile reflected
                damage = 0;
                player.immune = true;
                player.immuneTime = 6;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceEpee3")) >= 0)
            {
                proj.velocity = -proj.velocity * 2f;
                proj.friendly = true;
                proj.hostile = false;
                proj.damage = (int)((proj.damage+30) * 8 * player.meleeDamage);
                if (counterPlus)
                {
                    proj.damage = (int)(proj.damage * 1.2);
                }
                player.buffTime[player.FindBuffIndex(mod.BuffType("CounterCooldown"))] -= 60;
                for (int i = 0; i < 12; i++)
                {
                    int d = Dust.NewDust(player.position, 38, 38, 135, proj.velocity.X, proj.velocity.Y);
                    Main.dust[d].velocity /= 2;
                    Main.dust[d].scale = 1.3f;
                }
                damage = 0;
                player.immune = true;
                player.immuneTime = 6;
            }
            else if (player.FindBuffIndex(mod.BuffType("CounterStanceFoil")) >= 0 || player.FindBuffIndex(mod.BuffType("CounterStanceFoil2")) >= 0 || player.FindBuffIndex(mod.BuffType("CounterStanceFoil3")) >= 0 || player.FindBuffIndex(mod.BuffType("CounterStanceFoil4")) >= 0)
            {
                damage = 0;
                player.immune = true;
                player.immuneTime = 60;
            }
        }
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (shockTonic && shockCharge >= 100)
            {
                int dam = 30 + player.statDefense;
                int direction = (npc.Center.X - player.Center.X > 0) ? 1 : -1;
                npc.StrikeNPC(dam, 10, direction);
                NetMessage.SendData(28, -1, -1, null, npc.whoAmI, dam, 10, direction, 1);
                shockCharge = 0;
                Main.PlaySound(2, player.position, 66);
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (shockTonic && shockCharge >= 100)
            {
                int dam = 30 + player.statDefense;
                int direction = (target.Center.X - player.Center.X > 0) ? 1 : -1;
                target.StrikeNPC(dam, 10, direction);
                NetMessage.SendData(28, -1, -1, null, target.whoAmI, dam, 10, direction, 1);
                shockCharge = 0;
                Main.PlaySound(2, player.position, 66);
            }
            int index = player.FindBuffIndex(mod.BuffType("VoidBurn"));
            if (index != -1)
            {
                player.buffTime[index] -= (5 + damage);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            int index = player.FindBuffIndex(mod.BuffType("VoidBurn"));
            if(index != -1)
            {
                player.buffTime[index] -= (5 + damage);
            }
        }
        public override void PostUpdateRunSpeeds()
        {
            if (flightElixir)
            {
                player.accRunSpeed *= 1.3f;
                player.wingTimeMax = (int)(player.wingTimeMax * 1.3);
            }
            if (earthenDraught && player.velocity.Y == 0)
            {
                player.endurance += .15f;
            }
        }
        public override void PostUpdate()
        {
            if (shockTonic && player.velocity.Y == 0)
            {
                shockCharge += Math.Abs(player.velocity.X) / 9f;
            }
            if (shockCharge >= 100)
            {
                shockCharge = 100;
                if (Main.rand.Next(0, 12) == 0)
                {
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.Electric);
                }
            }
        }
        public override void UpdateLifeRegen()
        {
            if (reconstructionTime>0 && reconstructionTime%20==0)
            {
                int regen = (int)Math.Max(reconstructionHeal, 1);
                player.lifeRegen += regen;
                if(regen > Main.rand.Next(5, 20))
                {
                    for(int i=0; i < regen/2+1; i++)
                    {
                        int d = Dust.NewDust(player.position - new Vector2(player.width, player.height), player.width * 2, player.height * 2, DustID.SomethingRed);
                        Main.dust[d].velocity = (player.Center - Main.dust[d].position) * .01f;
                    }
                }
            }
        }
        public override void UpdateBadLifeRegen()
        {
            if (player.FindBuffIndex(mod.BuffType("VoidBurn")) == -1)
            {
                voidBurn = 0;
            }
            else
            {
                player.lifeRegenTime = 0;
                if(player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegen -= voidBurn;
            }
        }
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            if (battleDance)
            {
                battleDanceDamage = Math.Min(20, battleDanceDamage + 1);
            }
        }
    }
}
