using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Scaffold.Schemas.Editor
{
    public static class SchemaDrawerFactory
    {
        private static Dictionary<Type, Type> DrawerLookUp = new Dictionary<Type, Type>();

        public static SchemaDrawer CreateDrawer(SerializedProperty property, SchemaObjectEditor editor)
        {
            if (DrawerLookUp.Count <= 0)
            {
                FillDrawerList();
            }

            Type schemaType = property.boxedValue.GetType();
            Type drawerType = GetDrawerType(schemaType);
            return (SchemaDrawer)Activator.CreateInstance(drawerType, new object[] { property, editor });
        }

        private static Type GetDrawerType(Type targetType)
        {
            Type drawerType;
            while (!DrawerLookUp.TryGetValue(targetType, out drawerType))
            {
                targetType = targetType.BaseType;
            }
            return drawerType;
        }

        private static void FillDrawerList()
        {
            var types = SchemaCacheUtility.GetDerivedTypes(typeof(SchemaDrawer));
            foreach (var type in types)
            {
                var drawerAttribute = type.GetCustomAttribute<SchemaCustomDrawerAttribute>();
                if (drawerAttribute != null)
                {
                    DrawerLookUp[drawerAttribute.Type] = type;
                }
            }
        }
    }
}