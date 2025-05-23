#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UnityEssentials
{
    /// <summary>
    /// Provides a custom property drawer for fields marked with the <see cref="DirectoryAttribute"/>.
    /// </summary>
    /// <remarks>This drawer is designed to work with string fields in Unity's Inspector. It displays a button
    /// that allows users to select a directory using a folder selection dialog. The selected directory path is stored
    /// as a relative path starting from the "Assets" folder.</remarks>
    [CustomPropertyDrawer(typeof(DirectoryAttribute))]
    public class DirectoryDrawer : PropertyDrawer
    {
        /// <summary>
        /// Renders a custom GUI for a string property that represents a directory path, allowing the user to select a
        /// directory via a folder selection dialog.
        /// </summary>
        /// <remarks>This method displays a button labeled "Select Directory ..." or the current directory
        /// path.  When the button is clicked, a folder selection dialog is shown, allowing the user to choose a
        /// directory.  If a valid directory is selected, the property's value is updated with the relative path to the
        /// "Assets" folder.</remarks>
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

            var buttonClicked = GUI.Button(buttonPosition, string.IsNullOrEmpty(property.stringValue) ? "Select Directory ..." : "./" + property.stringValue);
            var keyboardClicked = InspectorFocusedHelper.ProcessKeyboardClick(buttonPosition);
            if (buttonClicked || keyboardClicked)
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