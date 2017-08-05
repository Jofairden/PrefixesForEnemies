using Terraria.ModLoader;

namespace EnemyMods.Items
{
    class ChoiceToken : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 20;
            item.value = 10000;
            item.rare = 3;
            item.maxStack = 99;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Choice Token");
      Tooltip.SetDefault("");
    }

    }
}
