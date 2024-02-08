using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scaffold.Schemas.Editor
{
    public class SchemaDrawerContainer : ScriptableSingleton<SchemaDrawerContainer>
    {

        private Dictionary<object, SchemaDrawer> drawers = new Dictionary<object, SchemaDrawer>();

        public SchemaDrawer GetDrawer(SerializedProperty prop, SchemaObjectEditor editor)
        {
            if (!drawers.TryGetValue(prop.boxedValue, out SchemaDrawer drawer))
            {
                drawer = SchemaDrawerFactory.CreateDrawer(prop, editor);
                drawers.Add(prop.boxedValue, drawer);
            }
            drawer.UpdateSerializedProperty(prop);
            return drawer;
        }

        public void ReleaseDrawer(SchemaDrawer drawer)
        {
            drawers.Remove(drawer.Property.boxedValue);
        }
    }
}
