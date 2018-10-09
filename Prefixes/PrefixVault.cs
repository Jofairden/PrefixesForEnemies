using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace EnemyMods.Prefixes
{
    public static class PrefixVault
    {

        private static uint prefixGroupNextID;

        private static IDictionary<String, Assembly> mods;
        private static IDictionary<uint, List<NPCPrefix>> prefixes;
        private static List<PrefixGroup> prefixGroups;


        internal static void Initialize()
        {
            prefixGroupNextID = 0;
            mods = new ConcurrentDictionary<string, Assembly>();
            prefixGroups = new List<PrefixGroup>();
            prefixes = new Dictionary<uint, List<NPCPrefix>>();
        }

        internal static void Unload()
        {
            prefixes = null;
            prefixGroups = null;
            mods = null;
            prefixGroupNextID = 0;
        }

        /// <summary>
        /// Registers specified mod, enabling autoloading for that mod
        /// </summary>
        public static void RegisterMod(Mod mod)
        {
            bool? b = mod.GetType().GetField("loading", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(mod) as bool?;
            if (b != null && !b.Value)
            {
                throw new Exception("RegisterMod can only be called from Mod.Load or Mod.Autoload");
            }

            if (mods.ContainsKey(mod.Name))
            {
                throw new Exception($"Mod {mod.Name} is already registered");
            }

            Assembly code;
#if DEBUG
            code = Assembly.GetAssembly(mod.GetType());
#else
			code = mod.Code;
#endif

            mods.Add(new KeyValuePair<string, Assembly>(mod.Name, code));
        }

        public static void SetupMod(Mod mod)
        {

            bool? b = mod.GetType().GetField("loading", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(mod) as bool?;
            if (b != null && !b.Value)
            {
                throw new Exception("SetupContent for EMMLoader can only be called from Mod.Load or Mod.Autoload");
            }

            if (!mods.ContainsKey(mod.Name))
            {
                throw new Exception($"Mod {mod.Name} is not yet registered in EMMLoader");
            }

            var ordered = mods.FirstOrDefault(x => x.Key.Equals(mod.Name))
                .Value
                .GetTypes()
                .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList(); /* || type.GetConstructor(new Type[0]) == null*/

            foreach (Type type in ordered.Where(x => x.IsSubclassOf(typeof(PrefixGroup))))
            {
                PrefixGroup prefixGroup = (PrefixGroup)Activator.CreateInstance(type);
                prefixGroup.ID = ReservePrefixGroupID();
                AddPrefixGroup(prefixGroup);
                prefixes.Add(prefixGroup.ID, new List<NPCPrefix>());
            }
            foreach (Type type in ordered.Where(x => x.IsSubclassOf(typeof(NPCPrefix))))
            {
                NPCPrefix prefix = (NPCPrefix)Activator.CreateInstance(type);
                AddPrefix(prefix);
            }
        }

        private static void AddPrefixGroup(PrefixGroup prefixGroup)
        {
            prefixGroups.Add(prefixGroup);
        }

        private static void AddPrefix(NPCPrefix prefix)
        {

            PrefixGroup prefixGroup = prefixGroups.Find(pg => pg.Name.Equals(prefix.Type));
            if (prefixGroup != null)
            {
                prefixes[prefixGroup.ID].Add(prefix);
            }
        }

        internal static List<NPCPrefix> GetPrefixes(uint groupID)
        {
            List<NPCPrefix> prefixList = null;
            if (prefixes.TryGetValue(groupID, out prefixList))
            {
                return prefixList;
            }
            return null;
        }

        internal static List<PrefixGroup> GetPrefixGroups()
        {
            return prefixGroups;
        }


        private static uint ReservePrefixGroupID()
        {
            uint reserved = prefixGroupNextID;
            prefixGroupNextID++;
            return reserved;
        }
    }
}
