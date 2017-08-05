using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class CircleSlash : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 50;
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
      DisplayName.SetDefault("Blood: Circle Slash");
      Tooltip.SetDefault("A blade of blood performs a slash around your Well");
    }

        public override bool UseItem(Player player)
        {
            Projectile p = null;
            int b = player.FindBuffIndex(mod.BuffType("BloodWell"));
            for (int i = 0; i < 1000; i++)
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
            if (player.buffTime[b] <= 121)
            {
                return false;
            }
            player.buffTime[b] -= 120;
            int q = Projectile.NewProjectile(p.position.X, p.position.Y, 0, 0, mod.ProjectileType("BloodSword"), (int)(item.damage * player.magicDamage), item.knockBack, player.whoAmI, p.whoAmI);
            return true;
        }
    }
}
