using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Undying : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Undying");
            Description.SetDefault("Defy the jaws of death...");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer pinf = ((MPlayer)player.GetModPlayer(mod, "MPlayer"));
            pinf.undying = true;
        }
    }
}
