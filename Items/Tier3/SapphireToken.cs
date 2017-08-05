using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    class SapphireToken : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;

            item.value = 10000;
            item.rare = 4;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
            item.useStyle = 3;
            item.useAnimation = 30;
            item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Sapphire Token");
      Tooltip.SetDefault("What will you get?");
    }

        public override bool UseItem(Player player)
        {
            //tier 3 loot
            int x = Main.rand.Next(0, 8);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("SapphireTicket"), Main.rand.Next(1, 3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("DeathKnellRing"), 1, false, -1);
                    goto default;
                //case 2:
                    //Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("DragoonLance"), 1, false, -1);
                    //goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("FireRainRing"), 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("GunbladeSpread"), 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("CursedFoil"), 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("BlessedRapier"), 1, false, -1);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ShadowstrungGreatbow"), 1, false, -1);
                    goto default;
                case 7:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("Greatbone"), 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("SapphireTicket"), Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("EmeraldToken"), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
