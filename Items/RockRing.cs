using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Items
{
    public class RockRing : ModItem
    {
        int maxCharges = 3;
        int rechargeTime = 600;
        int charges = 3;
        int rechargeCount = 0;

        //conditional charge modifiers go here

        public override void SetDefaults()
        {

            item.damage = 50;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item43;//change
            item.autoReuse = false;
            //item.shoot = mod.ProjectileType("Rock");
            item.shootSpeed = 8f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Rock Ring");
      Tooltip.SetDefault("Shoots a big rock. Three charges.");
    }

        public override bool CanUseItem(Player player)
        {
            if (charges <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            int p = Projectile.NewProjectile(Main.MouseWorld.X, player.Center.Y - 600, 0, 0, mod.ProjectileType("Rock"), item.damage, item.knockBack, item.owner);
            charges--;
            return false;
        }
        public override void UpdateInventory(Player player)
        {
            if (charges < maxCharges)
            {
                rechargeCount++;
                if (((MPlayer)player.GetModPlayer(mod, "MPlayer")).embellishedRegen && Main.rand.Next(0, 2) == 0)
                {
                    rechargeCount++;
                }
                if (rechargeCount >= rechargeTime)
                {
                    charges++;//consider combat text or other alert
                    rechargeCount = 0;
                }
            }
        }
    }
}
