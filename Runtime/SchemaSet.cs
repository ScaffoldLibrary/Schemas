using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scaffold.Schemas
{
    [Serializable]
    public class SchemaSet
    {
        [SerializeReference, SerializeField] public List<Schema> Collection = new List<Schema>();

        public List<Type> Types
        {
            get
            {
                if (types == null)
                {
                    types = Collection.Select(s => s.GetType()).ToList();
                }
                return types;
            }
        }

        private List<Type> types;

        public bool AddSchema(Type schema)
        {
            if (!typeof(Schema).IsAssignableFrom(schema))
            {
                Debug.Log($"schema object you are trying to add does not inherint from SCHEMA");
                return false;
            }
            if (Types.Contains(schema))
            {
                Debug.Log($"object already contains schema of type {schema}");
                return false;
            }
            Types.Add(schema);
            Collection.Add((Schema)Activator.CreateInstance(schema));
            return true;
        }

        public bool RemoveSchema(Type schema)
        {
            int removedCount = Collection.RemoveAll(s => s.GetType() == schema);
            Types.Remove(schema);
            return removedCount > 0;
        }

        public bool TryGetSchema<TSchema>(out TSchema schema) where TSchema : Schema
        {
            bool hasSchema = TryGetSchema(typeof(TSchema), out Schema baseSchema);
            schema = (TSchema)baseSchema;
            return hasSchema;
        }

        public bool TryGetSchema(Type schemaType, out Schema schema)
        {
            if (!Types.Contains(schemaType))
            {
                schema = default;
                return false;
            }

            schema = Collection.Find(t => t.GetType() == schemaType);
            return true;
        }

        public bool Contains(Type schema)
        {
            return Types.Contains(schema);
        }
    }
}