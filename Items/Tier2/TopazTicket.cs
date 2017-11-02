using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    class TopazTicket : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.value = 1000;
            item.rare = 3;
            item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Topaz Cosmic Ticket");
      Tooltip.SetDefault("");
    }

    }
}
