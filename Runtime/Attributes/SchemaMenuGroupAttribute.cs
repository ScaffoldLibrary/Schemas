using System;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SchemaMenuGroupAttribute : Attribute
    {
        public SchemaMenuGroupAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }
    }
}
