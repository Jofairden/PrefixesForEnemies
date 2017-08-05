using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class GunbladeRanged3 : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gunblade Ranged Bonus");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            bool flag = false;
            int stacks = 1 + player.buffTime[buffIndex] / 120;
            if (stacks >= 5)
            {
                flag = true;
                stacks = 5;
                player.buffTime[buffIndex] = 600;
            }
            player.rangedDamage += .04f * stacks;
            if (flag)
            {
                MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
                modPlayer.gunbladeRangedDebuff = 3;
            }
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return true;
        }
    }
}
