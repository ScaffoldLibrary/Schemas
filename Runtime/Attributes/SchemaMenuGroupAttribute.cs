using System;

namespace Scaffold.Schemas
{
    public class SchemaMenuGroupAttribute : Attribute
    {
        public SchemaMenuGroupAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}
