using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class Hemoplague : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 11;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item43;
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood: Plague");
      Tooltip.SetDefault("Your well releases a virulent disease");
    }

        public override bool UseItem(Player player)
        {
            Projectile p = null;
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == mod.ProjectileType("BloodWell"))
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 601)
            {
                return false;
            }
            player.buffTime[b] -= 600;
            //todo dust
            ArrayList infect = getNPCsInRange(p, 300);
            foreach (NPC npc in infect)
            {
                npc.AddBuff(mod.BuffType("Hemoplague"), 600);
            }
            return true;
        }
        private ArrayList getNPCsInRange(Projectile focus, int distance)
        {
            ArrayList NPCsInRange = new ArrayList();
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.Distance(focus.position) < distance && npc.aiStyle != 7 && !(npc.catchItem > 0) && npc.type != 401 && npc.type != 488 && npc.life > 0 && npc.active)
                {
                    NPCsInRange.Add(npc);
                }
            }
            return NPCsInRange;
        }
    }
}
