using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Dusts
{
    public class VoidDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale = 1.4f;
        }
        public override bool Update(Dust dust)
        {
            dust.scale -= .02f;
            if (dust.scale <= .3f)
            {
                dust.active = false;
            }
            return false;
        }
        /*
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(255, 255, 255, 50);
        }
        */
    }
}