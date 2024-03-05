using System;
using UnityEngine;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SchemaTemplateAttribute : Attribute
    {
        public SchemaTemplateAttribute(string templatePath)
        {
            TemplatePath = templatePath;
        }

        public string TemplatePath {get; private set;}
    }
}