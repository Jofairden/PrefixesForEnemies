using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace EnemyMods.Items.Tier5
{
    public class UndyingRing : ModItem
    {
        int maxCharges = 1;
        int rechargeTime = 1800;
        int charges = 1;
        int rechargeCount = 0;

        //conditional charge modifiers go here

        public override void SetDefaults()
        {

            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 80000;
            item.rare = 8;
            item.UseSound = SoundID.Item43;
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Ring of the Undying");
      Tooltip.SetDefault("Refuse death for 5 seconds. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[14] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("Undying"), (int)(5 * player.magicDamage) * 60);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[14]--;
            if (play.cooldowns[14] == -1)
            {
                play.cooldowns[14] = play.maxCooldowns[14];
            }
            return true;
        }
        /*
        public override void UpdateInventory(Player player)
        {
            item.ToolTip = "Refuse death for " + (int)(5*player.magicDamage) + " seconds. One charge.";
        }
        */
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
