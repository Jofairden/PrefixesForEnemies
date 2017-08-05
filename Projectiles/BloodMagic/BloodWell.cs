using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections;

namespace EnemyMods.Projectiles.BloodMagic
{
    public class BloodWell : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.scale = 1f;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Well");
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            if (player.dead)
            {
                modPlayer.bloodWell = false;
            }
            if (modPlayer.bloodWell)
            {
                projectile.timeLeft = 2;
            }
            projectile.localAI[0]++;//drain counter
            if(projectile.localAI[0] >= 60)
            {
                int dam = (int)(10 * player.minionDamage);
                ArrayList drainNPCs = getNPCsInRange(projectile, 300);
                for(int i=0; i<drainNPCs.Count; i++) //(NPC npcD in drainNPCs)
                {
                    NPC npcD = (NPC)drainNPCs[i];
                    npcD.life -= (int)(dam * ((npcD.FindBuffIndex(mod.BuffType("Bloodied"))>=0) ? 1.5 : 1));//drain 50% more if bloodied
                    CombatText.NewText(new Rectangle((int)npcD.position.X, (int)npcD.position.Y - 30, npcD.width, npcD.height), CombatText.DamagedHostile, "" + (int)(dam * ((npcD.FindBuffIndex(mod.BuffType("Bloodied")) >= 0) ? 1.5 : 1)));
                    if (npcD.realLife != -1) { npcD = Main.npc[npcD.realLife]; }
                    if (npcD.life <= 0)
                    {
                        npcD.life = 1;
                        if (Main.netMode != 1)
                        {
                            npcD.StrikeNPC(9999, 0f, 0, false, false);
                            if (Main.netMode == 2) { NetMessage.SendData(28, -1, -1, null, npcD.whoAmI, 9999f, 0f, 0f); }
                        }
                    }
                    int d = Dust.NewDust(npcD.position, (npcD.width), (npcD.height), 5, 0, 0, 0, default(Color), 3.5f);
                    Main.dust[d].velocity = (projectile.Center - npcD.Center)/9.8f;
                    Main.dust[d].noGravity = true;
                    
                    if (b>=0)
                    player.buffTime[b] += 30* (int)(dam * ((npcD.FindBuffIndex(mod.BuffType("Bloodied")) >= 0) ? 1.5 : 1));//buff timer keeps track of size
                }
                projectile.localAI[0] = 0;
            }

            if(projectile.ai[0] == 0f)//hovering over player
            {
                projectile.position = new Vector2(player.position.X, player.position.Y - 80 - player.buffTime[b] / 180);
                projectile.tileCollide = false;
            }
            if(projectile.ai[1] >= 0f)//move command issued
            {
                projectile.ai[1]--;
                projectile.tileCollide = true;
            }
            if(projectile.ai[1] <= 0f)//move command complete
            {
                projectile.velocity = Vector2.Zero;
            }
            if (projectile.localAI[1] > 0)//persistent command
            {

            }
            projectile.scale = 1 + player.buffTime[b] / 2400f;
            //projectile.localAI[1] = Math.Max(2, projectile.localAI[1] - 1);
            //projectile.localAI[1] = Math.Min(10800, projectile.localAI[1]);

            if (Main.rand.Next(0, 8) == 0 || Main.rand.Next (0, 100) < player.buffTime[b] / 100)
            {
                Dust.NewDust(projectile.position, (projectile.width), (projectile.height), 5);
                if(Main.rand.Next(0, 3) == 0)
                {
                    Dust.NewDust(projectile.position, (projectile.width), (projectile.height), 60);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            return false;
        }
        private ArrayList getNPCsInRange(Projectile focus, int distance)
        {
            ArrayList NPCsInRange = new ArrayList();
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.realLife != -1)
                {
                    npc = Main.npc[npc.realLife];
                }
                if (npc.Distance(focus.Center) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc.active)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
        }
    }
}
