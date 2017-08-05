using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Counter : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Counter!");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}
