using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items
{
    public class Armor : ModItem
    {
        public override void SetDefaults()
        {

            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item43;
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood: Armor");
      Tooltip.SetDefault("Protect yourself with from your Well");
    }

        public override bool UseItem(Player player)
        {
            Projectile p = null;
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == mod.ProjectileType("BloodWell"))
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 241)
            {
                return false;
            }
            player.buffTime[b] /= 2;
            player.AddBuff(mod.BuffType("BloodArmor"), 1200);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.bloodArmor = player.buffTime[b] / 100;
            return true;
        }
    }
}
