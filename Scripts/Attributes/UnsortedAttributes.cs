using System;
using UnityEditor;
using UnityEditor.Graphs;
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

    /// <summary>
    ///  Draws a horizontal line in the inspector.
    /// <param><name>Thickness</name></param>
    /// <param><name>Padding</name></param>
    /// </summary>
    public class HorizontalLineAttribute : PropertyAttribute
    {
        public int Thickness { get; set; }
        public int Padding { get; set; }
        
        public HorizontalLineAttribute(int thickness, int padding)
        {
            Thickness = thickness;
            Padding = padding;
        }
    }

    /// <summary>
    ///  Draws a note in the inspector.
    /// <param>The text to display.<name>text</name></param>
    /// <param>The type of message to display.<name>messageType</name></param>
    /// <see cref="MessageType"/>
    /// </summary>
    public class NoteAttribute : PropertyAttribute
    {
        public string Text { get; }
        
        public MessageType MessageType { get; set; } 

        public NoteAttribute(string text, MessageType messageType = MessageType.None)
        {
            Text = text;
            MessageType = messageType;
        }
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
    
    [CustomPropertyDrawer(typeof(HorizontalLineAttribute))]
    public class HorizontalLineDrawer : DecoratorDrawer
    {
        public override void OnGUI (Rect position)
        {
            if (attribute is not HorizontalLineAttribute attr) return;
            
            position.height = attr.Thickness;
            position.y += attr.Padding * .5f;
            
            EditorGUI.DrawRect(position, EditorGUIUtility.isProSkin ? new Color(.7f, .7f, .7f) :  new Color(.2f, .2f, .2f));
        }

        public override float GetHeight()
        {
            if (attribute is not HorizontalLineAttribute attr) return base.GetHeight();
            return Mathf.Max(attr.Padding + attr.Thickness);
        }
    }
    
    [CustomPropertyDrawer(typeof(NoteAttribute))]
    public class NoteDrawer : DecoratorDrawer
    {
        private float _height;
        private const float PADDING = 20;
        private float _width;
        public override void OnGUI (Rect position)
        {
            if (attribute is not NoteAttribute attr) return;
            position.height = _height;
            position.width = _width;
            position.y += PADDING *.5f;
            EditorGUI.HelpBox(position, attr.Text, (UnityEditor.MessageType)attr.MessageType);
        }

        public override float GetHeight()
        {
            if (attribute is not NoteAttribute attr) return base.GetHeight();
            var style = EditorStyles.helpBox;
            style.alignment = TextAnchor.MiddleLeft;
            style.wordWrap = true;
            style.padding = new RectOffset(10, 10, 10, 10);
            style.fontSize = 12;
            
            _width = Mathf.Clamp(attr.Text.Length * 8f, 100f, EditorGUIUtility.currentViewWidth - PADDING);

            _height = style.CalcHeight(new GUIContent(attr.Text), _width - PADDING);
            return _height + PADDING;
        }
    }

#endif
}
