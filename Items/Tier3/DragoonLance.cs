using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EnemyMods.Items.Tier3
{
    public class DragoonLance : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 20;
            item.melee = true;
            item.width = 40;
            item.height = 40;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;//check
            item.knockBack = 3;
            item.value = 30000;
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.shoot = 10;//spear proj
            item.shootSpeed = 10f;//modify
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Dragoon Lance");
      Tooltip.SetDefault("Right-click to enable Jump.");
    }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.AddBuff(mod.BuffType("Jump"), 600);
            }
            else if(player.FindBuffIndex(mod.BuffType("Jump")) != -1)
            {
                //initiate dive attack
                item.shoot = 1;//change to downward falling lance
                player.jump = 0;//can't jump to stop
                player.wingTime = 0;//can't use wings to stop
                player.velocity.X = 0;
                player.velocity.Y -= 4;
            }
            return true;
        }
        public override void GetWeaponDamage(Player player, ref int damage)
        {
            if (player.FindBuffIndex(mod.BuffType("Jump")) != -1)
            {
                damage += (int)(player.velocity.Y * item.damage/4);
            }
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("SapphireTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(mod.ItemType("DragoonLance"), 1);
            recipe2.AddIngredient(mod.ItemType("SapphireTicket"), 2);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this);
            recipe2.AddRecipe();
        }*/
    }
}
