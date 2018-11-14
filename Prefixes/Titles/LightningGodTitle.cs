using EnemyMods.Prefixes.Groups;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnemyMods.Prefixes.Stats
{
    public class LightningGodTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Lightning God";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public int TriggerTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }

        public override void AI(NPC npc)
        {
            if (npc.target == 255 || Main.netMode == 2)
            {
                return;
            }
            Player target = Main.player[npc.target];

            int distance = (int)Math.Sqrt((npc.Center.X - target.Center.X) * (npc.Center.X - target.Center.X) + (npc.Center.Y - target.Center.Y) * (npc.Center.Y - target.Center.Y));

            if (distance < 800)
            {
                if (Main.rand.Next(0, 3) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
                }
                TriggerTimer = Math.Min(TriggerTimer + 1, 90);
                if (Main.rand.Next(0, 80) == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, npc.position, EnemyMods.Instance.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
                    Vector2 randomVector = new Vector2(Main.rand.Next(-50, 51), 800);

                    Vector2 velocity = Vector2.Normalize(randomVector.RotatedByRandom(0.78539818525314331)) * 8;

                    float ai = Main.rand.Next(100);
                    Projectile.NewProjectile(target.position.X + Main.rand.Next(-100, 101), target.position.Y - 900, velocity.X, velocity.Y, 466, (int)(npc.damage * ProjectilesGroup.DamageMultiplier), 0f, target.whoAmI, randomVector.ToRotation(), ai);
                }
                if (TriggerTimer >= 60 && Collision.CanHitLine(target.position, target.width, target.height, npc.position, npc.width, npc.height))
                {
                    Main.PlaySound(SoundLoader.customSoundType, npc.position, EnemyMods.Instance.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));

                    Vector2 position = target.position - npc.position;
                    float ai = Main.rand.Next(100);
                    Vector2 velocity = Vector2.Normalize(position.RotatedByRandom(0.78539818525314331)) * 8;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, velocity.X, velocity.Y, 580, (int)(npc.damage * ProjectilesGroup.DamageMultiplier), 0f, target.whoAmI, position.ToRotation(), ai);
                    TriggerTimer = 0;
                }
            }
        }
    }
}
