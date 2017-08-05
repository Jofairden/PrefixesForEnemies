using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class CounterStanceRapier : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Counter Stance");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}
