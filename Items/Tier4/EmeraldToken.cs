using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier4
{
    class EmeraldToken : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;

            item.value = 10000;
            item.rare = 6;
            item.maxStack = 99;
            item.consumable = true;
            item.UseSound = SoundID.Item4;
            item.useStyle = 3;
            item.useAnimation = 30;
            item.useTime = 30;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Emerald Token");
      Tooltip.SetDefault("What will you get?");
    }

        public override bool UseItem(Player player)
        {
            //tier 4 loot
            int x = Main.rand.Next(0, 8);
            switch (x)
            {
                case 0:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("EmeraldTicket"), Main.rand.Next(1, 3));
                    goto default;
                case 1:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ChargeBangle"), 1, false, -1);
                    goto default;
                case 2:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("LightSpearRing"), 1, false, -1);
                    goto default;
                case 3:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ShadowflameRing"), 1, false, -1);
                    goto default;
                case 4:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("GunbladeBurst"), 1, false, -1);
                    goto default;
                case 5:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("FrostbiteEpee"), 1, false, -1);
                    goto default;
                case 6:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ShadowflameEstoc"), 1, false, -1);
                    goto default;
                case 7:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("LightcrystalGreatbow"), 1, false, -1);
                    goto default;
                default:
                    Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("EmeraldTicket"), Main.rand.Next(1, 3));
                    break;
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RubyToken"), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
