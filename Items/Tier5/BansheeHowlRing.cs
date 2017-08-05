using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace EnemyMods.Items.Tier5
{
    public class BansheeHowlRing : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 222;
            item.magic = true;
            item.width = 10;
            item.height = 10;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 80000;
            item.rare = 8;
            item.autoReuse = false;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Banshee Howl Ring");
      Tooltip.SetDefault("The weak perish. One charge.");
    }

        public override bool CanUseItem(Player player)
        {
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            if (play.charges[11] <= 0)
            {
                return false;
            }
            else return true;
        }
        public override bool UseItem(Player player)
        {
            Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Scream"));
            for (int i = 0; i < 100; i++)
            {
                NPC npc = Main.npc[i];
                float distance = (float)Math.Sqrt((npc.Center.X - player.Center.X) * (npc.Center.X - player.Center.X) + (npc.Center.Y - player.Center.Y) * (npc.Center.Y - player.Center.Y));
                if (distance < 800 && !npc.townNPC)
                {
                    if (npc.life <= item.damage*player.magicDamage && !npc.dontTakeDamage && npc.type != NPCID.DD2LanePortal)
                    {
                        npc.StrikeNPC(9999, 0f, 0, false, false);
                        NetMessage.SendData(28, -1, -1, null, npc.whoAmI, 9999, 0, 0, 0);
                    }
                }
            }
            MPlayer play = (MPlayer)player.GetModPlayer(mod, "MPlayer");
            play.charges[11]--;
            if (play.cooldowns[11] == -1)
            {
                play.cooldowns[11] = play.maxCooldowns[11];
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("DeathKnellRing"), 1);
            recipe.AddIngredient(mod.ItemType("RubyTicket"), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
