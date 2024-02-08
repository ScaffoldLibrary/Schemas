using System;

namespace Scaffold.Schemas
{
    public class SchemaDescriptionAttribute : Attribute
    {
        public SchemaDescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}

