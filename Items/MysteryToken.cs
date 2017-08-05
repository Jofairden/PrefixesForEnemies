using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items
{
    class MysteryToken : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;

            item.value = 10000;
            item.rare = 3;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
            item.useStyle = 3;
            item.useAnimation = 30;
            item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Mystery Token");
      Tooltip.SetDefault("What will you get?");
    }

        public override bool UseItem(Player player)
        {
            int lootLevel = 0;
            if (NPC.downedBoss1) lootLevel++;
            if (NPC.downedBoss2) lootLevel++;
            if (NPC.downedBoss3) lootLevel++;
            if (NPC.downedQueenBee) lootLevel++;
            if (NPC.downedSlimeKing) lootLevel++;
            if (NPC.downedMechBoss1) lootLevel++;
            if (NPC.downedMechBoss2) lootLevel++;
            if (NPC.downedMechBoss3) lootLevel++;
            if (NPC.downedPlantBoss) lootLevel++;
            if (NPC.downedGolemBoss) lootLevel++;
            if (NPC.downedFishron) lootLevel++;
            if (NPC.downedAncientCultist) lootLevel++;
            if (Main.hardMode) lootLevel++;
            if(lootLevel == 0)
            {

            }
            if(lootLevel == 1)
            {

            }
            if (lootLevel == 2)
            {

            }
            if (lootLevel == 3)
            {

            }
            if (lootLevel == 4)
            {

            }
            if (lootLevel == 5)
            {

            }
            if (lootLevel == 6)
            {

            }
            if (lootLevel == 7)
            {

            }
            if (lootLevel == 8)
            {

            }
            if (lootLevel == 9)
            {

            }
            if (lootLevel == 10)
            {

            }
            if (lootLevel == 11)
            {

            }
            if (lootLevel == 12)
            {

            }
            if (lootLevel == 13)
            {

            }
            return true;
        }
    }
}
