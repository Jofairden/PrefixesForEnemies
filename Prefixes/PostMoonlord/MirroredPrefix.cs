using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace EnemyMods.Prefixes.PostMoonlord
{
    public class MirroredPrefix : NPCPrefix
    {
        public override string Type => PostMoonlordGroup.NAME;

        public override string Name => "Mirrored";

        public override float Rarity => 1.0f;

        public int TriggerTimer { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            npc.reflectingProjectiles = true;
            TriggerTimer = 20;
        }

        public override void AI(NPC npc)
        {
            if (Main.rand.Next(0, 10) == 0)
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.SilverCoin);
            }
            if (npc.reflectingProjectiles)
            {
                if (Main.rand.Next(0, 4) == 0)
                {
                    int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.SilverCoin);
                }
                TriggerTimer--;
                if (TriggerTimer <= 0)
                {
                    npc.reflectingProjectiles = false;
                }
            }
        }

        public override int GetShaderID(NPC nPC) => GameShaders.Armor.GetShaderIdFromItemId(ItemID.ReflectiveSilverDye);
    }
}
