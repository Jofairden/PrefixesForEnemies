using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class ArtilleryCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Artillery Cooldown");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}
