using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Items.Tier2
{
    [AutoloadEquip(EquipType.Body)]
    public class EmbellishedRobe : ModItem
    {

        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = 3;
            item.defense = 5;

        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Embellished Robe");
      Tooltip.SetDefault("+4% magic damage and crit");
    }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += .04f;
            player.magicCrit += 4;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("EmbellishedHood") && legs.type == mod.ItemType("EmbellishedShoes");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "50% faster charge regeneration";
            ((MPlayer)player.GetModPlayer(mod, "MPlayer")).embellishedRegen = true;
        }

        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }*/
    }
}
