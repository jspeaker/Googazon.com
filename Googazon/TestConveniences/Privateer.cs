using System;
using System.Linq;
using System.Reflection;

namespace TestConveniences
{
    public class Privateer
    {
        public T Object<T>(params object[] parameters) where T : class
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                parameters.Select(p => p.GetType()).ToArray(), null);
            if (constructor == null)
                throw new NullReferenceException(
                    $"Private constructor for type '{nameof(T)}' with the specified parameters was not found.");

            return (T) constructor.Invoke(parameters);
        }

        public T Field<T>(object instance, string fieldName)
        {
            FieldInfo field = instance.GetType().GetField(fieldName,
                BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            return (T) field.GetValue(instance);
        }

        public T BaseField<T>(object instance, string fieldName)
        {
            Type memberInfo = instance.GetType().BaseType;
            if (memberInfo == null) return default(T);

            FieldInfo field = memberInfo.GetField(fieldName,
                BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.FlattenHierarchy);
            if (field == null)
                throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            return (T) field.GetValue(instance);
        }

        public T TwoDeepBaseField<T>(object instance, string fieldName)
        {
            Type memberInfo = instance.GetType().BaseType?.BaseType;
            if (memberInfo == null) return default(T);

            FieldInfo field = memberInfo.GetField(fieldName,
                BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.FlattenHierarchy);
            if (field == null)
                throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            return (T) field.GetValue(instance);
        }

        public void SetField<T>(object instance, string fieldName, T value)
        {
            FieldInfo field = instance.GetType().GetField(fieldName,
                BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            field.SetValue(instance, value);
        }

        public void SetBaseField<T>(object instance, string fieldName, T value)
        {
            Type memberInfo = instance.GetType().BaseType;
            if (memberInfo == null) return;

            FieldInfo field = memberInfo.GetField(fieldName,
                BindingFlags.Static | BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
                throw new NullReferenceException($"Private field '{fieldName}' for type '{nameof(T)}' was not found.");

            field.SetValue(instance, value);
        }

        public T Property<T>(object instance, string propertyName)
        {
            PropertyInfo property = instance.GetType().GetProperty(propertyName,
                BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
            if (property == null)
                throw new NullReferenceException(
                    $"Private property '{propertyName}' for type '{nameof(T)}' was not found.");

            return (T) property.GetValue(instance);
        }
    }
}