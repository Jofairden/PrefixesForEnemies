using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Essences
{
    public class VengefulEssence : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 10;
            item.height = 10;


            item.value = 10000;
            item.rare = 3;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Vengeful Essence");
      Tooltip.SetDefault("Simply holding this grants you power\nDeal 20% more damage when below 20% life");
    }

        public override void UpdateInventory(Player player)
        {
            MPlayer mplayer = (MPlayer)(player.GetModPlayer(mod, "MPlayer"));
            mplayer.vengeful = true;
        }
    }
}
