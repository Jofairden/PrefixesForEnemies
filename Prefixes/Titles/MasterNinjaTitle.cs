using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class MasterNinjaTitle : NPCPrefix
    {
        public override string Type => TitleGroup.NAME;

        public override string Name => "the Master Ninja";

        public override float Rarity => 1.0f;

        public override bool IsPrefix() => false;

        public override bool IsAllowed(NPC npc) => !npc.dontTakeDamage;

        public int TriggerTimer { get; private set; }
        public int TriggerCounter { get; private set; }

        public override void OnCreate(NPC npc)
        {
            npc.value *= 5f;
            npc.damage = (int)(npc.damage * 1.5);
        }
        public override void NPCLoot(NPC npc)
        {
            //TODO: should we spawn stuff?
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChoiceToken"));
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (TriggerCounter == -1)
            {
                npc.alpha = 250;
                npc.dontTakeDamage = true;
                int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, 75, npc.damage, 0);
                Main.projectile[p].Kill();
            }
            else
            {
                npc.alpha = 0;
                TriggerCounter++;
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (TriggerCounter == -1)
            {
                npc.alpha = 250;
                npc.dontTakeDamage = true;
                int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, 75, (int)(npc.damage * .7), 0);
                Main.projectile[p].Kill();
            }
            else
            {
                npc.alpha = 0;
                TriggerCounter++;
            }
        }

        public override void AI(NPC npc)
        {
            if (TriggerCounter > 0)
            {
                TriggerTimer += TriggerCounter;
                if (TriggerTimer >= 360)
                {
                    TriggerCounter = -1;
                    TriggerTimer = 120;
                }
            }
            else if (TriggerCounter == -1)
            {
                TriggerTimer--;
                if (TriggerTimer <= 0)
                {
                    TriggerCounter = 0;
                    npc.dontTakeDamage = false;
                }
            }
            else
            {
                npc.alpha += 10;
                if (npc.alpha > 250)
                {
                    npc.alpha = 250;
                }
            }
        }
    }
}
