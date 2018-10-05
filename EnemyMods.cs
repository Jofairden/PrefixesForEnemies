using EnemyMods.NPCs;
using EnemyMods.Prefixes;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace EnemyMods
{
    public class EnemyMods : Mod
    {
        internal static EnemyMods Instance;

        public EnemyMods()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load()
        {
            Instance = this;

            PrefixVault.Initialize();

            PrefixVault.RegisterMod(this);
            PrefixVault.SetupMod(this);
        }

        public override void Unload()
        {
            PrefixVault.Unload();
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            if (!reader.ReadString().Contains("Prefixes"))
            {
                return;
            }
            //packet should contain npc.whoAmI and all prefixes/suffixes; this is where prefixes will be applied. Need to handle basic stats here too.
            int npcIndex = reader.ReadInt32();
            NPC npc = Main.npc[npcIndex];
            gNPC info = GetGlobalNPC<gNPC>();
            info.readyForChecks = true;
            string prefixes = "";
            string suffixes = "";
            try
            {
                bool hasPrefix = reader.ReadBoolean();
                if (hasPrefix)
                {
                    prefixes = reader.ReadString();
                }
                bool hasSuffix = reader.ReadBoolean();
                if (hasSuffix)
                {
                    suffixes = reader.ReadString();
                }
            }
            catch
            {
                Main.NewText("Failed to get prefix");
                ErrorLogger.Log("Error getting prefixes from server");
            }
            info.prefixes = prefixes;
            info.suffixes = suffixes;
            string addToName = prefixes.Replace("#", "");
            npc.GivenName = addToName + npc.GivenName + suffixes;
            //handle basic stats here
            if (prefixes.Contains("Rare"))
            {
                npc.lifeMax *= 2;
                npc.life = npc.lifeMax;
            }
            if (prefixes.Contains("Actually-A-Penguin"))
            {
                npc.catchItem = 2205;
            }
            if (prefixes.Contains("Actually-A-Bird"))
            {
                npc.catchItem = 2015;
            }
            if (prefixes.Contains("Tough"))
            {
                npc.lifeMax = (int)(npc.lifeMax * 1.5);
                npc.life = npc.lifeMax;
            }
            if (prefixes.Contains("Dangerous"))
            {
                npc.damage = (int)(npc.damage * 1.4);
            }
            if (prefixes.Contains("Armored"))
            {
                npc.defense = (int)(npc.defense * 1.2 + 4);
            }
            if (prefixes.Contains("Small"))
            {
                npc.scale *= .7f;
            }
            if (prefixes.Contains("Big"))
            {
                npc.scale *= 1.4f;
            }
            if (prefixes.Contains("Persistent"))
            {
                npc.knockBackResist /= 1.5f;
            }
            if (prefixes.Contains("Enduring"))
            {
                npc.takenDamageMultiplier *= .8f;
            }
            if (prefixes.Contains("Colossal"))
            {
                npc.scale *= 1.8f;
                npc.damage = (int)(npc.damage * 1.2);
            }
            if (prefixes.Contains("Miniature"))
            {
                npc.scale *= .45f;
                npc.damage = (int)(npc.damage * .8);
            }
            if (prefixes.Contains("Generous"))
            {
                npc.value *= 2;
            }
            if (prefixes.Contains("Philanthropic"))
            {
                npc.value *= 3;
            }
            if (suffixes.Contains("the Juggernaut"))
            {
                npc.defense *= 2;
                npc.lifeMax *= 3;
                npc.life = npc.lifeMax;
                npc.knockBackResist = 0;
                npc.scale *= 1.3f;
            }
            if (suffixes.Contains("the Reaper"))
            {
                npc.defense /= 2;
                npc.lifeMax = (int)(npc.lifeMax * .666);
            }
            if (suffixes.Contains("the Immortal"))
            {
                info.lives = 8;
            }
            if (suffixes.Contains("the Nullifier"))
            {
                for (int i = 0; i < npc.buffImmune.Length; i++)
                {
                    npc.buffImmune[i] = true;
                }
            }
            if(suffixes.Contains("the Master Ninja"))
            {
                npc.damage = (int)(npc.damage * 1.5);
            }
            string[] allPfix = prefixes.Split('#');
            npc.value = allPfix.Length * 1.5f;
            if (suffixes.Length > 0)
            {
                npc.value *= 5;
            }
            //base.HandlePacket(reader, whoAmI);
        }
    }
}
