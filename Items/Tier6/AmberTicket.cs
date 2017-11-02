using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier6
{
    class AmberTicket : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.value = 1000;
            item.rare = 10;
            item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amber Cosmic Ticket");
      Tooltip.SetDefault("");
    }

    }
}
