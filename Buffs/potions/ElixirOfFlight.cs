using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class ElixirOfFlight : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Elixir of Flight");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            modPlayer.flightElixir = true;
        }
    }
}
