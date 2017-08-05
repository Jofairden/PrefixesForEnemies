using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Buffs
{
    public class VoidTarget : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Void Hunt");
            Description.SetDefault("An agent of the void has spotted you!");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            MPlayer pinf = ((MPlayer)player.GetModPlayer(mod, "MPlayer"));
            if(Main.rand.Next(0, 100) == 0)
            {
                Projectile.NewProjectile(new Vector2(Main.rand.Next(-120, 121) + player.Center.X + player.velocity.X * 15, Main.rand.Next(-120, 121) + player.Center.Y + player.velocity.Y * 15), Vector2.Zero, mod.ProjectileType("VoidSpawner"), pinf.voidTargetDamage, 0, 255, player.whoAmI);
            }
        }
    }
}
