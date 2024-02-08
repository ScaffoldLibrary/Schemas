using System;

namespace Scaffold.Schemas
{
    public class SchemaCustomDrawerAttribute : Attribute
    {
        public SchemaCustomDrawerAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
