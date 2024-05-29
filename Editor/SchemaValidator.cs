using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Scaffold.Schemas.Editor
{
    public class SchemaValidator
    {
        public SchemaValidator(SchemaObject schemaObject, SerializedObject serializedObject, SchemaObjectEditor editor)
        {
            SchemaObject = schemaObject;
            SerializedObject = serializedObject;
            Editor = editor;
        }

        public SchemaObject SchemaObject { get; private set; }

        public SerializedObject SerializedObject { get; private set; }

        public SchemaObjectEditor Editor { get; private set; }

        public Dictionary<Type, bool> duplicateLookup { get; private set; }

        public void Validate()
        {
            CreateDuplicateLookup();
            CheckRequiredSchemas();
        }

        private void CheckRequiredSchemas()
        {
            IEnumerable<RequireSchemaAttribute> attributes = SchemaObject.GetType().GetCustomAttributes<RequireSchemaAttribute>(true);
            if (attributes != null && attributes.Count() > 0)
            {
                IEnumerable<Type> requiredTypes = attributes.SelectMany(a => a.SchemaTypes).Distinct();
                SchemaSet set = SerializedObject.FindProperty("schemas").boxedValue as SchemaSet;
                foreach (var required in requiredTypes)
                {
                    if (!set.Contains(required))
                    {
                        Editor.AddSchema(required);
                    }
                }
            }
        }

        public List<Type> GetSchemaOptions()
        {
            SchemaFilterAttribute schemaFilter = SchemaObject.GetType().GetCustomAttribute<SchemaFilterAttribute>(true);
            Type baseType = schemaFilter != null ? schemaFilter.BaseSchemaType : typeof(Schema);
            return SchemaCacheUtility.GetDerivedTypes(baseType);
        }

        public bool CanAddType(Type type)
        {
            bool hasDuplicate = SchemaObject.HasSchema(type);
            if (duplicateLookup.TryGetValue(type, out bool allow))
            {
                return allow || !hasDuplicate;
            }

            if (duplicateLookup.TryGetValue(SchemaObject.GetType(), out allow))
            {
                return allow || !hasDuplicate;
            }

            return true;
        }

        private void CreateDuplicateLookup()
        {
            duplicateLookup = new Dictionary<Type, bool>();

            var duplicateTypes = TypeCache.GetTypesWithAttribute<AllowDuplicateSchemasAttribute>();
            foreach (var type in duplicateTypes)
            {
                var attribute = type.GetCustomAttributes<AllowDuplicateSchemasAttribute>(true).FirstOrDefault();
                duplicateLookup.Add(type, attribute.AllowMultiple);
            }
        }
    }
}
