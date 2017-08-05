using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class ShockTonic : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shock Tonic");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            modPlayer.shockTonic = true;
        }
    }
}
