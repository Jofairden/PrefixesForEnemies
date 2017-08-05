using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    public class MysteriousPhilter : ModItem
    {
        public override void SetDefaults()
        {


            item.maxStack = 30;
            item.value = 1000;
            item.rare = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.consumable = true;
            item.useTurn = true;
            item.width = 14;
            item.height = 24;
            item.UseSound = SoundID.Item3;
            item.buffType = mod.BuffType("MysteriousPhilter");
            item.buffTime = 36000;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Mysterious Philter");
      Tooltip.SetDefault("Grants a random buff each minute");
    }

    }
}
