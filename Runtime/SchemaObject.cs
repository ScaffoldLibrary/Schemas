using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scaffold.Schemas
{
    public abstract class SchemaObject : ScriptableObject
    {
        public IReadOnlyList<Schema> Schemas => schemas.Collection;
        [SerializeReference] private SchemaSet schemas = new SchemaSet();

        public bool TryGetSchema<T>(out T schema) where T : Schema
        {
            bool contains = schemas.TryGetSchema(typeof(T), out Schema rawSchema);
            schema = (T)rawSchema;
            return contains;
        }

        public List<T> GetSchemas<T>() where T: Schema
        {
            return schemas.GetSchemas(typeof(T)).Cast<T>().ToList();
        }

        public T GetSchema<T>() where T : Schema
        {
            schemas.TryGetSchema(typeof(T), out Schema schema);
            return schema as T;
        }

        public bool AddSchema<T>() where T : Schema
        {
            return AddSchema(typeof(T));
        }

        public bool AddSchema(Type type)
        {
            return schemas.AddSchema(type);
        }

        public bool RemoveSchema<T>() where T : Schema
        {
            return RemoveSchema(typeof(T));
        }

        public bool RemoveSchema(Type type)
        {
            Schema schema = Schemas.FirstOrDefault(s => s.GetType().IsAssignableFrom(type));
            return schemas.RemoveSchema(schema);
        }

        public bool RemoveSchema(Schema schema)
        {
            return schemas.RemoveSchema(schema);
        }

        public bool HasSchema<T>() where T : Schema
        {
            return HasSchema(typeof(T));
        }

        public bool HasSchema(Type type)
        {
            return schemas.Contains(type);
        }
    }
}
