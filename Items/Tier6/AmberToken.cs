using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier6
{
    class AmberToken : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;

            item.value = 10000;
            item.rare = 10;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
            item.useStyle = 3;
            item.useAnimation = 30;
            item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amber Token");
      Tooltip.SetDefault("What will you get?");
    }

        public override bool UseItem(Player player)
        {
            //tier 6 loot
            int x = Main.rand.Next(0, 8);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AmberTicket"), Main.rand.Next(1, 3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("MoonIdol"), 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("NebulaParasiteRing"), 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("NebulaEstoc"), 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("SolarRapier"), 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("StardustEpee"), 1, false, -1);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("VortexFoil"), 1, false, -1);
                    goto default;
                case 7:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Orion"), 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AmberTicket"), Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
    }
}
