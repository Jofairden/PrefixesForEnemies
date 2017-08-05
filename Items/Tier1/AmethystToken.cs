using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier1
{
    class AmethystToken : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;

            item.value = 10000;
            item.rare = 2;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
            item.useStyle = 3;
            item.useAnimation = 30;
            item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Amethyst Token");
      Tooltip.SetDefault("What will you get?");
    }

        public override bool UseItem(Player player)
        {
            //tier 1 loot
            int x = Main.rand.Next(0, 7);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AmethystTicket"), Main.rand.Next(1,3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ManaBattery"), 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ScourgeRing"), 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("IceShardRing"), 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("SimpleRapier"), 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("FencerFoil"), 1, false, -1);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("WoodGreatbow"), 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AmethystTicket"), Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TopazToken"), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
