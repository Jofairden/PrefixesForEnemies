using EnemyMods.Prefixes.Groups;
using Terraria;
using Terraria.ID;

namespace EnemyMods.Prefixes.Stats
{
    public class CutpursePrefix : NPCPrefix
    {
        public override string Type => SpecialGroup.NAME;

        public override string Name => "Cutpurse";

        public override float Rarity => 0.7f;

        public override void OnCreate(NPC npc)
        {
            npc.value *= 1.5f;
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            for (int i = 0; i < 59; i++)
            {
                if (target.inventory[i].type >= 71 && target.inventory[i].type <= 74)
                {
                    int num2 = Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, target.inventory[i].type, 1, false, 0, false, false);
                    int num3 = (int)(target.inventory[i].stack * .9);
                    num3 = target.inventory[i].stack - num3;
                    target.inventory[i].stack -= num3;
                    if (target.inventory[i].stack <= 0)
                    {
                        target.inventory[i] = new Item();
                    }
                    Main.item[num2].stack = num3;
                    Main.item[num2].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
                    Main.item[num2].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
                    Main.item[num2].noGrabDelay = 100;
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
                    }
                    if (i == 58)
                    {
                        Main.mouseItem = target.inventory[i].Clone();
                    }
                }
            }
        }
    }
}
