using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class CounterCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Counter Cooldown");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (modPlayer.reducedCounterCD && player.buffTime[buffIndex]>270)
            {
                player.buffTime[buffIndex] = 270;
            }
        }
    }
}
