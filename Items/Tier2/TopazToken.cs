using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier2
{
    class TopazToken : ModItem
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
      DisplayName.SetDefault("Topaz Token");
      Tooltip.SetDefault("What will you get?");
    }

        public override bool UseItem(Player player)
        {
            //tier 2 loot
            int x = Main.rand.Next(0, 10);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("TopazTicket"), Main.rand.Next(1, 3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AcidRainRing"), 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Gunblade"), 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("RazorwindRing"), 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Gunblade"), 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("EmbellishedHood"));
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("EmbellishedShoes"));
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("EmbellishedRobe"));
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("BlazingEstoc"), 1, false, -1);
                    goto default;
                case 7:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("StingingEpee"), 1, false, -1);
                    goto default;
                case 8:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("HellfireGreatbow"), 1, false, -1);
                    goto default;
                case 9:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AzureGreatbow"), 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("TopazTicket"), Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SapphireToken"), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
