using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace OOAP2_Tasks.Task_9;

[Serializable]
public class General : object, IEquatable<General>, ISerializable
{
    public General() { }
    
    // Десериализация объекта
    public General(SerializationInfo info, StreamingContext context)
    {
        OnDeserialize(info, context);
    }
    
    // Сериализация объекта
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => 
        OnSerialize(info, context); 
    
    public virtual void OnSerialize(SerializationInfo info, StreamingContext context)
    {
        var fields = GetRealType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsSerializable)
                info.AddValue(field.Name, field.GetValue(this), field.FieldType);
        }
    }

    public virtual void OnDeserialize(SerializationInfo info, StreamingContext context)
    {
        var fields = GetRealType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsSerializable)
            {
                object deserializedValue = info.GetValue(field.Name, field.FieldType);
                field.SetValue(this, deserializedValue);
            }
        }
    }
    
    // Копирование объекта
    public virtual void CopyTo(General otherObject)
    {
        if (otherObject is null)
            throw new ArgumentNullException(nameof(otherObject));
        
        MemberwiseCopy(this, otherObject);
    }

    // Глубокое копирование объекта (рекурсивное)
    public virtual void DeepCopyTo(General otherObject)
    {
        if (otherObject is null)
            throw new ArgumentNullException(nameof(otherObject));
        
        var visited = new Dictionary<object, object>(ReferenceEqualityComparer.Instance);
        DeepCopyRecursive(this, otherObject, visited);
    }
	
    // Клонирование объекта
    public virtual General Clone()
    {
        General clone = (General)Activator.CreateInstance(GetType());
        DeepCopyTo(clone);
        
        return clone;
    }

    public bool IsTypeOf(Type type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));
        
        return this.GetType() == type;
    }
        
    // Получение реального типа объекта.
    public Type GetRealType() => 
        this.GetType();

    // Сравнение объекта по ссылке
    public virtual bool ReferenceEquals(General otherObject)
    {
        if (otherObject is null)
            return false;
        
        return RuntimeHelpers.ReferenceEquals(this, otherObject);
    }
    
    // Сравнение объекта по значениям
    public bool Equals(General otherObject)
    {
        if (otherObject == null)
            return false;

        if (ReferenceEquals(otherObject))
            return true;

        if (!IsTypeOf(otherObject.GetRealType()))
            return false;

        Type objectsType = this.GetRealType();
        
        return objectsType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).All(fieldInfo =>
        {
            Type fieldType = fieldInfo.FieldType;
            
            if (fieldType.IsValueType || fieldType == typeof(string))
                return object.Equals(fieldInfo.GetValue(this), fieldInfo.GetValue(otherObject));;
            
            return ReferenceEquals(fieldInfo.GetValue(this), fieldInfo.GetValue(otherObject)); 
        });
    }
    
    // Строковое представление объекта
    public override string ToString() => GetType().Name;

    
    private void MemberwiseCopy(object source, object target)
    {
        if (source == null || target == null)
            return;
        
        FieldInfo[] fields = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            MemberwiseCopyField(field, source, target);
        }
    }
    
    private void MemberwiseCopyField(FieldInfo field, object source, object target )
    {
        object fieldValue = field.GetValue(source);
        
        field.SetValue(target, fieldValue);
    }
    
    private void DeepCopyRecursive(object source, object target, IDictionary<object, object> visited)
    {
        if (source == null || target == null)
            return;
        
        if (visited.ContainsKey(source))
            return;

        visited[source] = target;
        
        FieldInfo[] fields = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (FieldInfo field in fields)
        {
            CopyField(field, source, target, visited);
        }
    }

    private void CopyField(FieldInfo field, object source, object target, IDictionary<object, object> visited)
    {
        object fieldValue = field.GetValue(source);
        Type fieldType = field.FieldType;
        
        if (fieldValue == null)
        {
            field.SetValue(target, null);
            return;
        }

        if (fieldType.IsValueType || fieldType == typeof(string))
        {
            field.SetValue(target, fieldValue);
            return;
        }
            
        if (visited.TryGetValue(fieldValue, out object existingCopy))
        {
            field.SetValue(target, existingCopy);
            return;
        }
            
        if (typeof(IEnumerable).IsAssignableFrom(fieldType) || typeof(IEnumerable<>).IsAssignableFrom(fieldType))
        {
            bool isArray = source.GetType().IsArray;
            object сollectionСopy = isArray ? CopyArray(fieldValue, visited) : CopyCollection(fieldValue, visited);
            field.SetValue(target, сollectionСopy);
            visited[fieldValue] = сollectionСopy!;
            return;
        }
            
        object instance = Activator.CreateInstance(fieldType, nonPublic: true)!;
        visited[fieldValue] = instance;
        DeepCopyRecursive(fieldValue, instance, visited);
        field.SetValue(target, instance);
    }

    private object CopyArray(object source, IDictionary<object, object> visited)
    {
        Type sourceType = source.GetType();
        Type itemType = sourceType.GetElementType()!;
        Array sourceArray = (Array)source;
        Array destinationArray = Array.CreateInstance(itemType, sourceArray.Length);
        visited[source] = destinationArray;

        Func<object, IDictionary<object, object>, object> getCopy = itemType.IsValueType || itemType == typeof(string)
            ? GetCopyOfValueType
            : GetCopyOfReferenceType;

        for (int i = 0; i < sourceArray.Length; i++)
        {
            object item = sourceArray.GetValue(i);
            object itemCopy = getCopy(item, visited);
            
            destinationArray.SetValue(itemCopy, i);
        }

        return destinationArray;
    }

    private object CopyCollection(object source, IDictionary<object, object> visited)
    {
        Type sourceType = source.GetType();
        
        object collectionCopy = Activator.CreateInstance(sourceType, nonPublic: true);
        visited[source] = collectionCopy;
        
        MethodInfo addMethod = sourceType.GetMethod(nameof(ICollection<object>.Add));
        
        foreach (object item in source as IEnumerable)
        {
            object itemCopy = null;
            
            if (item != null)
            {
                Type itemType = item.GetType();

                Func<object, IDictionary<object, object>, object> getCopy =
                    itemType.IsValueType || itemType == typeof(string)
                        ? GetCopyOfValueType
                        : GetCopyOfReferenceType;

                itemCopy = getCopy(item, visited);
            }

            addMethod!.Invoke(collectionCopy, new[] { itemCopy });
        }

        return collectionCopy!;
    }
    
    private object GetCopyOfValueType(object item, IDictionary<object, object> visited) => item;
    
    private object GetCopyOfReferenceType(object item, IDictionary<object, object> visited)
    {
        if (item == null)
            return null;
        
        object itemCopy = Activator.CreateInstance(item.GetType());
        DeepCopyRecursive(item, itemCopy, visited);

        return itemCopy;
    }

    // На случай если кто-то из наследников переопределит Equals
    private class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static ReferenceEqualityComparer Instance { get; } = new ReferenceEqualityComparer();
        
        public bool Equals(object x, object y) =>
            RuntimeHelpers.ReferenceEquals(x, y);

        public int GetHashCode(object obj) =>
            obj.GetHashCode();
    }
}

public class Any : General
{
    
}