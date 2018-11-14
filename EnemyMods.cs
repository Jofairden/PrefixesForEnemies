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
            PrefixNPC prefixNPC = GetGlobalNPC<PrefixNPC>();
            prefixNPC.readyForChecks = true;

            int prefixCount = reader.ReadInt32();
            NPCPrefix[] prefixes = new NPCPrefix[prefixCount];
            for (int i = 0; i < prefixCount; i++)
            {
                string group = reader.ReadString();
                string prefixName = reader.ReadString();
                try
                {
                    uint groupID = PrefixVault.GetPrefixGroups().Find(pg => pg.Name == group).ID;
                    NPCPrefix prefix = PrefixVault.GetPrefixes(groupID).Find(pf => pf.Name == prefixName);
                    prefix.ReadValues(reader);
                    prefixes[i] = prefix;
                }
                catch
                {
                    Main.NewText("Failed to get prefix");
                    ErrorLogger.Log("Error getting prefixes from server");
                }
                //TODO READ!
            }
            prefixNPC.NewPrefixes = prefixes;
            prefixNPC.OverrideName(npc);
            foreach (NPCPrefix prefix in prefixNPC.NewPrefixes)
            {
                prefix.OnCreate(npc);
            }
            //base.HandlePacket(reader, whoAmI);
        }
    }
}
