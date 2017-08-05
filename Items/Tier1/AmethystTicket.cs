using Terraria.ModLoader;

namespace EnemyMods.Items.Tier1
{
    class AmethystTicket : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.value = 1000;
            item.rare = 2;
            item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amethyst Cosmic Ticket");
      Tooltip.SetDefault("");
    }

    }
}
