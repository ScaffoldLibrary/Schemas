using System;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SchemaCustomDrawerAttribute : Attribute
    {
        public SchemaCustomDrawerAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
