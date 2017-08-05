using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier1
{
    public class Panacea : ModItem
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
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Panacea");
      Tooltip.SetDefault("Cures most all debuffs");
    }

        public override bool UseItem(Player player)
        {
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (Main.debuff[player.buffType[i]])
                {
                    player.DelBuff(i);
                }
            }
            return true;
        }
    }
}
