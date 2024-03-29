﻿using System.Reflection;
using System.Collections.Generic;
using System;

namespace WeatherApp.Extenstions
{
    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(string)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        public static object DeepCopy(this object originalobject)
        {
            return InternalCopy(originalobject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
        }
        private static object InternalCopy(object originalobject, IDictionary<object, object> visited)
        {
            if (originalobject is null)
                return null;
            var typeToReflect = originalobject.GetType();
            if (IsPrimitive(typeToReflect))
                return originalobject;

            if (visited.ContainsKey(originalobject))
                return visited[originalobject];

            if (typeof(Delegate).IsAssignableFrom(typeToReflect))
                return null;

            var cloneobject = CloneMethod.Invoke(originalobject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneobject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }

            visited.Add(originalobject, cloneobject);
            CopyFields(originalobject, visited, cloneobject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalobject, visited, cloneobject, typeToReflect);
            return cloneobject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalobject, IDictionary<object, object> visited, object cloneobject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalobject, visited, cloneobject, typeToReflect.BaseType);
                CopyFields(originalobject, visited, cloneobject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalobject, IDictionary<object, object> visited, object cloneobject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false)
                    continue;

                if (IsPrimitive(fieldInfo.FieldType))
                    continue;

                var originalFieldValue = fieldInfo.GetValue(originalobject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneobject, clonedFieldValue);
            }
        }
        public static T DeepCopy<T>(this T original)
        {
            return (T)DeepCopy((object)original);
        }
    }

    public class ReferenceEqualityComparer : EqualityComparer<object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }
        public override int GetHashCode(object obj)
        {
            if (obj is null) return 0;
            return obj.GetHashCode();
        }
    }
}