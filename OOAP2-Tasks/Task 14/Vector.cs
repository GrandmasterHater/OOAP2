using OOAP2_Tasks.Task_12;

namespace OOAP2_Tasks.Task_14;

public interface Addable<T>
{
    T Sum(T other);
}

public class Vector<T> : Any, Addable<Vector<T>> where T : General, Addable<T>
{
    private T[] _elements;
    
    public int Length => _elements.Length;

    public T this[int index]
    {
        get => _elements[index];
        set => _elements[index] = value;
    }
    
    public Vector(int size)
    {
        _elements = new T[size];
    }


    public Vector<T> Sum(Vector<T> other)
    {
        if (other is null)
        {
            return null;
        }
        
        if (Length != other.Length)
            return null;
        
        Vector<T> result = new Vector<T>(Length);

        for (int i = 0; i < Length; i++)
        {
            if (this[i] == null || other[i] == null)
                result[i] = null;
            else
                result[i] = this[i].Sum(other[i]);
        }
        
        return result;
    }
}

public class SomeType : Any, Addable<SomeType>
{
    public int Value { get; }
    
    public SomeType(int value) => Value = value;
    
    public SomeType Sum(SomeType other) => new SomeType(Value + other.Value);
}

public class Test
{
    public void Sum()
    {
        Vector<SomeType> first = new Vector<SomeType>(3);
        Vector<SomeType> second = new Vector<SomeType>(3);
        
        Vector<SomeType> result = first.Sum(second);
        
        Vector<Vector<Vector<SomeType>>> firstCube = new Vector<Vector<Vector<SomeType>>>(3);
        Vector<Vector<Vector<SomeType>>> secondCube = new Vector<Vector<Vector<SomeType>>>(3);

        Vector<Vector<Vector<SomeType>>> resultCube =firstCube.Sum(secondCube);
    }
}