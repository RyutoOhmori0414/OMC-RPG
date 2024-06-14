using System;
using UnityEngine;

namespace RPG.Attribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SubclassSelectorAttribute : PropertyAttribute
    {
        public bool IncludeMono { get; }

        public SubclassSelectorAttribute(bool includeMono = false)
        {
            IncludeMono = includeMono;
        }
    }   
}