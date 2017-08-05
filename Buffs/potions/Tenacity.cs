using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class Tenacity : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Tenacity Tonic");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            modPlayer.tenacity = true;
        }
    }
}
