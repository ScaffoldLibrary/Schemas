using System;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SchemaDescriptionAttribute : Attribute
    {
        public SchemaDescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}

