using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs//be sure to change this to your modname
{
    public class UnboundSoul : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Unbound Soul");
            Description.SetDefault("");//add this
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer modPlayer = (MPlayer)player.GetModPlayer(mod, "MPlayer");//IMPORTANT - you need a ModPlayer class in which you declare a bool "soulMinion = false;"
            if (player.ownedProjectileCounts[mod.ProjectileType("UnboundSoul")] > 0)
            {
                modPlayer.soulMinion = true;
            }
            if (!modPlayer.soulMinion)
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