using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scaffold.Schemas
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AllowDuplicateSchemasAttribute : Attribute
    {
        public AllowDuplicateSchemasAttribute(bool allowMultiple)
        {
            AllowMultiple = allowMultiple;
        }

        public bool AllowMultiple {get; private set;}
    }
}