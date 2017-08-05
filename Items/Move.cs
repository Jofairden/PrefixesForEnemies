using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items
{
    public class Move : ModItem
    {
        public override void SetDefaults()
        {

            //item.damage = 11;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Blood: Move");
      Tooltip.SetDefault("Commands your blood well to a location");
    }

        public override bool UseItem(Player player)
        {
            Projectile p = null;
            for (int i = 999; i >= 0; i--)
            {
                if (Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == mod.ProjectileType("BloodWell"))
                {
                    p = Main.projectile[i];
                    break;
                }
            }
            if (p == null)
            {
                return false;
            }
            Vector2 distance = Main.MouseWorld - p.position;
            Vector2 vel = distance;
            vel.Normalize();
            vel *= 3;
            int time = (int)(distance.Length() / vel.Length());
            p.velocity = vel;
            p.ai[0] = 1f;
            p.ai[1] = time;
            return true;
        }
    }
}
