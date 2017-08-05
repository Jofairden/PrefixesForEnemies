using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class Jump : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Jump");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.jumpSpeedBoost += 10f;
            player.maxFallSpeed *= 2;
            player.socialGhost = true;
        }
    }
}
