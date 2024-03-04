using System;
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