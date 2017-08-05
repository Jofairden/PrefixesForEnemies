using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class RitualDagger : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 11;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 3;
            //item.UseSound = SoundID.Item43;
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood: Ritual Dagger");
      Tooltip.SetDefault("Bleed yourself to fill your well");
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
            if (p == null || b == -1 || player.FindBuffIndex(BuffID.Bleeding)!=-1)
            {
                return false;
            }
            for (int num259 = 0; num259 < 10; num259++)
            {
                Dust.NewDust(player.position, player.width, player.height, 5, 0f, 0f, 0, default(Color), 1.5f);
            }
            int dam = (int)(player.statLife * .25);
            player.Hurt(new Terraria.DataStructures.PlayerDeathReason(), dam, 0);
            player.AddBuff(BuffID.Bleeding, 1200);
            player.buffTime[b] += dam*60;
            return true;
        }
    }
}
