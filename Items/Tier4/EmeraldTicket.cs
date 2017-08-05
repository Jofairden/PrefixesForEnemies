using Terraria.ModLoader;

namespace EnemyMods.Items.Tier4
{
    class EmeraldTicket : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.value = 1000;
            item.rare = 6;
            item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Emerald Cosmic Ticket");
      Tooltip.SetDefault("");
    }

    }
}
