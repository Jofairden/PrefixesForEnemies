using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace EnemyMods.Items.Tier4
{
    public class LightningRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 90;
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
            item.autoReuse = false;
            item.shoot = 580;
            item.shootSpeed = 7f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Lightning Ring");
      Tooltip.SetDefault("Summon a bolt of lightning. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[7] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
            Vector2 vector82 =  -Main.player[Main.myPlayer].Center + Main.MouseWorld;
            float ai = Main.rand.Next(100);
            Vector2 vector83 = Vector2.Normalize(vector82) * item.shootSpeed;
            Projectile.NewProjectile(player.Center.X, player.Center.Y, vector83.X, vector83.Y, type, damage, .5f, player.whoAmI, vector82.ToRotation(), ai);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[7]--;
            if (play.cooldowns[7] == -1)
            {
                play.cooldowns[7] = play.maxCooldowns[7];
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("ChoiceToken"), 1);
            recipe.AddIngredient(mod.ItemType("EmeraldTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
