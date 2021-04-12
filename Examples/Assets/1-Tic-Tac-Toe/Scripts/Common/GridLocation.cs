#nullable enable
using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

#endif

namespace PlayduxExamples.TicTacToe.Scripts.Common
{
    [Serializable]
    public struct GridLocation
    {
        public int Row;
        public int Column;

        public override string ToString() => $"{{ {nameof(Row)}: {Row}, {nameof(Column)}: {Column} }}";
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(GridLocation))]
    public class GridLocationDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            rect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);

            const float padding = 2.5f;
            var propertyWidth = rect.width / 2 - padding;
            var rowRect = new Rect(rect.x, rect.y, propertyWidth, rect.height);
            var columnRect = new Rect(rect.x + propertyWidth + padding, rect.y, propertyWidth, rect.height);

            EditorGUI.PropertyField(rowRect, property.FindPropertyRelative("Row"), GUIContent.none);
            EditorGUI.PropertyField(columnRect, property.FindPropertyRelative("Column"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
#endif
}