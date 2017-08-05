using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class Mist : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 15;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 30;
            item.useAnimation = 30;
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
      DisplayName.SetDefault("Blood: Mist");
      Tooltip.SetDefault("Your Well emits a pulse of fine blood mist");
    }

        public override bool UseItem(Player player)
        {
            Projectile p = null;
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            for (int j = 0; j < 1000; j++)
            {
                if (Main.projectile[j].owner == player.whoAmI && Main.projectile[j].type == mod.ProjectileType("BloodWell"))
                {
                    p = Main.projectile[j];
                    break;
                }
            }
            if (p == null || b == -1)
            {
                return false;
            }
            if (player.buffTime[b] <= 121)
            {
                return false;
            }
            player.buffTime[b] -= 120;
            float spread = 360f * 0.0174f;
            float baseSpeed = 2.5f;
            double startAngle = spread / 2;
            double deltaAngle = spread / 30f;
            double offsetAngle;
            int i;
            for (i = 0; i < 30; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(p.Center.X, p.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("BloodMist"), (int)(item.damage * player.magicDamage), item.knockBack, player.whoAmI);
            }
            return true;
        }
    }
}
