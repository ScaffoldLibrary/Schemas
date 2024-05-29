using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SchemaFilterAttribute : Attribute
    {
        public SchemaFilterAttribute(Type baseSchemaType)
        {
            BaseSchemaType = baseSchemaType;
        }

        public Type BaseSchemaType { get; private set; }
    }
}
