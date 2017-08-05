using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items//be sure to change this to your modname
{
    public class SUMMONITEM:ModItem
    {
        public override void SetDefaults()
        {
            //placeholder stats, adjust as needed

            item.damage = 100;
            item.summon = true;
            item.mana = 10;
            item.width = 26;
            item.height = 28;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("UnboundSoul");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("UnboundSoul");
            item.buffTime = 3600;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Summon Item");
      Tooltip.SetDefault("");
    }

    }
}
