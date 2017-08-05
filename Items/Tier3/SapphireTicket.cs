using Terraria.ModLoader;

namespace EnemyMods.Items.Tier3
{
    class SapphireTicket : ModItem
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
      DisplayName.SetDefault("Sapphire Cosmic Ticket");
      Tooltip.SetDefault("");
    }

    }
}
