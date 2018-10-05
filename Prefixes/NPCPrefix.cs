using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods.Prefixes
{
    public abstract class NPCPrefix : ICloneable
    {
        public abstract String Type { get; }

        public abstract String Name { get; }

        public abstract float Rarity { get; }

        public virtual bool IsPrefix() => true;

        /// <summary>
        /// Indicates if the prefix is allowed at this time
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public virtual bool IsAllowed(NPC npc) => true;

        public virtual void OnCreate(NPC npc) { }

        //TODO: RARE requires draw effect!

        public virtual void OnHitPlayer(NPC npc, Player target, int damage, bool crit) { }

        public virtual void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit) { }

        public virtual bool CheckDead(NPC npc) => true;

        public virtual void UpdateLifeRegen(NPC npc, ref int damage) { }

        //TODO: something something PRENPCLOOT

        public virtual void NPCLoot(NPC npc) { }

        public virtual void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit) { }

        public virtual void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit) { }

        public virtual bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit) { return true; }

        public virtual void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit) { }

        public virtual void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) { }

        public virtual void AI(NPC npc) { }

        public virtual void DrawEffects(NPC npc, ref Color drawColor) { }

        public virtual int GetShaderID(NPC nPC) => 0;

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public virtual void Sync(ModPacket modPacket)
        {
            modPacket.Write(this.Type);
            modPacket.Write(this.Name);
        }
    }
}
