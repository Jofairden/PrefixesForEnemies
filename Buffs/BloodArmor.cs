using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class BloodArmor : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Blood Armor");
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            //todo visual effect, red shader
        }
    }
}
