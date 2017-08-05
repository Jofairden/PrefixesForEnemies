using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Sounds
{
    public class Thunder : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            if (soundInstance.State == SoundState.Playing)
                return null;
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            soundInstance.Pitch = Main.rand.Next(-6, 7) /30f;
            Main.PlaySoundInstance(soundInstance);
            return soundInstance;
        }
    }
}
