using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RequiredSchemaAttribute : Attribute
{
    public RequiredSchemaAttribute(params Type[] schemaTypes)
    {
        SchemaTypes = schemaTypes;
    }

    public Type[] SchemaTypes = new Type[0]; 
}
