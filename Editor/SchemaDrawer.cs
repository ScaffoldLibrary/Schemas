using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Scaffold.Schemas.Editor
{
    [SchemaCustomDrawer(typeof(Schema))]
    public class SchemaDrawer
    {
        public SchemaDrawer(SerializedProperty property, SchemaObjectEditor editor)
        {
            this.Editor = editor;
            this.Property = property;
            this.IsExpanded = property.isExpanded;
            this.SchemaName = SchemaCacheUtility.GetTypeDisplayName(property.boxedValue.GetType());
            CheckAttributes();
        }

        public SerializedProperty Property { get; set; }
        public string SchemaName { get; protected set; }
        public string SchemaDescription { get; protected set; }
        public bool Expired { get; protected set; }

        protected SchemaObjectEditor Editor { get; set; }
        protected bool IsExpanded { get; set; }


        private void CheckAttributes()
        {
            Type schemaType = Property.boxedValue.GetType();
            SchemaDescriptionAttribute description = schemaType.GetCustomAttribute<SchemaDescriptionAttribute>(true);
            if (description != null)
            {
                SchemaDescription = description.Description;
            }
        }

        public virtual void UpdateSerializedProperty(SerializedProperty property)
        {
            Property = property;
        }

        public virtual void Draw()
        {
            SchemaLayout.Divider(0, 0);
            DrawHeader();
            SchemaLayout.Divider(0, 0);
            if (IsExpanded)
            {
                DrawBody();
            }
        }

        public virtual void DrawHeader()
        {
            SchemaLayout.Header(this, ToggleExpanded, DeleteSchema);
        }

        private void ToggleExpanded()
        {
            IsExpanded = !IsExpanded;
            Property.isExpanded = IsExpanded;
        }

        private void DeleteSchema()
        {
            SchemaDrawerContainer.instance.ReleaseDrawer(this);
            Editor.RemoveSchema(Property.boxedValue as Schema);
            IsExpanded = false;
            Expired = true;
        }


        public virtual void DrawBody()
        {
            var childProps = GetChildProperties(Property);
            foreach (var child in childProps)
            {
                EditorGUILayout.PropertyField(child, true);
            }
            EditorGUILayout.Space(3);
        }

        private IEnumerable<SerializedProperty> GetChildProperties(SerializedProperty parent, int depth = 1)
        {
            var cpy = parent.Copy();
            var depthOfParent = cpy.depth;
            var enumerator = cpy.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is not SerializedProperty childProperty) continue;
                if (childProperty.depth > depthOfParent + depth) continue;

                yield return childProperty.Copy();
            }
        }
    }
}
