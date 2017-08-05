using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs.potions
{
    public class MysteriousPhilter : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mysterious Philter");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] % 3600 == 0)
            {
                bool flag = (Main.rand.Next(0, 39) < 19);
                int type = 0;
                //1-18, 104-124 all potions
                if (flag)
                {
                    type = Main.rand.Next(1, 19);
                }
                else
                {
                    type = Main.rand.Next(104, 125);
                }
                player.AddBuff(type, 7200);
            }
        }
    }
}
