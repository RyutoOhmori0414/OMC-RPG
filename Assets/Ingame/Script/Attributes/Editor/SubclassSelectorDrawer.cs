using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RPG.Attribute.Editor
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public sealed class SubclassSelectorDrawer : PropertyDrawer
    {
        private bool _initialized = false;
        private Type[] _inheritedTypes;
        private string[] _typePopupNameArray;
        private string[] _typeFullNameArray;
        private int _currentTypeIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference) return;

            if (!_initialized)
            {
                Init(property);
                _initialized = true;
            }

            GetCurrentTypeIndex(property.managedReferenceFullTypename);
            int selectedTypeIndex = EditorGUI.Popup(GetPopupPosition(position), _currentTypeIndex, _typePopupNameArray);
            UpdatePropertyToSelectedTypeIndex(property, selectedTypeIndex);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
            => EditorGUI.GetPropertyHeight(property, true);

        private void Init(SerializedProperty property)
        {
            SubclassSelectorAttribute utility = (SubclassSelectorAttribute)attribute;
            GetAllInheritedTypes(GetType(property), utility.IncludeMono);
            GetInheritedNameArrays();
        }

        private void GetCurrentTypeIndex(string typeFullName)
            => _currentTypeIndex = Array.IndexOf(_typeFullNameArray, typeFullName);

        private void GetAllInheritedTypes(Type baseType, bool includeMono)
        {
            var monoType = typeof(MonoBehaviour);
            _inheritedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p.IsClass && (!monoType.IsAssignableFrom(p) || includeMono))
                .Prepend(null)
                .ToArray();
        }

        private void GetInheritedNameArrays()
        {
            _typePopupNameArray = _inheritedTypes.Select(type => type == null ? "<null>" : type.ToString()).ToArray();
            _typeFullNameArray = _inheritedTypes.Select(type => type == null ? "" : $"{type.Assembly.ToString().Split(',')[0]} {type.FullName}").ToArray();
        }

        public void UpdatePropertyToSelectedTypeIndex(SerializedProperty property, int selectedTypeIndex)
        {
            if (_currentTypeIndex == selectedTypeIndex) return;

            _currentTypeIndex = selectedTypeIndex;
            Type selectedType = _inheritedTypes[selectedTypeIndex];
            property.managedReferenceValue = selectedType == null ? null : Activator.CreateInstance(selectedType);
        }

        private Rect GetPopupPosition(Rect currentPosition)
        {
            var popupPosition = new Rect(currentPosition);
            popupPosition.width -= EditorGUIUtility.labelWidth;
            popupPosition.x += EditorGUIUtility.labelWidth;
            popupPosition.height = EditorGUIUtility.singleLineHeight;
            return popupPosition;
        }

        public static Type GetType(SerializedProperty property)
        {
            const BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public |
                                             BindingFlags.FlattenHierarchy | BindingFlags.Instance;

            var propertyPaths = property.propertyPath.Split(".");
            var parentType = property.serializedObject.targetObject.GetType();
            var fieldInfo = parentType.GetField(propertyPaths[0], bindingAttr);
            var fieldType = fieldInfo.FieldType;

            if (propertyPaths.Contains("Array"))
            {
                if (fieldType.IsArray)
                {
                    return fieldType.GetElementType();
                }
                else
                {
                    return fieldType.GetGenericArguments()[0];
                }
            }

            return fieldType;
        }
    }
}