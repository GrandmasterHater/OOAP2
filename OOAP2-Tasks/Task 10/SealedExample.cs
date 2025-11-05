namespace OOAP2_Tasks.Task_10
{
    public abstract class Character
    {
        public abstract void Attack();

        public abstract void Heal();
    }
    
    public abstract class Vampire : Character
    {
        public override void Attack()
        {
            // Базовая реализация команды атаки для всех вампиров.
        }

        public override void Heal()
        {
            // Базовая реализация команды восстановления здоровья для всех вампиров.
        }
    }
    
    // Разновидность вампира - Катакан. Класс задаёт и определяет все операции над этим типом.
    public class VampireKatakan : Vampire
    {
        public override void Attack()
        {
            // Реализация атаки для этого типа вампиров.
        }

        public sealed override void Heal()
        {
            // Реализация восстановления здоровья для всех вампиров с типом "Катакан". В игре все вампиры этого типа
            // должны восстанавливать здоровье одинаково. По этой причине запрещаем переопределение этой команды в наследниках
            // используя ключевое слово sealed.
        }
    }
    
    // Уникальная разновидность вампира, со своим уникальным поведением, которое не должно переопределяться нигде более.
    // Используем ключевое слово sealed, для запрета переопределения класса.
    public sealed class OldVampireKatakan : VampireKatakan
    {
        public override void Attack()
        {
            // Реализация атаки для этого типа вампиров.
        }
        
        
        //public override void Heal() - так записать уже не можем поскольку есть запрет на переопределение этого метоа.
    }
}