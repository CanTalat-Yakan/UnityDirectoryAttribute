#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    [CustomPropertyDrawer(typeof(DirectoryAttribute))]
    public class DirectoryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox(position, "Directory attribute only supports string fields.", MessageType.Error);
                return;
            }

            // Set the height of the row
            var rowHeight = EditorGUIUtility.singleLineHeight;

            // Draw the label first (label width is the default indent width)
            var labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, rowHeight);
            EditorGUI.LabelField(labelPosition, label);

            // Calculate the remaining width for the value area
            float valueAreaWidth = position.width - EditorGUIUtility.labelWidth;

            // Draw the Browse button to the right of the value area, filling the remaining width
            var buttonPosition = new Rect(position.x + labelPosition.width, position.y, valueAreaWidth, rowHeight);
            if (GUI.Button(buttonPosition, property.stringValue != "" ? property.stringValue : "Browse"))
            {
                // Open the folder selection dialog
                string selectedPath = EditorUtility.OpenFolderPanel("Select Directory", "Assets", "");

                // If a folder was selected and the path starts with "Assets/"
                if (!string.IsNullOrEmpty(selectedPath) && selectedPath.StartsWith(Application.dataPath))
                {
                    // Get the relative path to the "Assets" folder
                    string relativePath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                    property.stringValue = relativePath;
                }
            }
        }
    }
}
#endif