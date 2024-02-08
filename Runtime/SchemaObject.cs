using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Schemas
{

    public abstract class SchemaObject : ScriptableObject
    {
        public IReadOnlyList<Schema> Schemas => schemas.Collection;
        [SerializeReference] public SchemaSet schemas = new SchemaSet();

        public bool TryGetSchema<T>(out T schema) where T : Schema
        {
            bool contains = schemas.TryGetSchema(typeof(T), out Schema rawSchema);
            schema = (T)rawSchema;
            return contains;
        }

        public T GetSchema<T>() where T : Schema
        {
            schemas.TryGetSchema(typeof(T), out Schema schema);
            return schema as T;
        }

        public bool AddSchema<T>() where T : Schema
        {
            return schemas.AddSchema(typeof(T));
        }

        public bool RemoveSchema<T>() where T : Schema
        {
            return schemas.RemoveSchema(typeof(T));
        }

        public bool HasSchema<T>() where T : Schema
        {
            return schemas.Contains(typeof(T));
        }
    }
}
