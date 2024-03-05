using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireSchemaAttribute : Attribute
    {
        public RequireSchemaAttribute(params Type[] schemaTypes)
        {
            SchemaTypes = schemaTypes;
        }

        public Type[] SchemaTypes = new Type[0];
    }
}
