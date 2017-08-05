using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class GunbladeDefense : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gunblade Defense Bonus");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += player.buffTime[buffIndex]/20;
        }
    }
}
