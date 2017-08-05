using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class ShotgunTurret : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Shotgun Turret");
            Description.SetDefault("");//add this
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("ShotgunTurret")] > 0)
            {
                modPlayer.shotgunTurret = true;
            }
            if (!modPlayer.shotgunTurret)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}