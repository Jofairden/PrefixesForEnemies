using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyMods.Items.Tier6
{
    public class VortexLightningRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 265;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 100000;
            item.rare = 10;
            item.autoReuse = false;
            item.shoot = 580;
            item.shootSpeed = 7f;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Vortex Lightning Ring");
      Tooltip.SetDefault("Summon a forking bolt of lightning. Two charges.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[18] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
            Vector2 vector82 = -Main.player[Main.myPlayer].Center + Main.MouseWorld;
            float ai = Main.rand.Next(100);
            Vector2 vector83 = Vector2.Normalize(vector82) * item.shootSpeed;
            Projectile.NewProjectile(player.Center.X, player.Center.Y, vector83.X, vector83.Y, type, damage, .49f, player.whoAmI, vector82.ToRotation(), ai);
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[18]--;
            if (play.cooldowns[18] == -1)
            {
                play.cooldowns[18] = play.maxCooldowns[18];
            }
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LightningRing"), 1);
            recipe.AddIngredient(mod.ItemType("AmberTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
