using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Scaffold.Schemas
{
    public static class SchemaCacheUtility
    {
        private static Dictionary<Type, string> CachedTypeNames = new Dictionary<Type, string>();
        private static Dictionary<Type, string> CachedTypePaths = new Dictionary<Type, string>();

        public static string GetTypeDisplayName(Type type)
        {
            if (!CachedTypeNames.TryGetValue(type, out string name))
            {
                name = type switch
                {
                    { IsGenericType: true } => $"{type.Name.Split('`')[0]}<{string.Join(", ", type.GetGenericArguments().Select(GetTypeDisplayName))}>",
                    _ => type.Name
                };
                CachedTypeNames[type] = name;
            }
            return name;
        }

        public static string GetTypeGroupPath(Type type)
        {
            if (!CachedTypePaths.TryGetValue(type, out string name))
            {
                name = GetTypeDisplayName(type);
                var menuGroupAttribute = type.GetCustomAttribute<SchemaMenuGroupAttribute>();
                if (menuGroupAttribute != null)
                {
                    name = $"{menuGroupAttribute.Path}/{name}";
                }
                CachedTypePaths[type] = name;
            }
            return name;
        }

        private static Dictionary<Type, List<Type>> CachedDerivedTypes = new Dictionary<Type, List<Type>>();

        public static List<Type> GetDerivedTypes(Type type)
        {

            if (!CachedDerivedTypes.TryGetValue(type, out List<Type> list))
            {
                var domain = AppDomain.CurrentDomain;
                list = domain
                  .GetAssemblies()
                  .SelectMany(x => x.GetTypes())
                  .Where(t => !t.IsAbstract && type.IsAssignableFrom(t))
                  .ToList();

                if (type.IsAbstract)
                {
                    list.Remove(type);
                }

                CachedDerivedTypes[type] = list;
            }
            return list;
        }

    }
}
