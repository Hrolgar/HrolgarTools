using System;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Attributes
{
    public class ReadOnlyAttribute : PropertyAttribute
    {
        /// <summary>
        /// Makes a field read-only in the inspector, and greyed out.
        /// But it can still be changed in code.
        /// </summary>
        public ReadOnlyAttribute()
        {
        }
    }

    public class RangeWithSliderAttribute : PropertyAttribute
    {
        public float Min { get; }
        public float Max { get; }
        public bool ShowSlider { get; }

        /// <summary>
        /// Makes a field have a slider in the inspector.
        /// </summary>
        /// <param name="min">The minimum value of the slider.</param>
        /// <param name="max">The maximum value of the slider.</param>
        /// <param name="showSlider">Whether or not to show the slider.</param>
        public RangeWithSliderAttribute(float min, float max, bool showSlider = true)
        {
            Min = min;
            Max = max;
            ShowSlider = showSlider;
        }
    }
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumFlagAttribute : PropertyAttribute
    {
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
    
    [CustomPropertyDrawer(typeof(RangeWithSliderAttribute))]
    public class RangeWithSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var range = (RangeWithSliderAttribute) attribute;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float when range.ShowSlider:
                    property.floatValue = EditorGUI.Slider(position, label, property.floatValue, range.Min, range.Max);
                    break;
                case SerializedPropertyType.Float:
                    EditorGUI.PropertyField(position, property, label);
                    break;
                case SerializedPropertyType.Integer when range.ShowSlider:
                    property.intValue = (int) EditorGUI.Slider(position, label, property.intValue, range.Min, range.Max);
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUI.PropertyField(position, property, label);
                    break;
                default:
                    EditorGUI.PropertyField(position, property, label);
                    break;
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(EnumFlagAttribute))]
    public class EnumFlagDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }
    }
#endif
}
