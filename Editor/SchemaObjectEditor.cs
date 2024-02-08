using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Scaffold.Schemas.Editor
{
    [CustomEditor(typeof(SchemaObject), true)]
    public class SchemaObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty collectionProp;
        private SchemaSet set;
        private List<Type> schemaOptions;

        protected virtual string[] PropertiesToIgnore => new string[]
        {
            "m_Script",
            "schemas"
        };

        private void OnEnable()
        {
            collectionProp = serializedObject.FindProperty("schemas.Collection");
            set = serializedObject.FindProperty("schemas").boxedValue as SchemaSet;
            schemaOptions = SchemaCacheUtility.GetDerivedTypes(typeof(Schema));
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultProperties();
            EditorGUILayout.Space(5);
            DrawSchemas();
            EditorGUILayout.Space(5);
            DrawControls();
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        private void DrawSchemas()
        {
            if(collectionProp.arraySize == 0)
            {
                SchemaLayout.Divider(0, 0);
                return;
            }

            for (int i = 0; i < collectionProp.arraySize; i++)
            {
                SerializedProperty prop = collectionProp.GetArrayElementAtIndex(i);
                if (prop == null || prop.boxedValue == null)
                {
                    continue;
                }
                SchemaDrawer drawer = SchemaDrawerContainer.instance.GetDrawer(prop, this);
                if (drawer.Expired)
                {
                    continue;
                }
                drawer.Draw();
                if (i == collectionProp.arraySize - 1 && prop.isExpanded)
                {
                    SchemaLayout.Divider(0, 0);
                }
            }
        }

        protected void DrawDefaultProperties()
        {
            DrawPropertiesExcluding(serializedObject, PropertiesToIgnore);
        }


        private void DrawControls()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (EditorGUILayout.DropdownButton(new GUIContent("Add Schema", "Add new schema to this object."), FocusType.Keyboard, SchemaStyles.CenterButton))
            {
                ShowSchemaMenu();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ShowSchemaMenu()
        {
            var menu = new GenericMenu();
            for (int i = 0; i < schemaOptions.Count; i++)
            {
                var type = schemaOptions[i];
                var menuOption = new GUIContent(SchemaCacheUtility.GetTypeGroupPath(type), "");
                if (!set.Contains(type))
                {
                    menu.AddItem(menuOption, false, () => AddSchema(type));
                }
                else
                {
                    menu.AddDisabledItem(menuOption, true);
                }
            }
            menu.ShowAsContext();
        }

        public void AddSchema(Type schema)
        {
            Undo.RecordObject(target, "adding schema to object");
            set.AddSchema(schema);
            Refresh();
        }

        public void RemoveSchema(object schema)
        {
            Undo.RecordObject(target, "removing schema from object");
            set.RemoveSchema(schema.GetType());
            Refresh();
        }

        public void Refresh()
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
            serializedObject.Update();
        }
    }
}
