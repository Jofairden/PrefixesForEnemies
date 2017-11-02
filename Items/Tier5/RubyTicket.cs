using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier5
{
    class RubyTicket : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.value = 1000;
            item.rare = 8;
            item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ruby Cosmic Ticket");
      Tooltip.SetDefault("");
    }

    }
}
