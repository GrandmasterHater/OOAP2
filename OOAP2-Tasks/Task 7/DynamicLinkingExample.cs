using OOAP2_Tasks.Task_2;

namespace OOAP2_Tasks.Task_7;

public class DynamicLinkingExample
{
    // Тип описывающий зелье, которое может применять персонаж.
    public abstract class Potion
    {
        // Запрос получения эффекта от зелья для персонажа.
        public abstract void Apply(Character character);
    }

    
    public class WitcherHealPotion : Potion
    {
        public override void Apply(Character character)
        {
            // Восстанавливаем здоровье персонажа ведьмак и наносит урон остальным.
        }
    }

    public class BlackBlood : Potion
    {
        public override void Apply(Character character)
        {
            // Наносим персонажу с типом вампир урон
        }
    }
    
    // Тип всех персонажей
    public abstract class Character
    {
        // Тут происходит динамическое связывание. Метод TakePotion вызывает команду применения зелья к персонажу (Apply), у применяемого зелья. В момент компиляции 
        // неизвестно какое будет использовано зелье, эффект будет получен только в момент исполнения от конкретной реализации.
        public virtual void TakePotion(Potion potion) =>
            potion.Apply(this);
    }

    // Конкретный тип персонажа - ведьмак.
    public class Witcher : Character
    {
        // Уникальная реализация персонажа
    }
    
    public class Human : Character { }
    
    public class Vampire : Character { }
}