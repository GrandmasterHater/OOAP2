namespace OOAP2_Tasks.Task_20.InheritanceExample;

#region Наследования вариаций

public class Item { }

public class Bag
{
    public virtual void Add(Item item)
    {
        
    }
}

// Наследование с функциональной вариацией (functional variation inheritance)
public class MagicBag : Bag
{
    public override void Add(Item item)
    {
        // Новая логика, которая удваивает добавляемые предметы.
    }
}

// Наследование с вариацией типа (type variation inheritance)
public class CustomBag : Bag
{
    public void Add(Item item, int count)
    {
        // Логика добавления с учётом количества.
    }
}

#endregion


#region Наследование с конкретизацией (reification inheritance)

public abstract class Window
{
    public void Open()
    {
        // Некоторая общая логика открытия окна

        try
        {
            OnOpen();
        }
        catch (Exception e)
        {
            // Обработка исключения
        }
    }
    
    protected abstract void OnOpen();
}

public class PopupWindow : Window
{
    protected override void OnOpen()
    {
        // Дополнение частичной реализации логики родительского типа Window.
    }
}

#endregion


#region Структурное наследование (structure inheritance)

//Классический пример .NET - реализация интерфейса ICloneable. Тип документ получет новую способность - клонироваться.
public class Document : ICloneable
{
    public string Content { get; set; }

    public object Clone()
    {
        return new Document { Content = this.Content };
    }
}

#endregion

