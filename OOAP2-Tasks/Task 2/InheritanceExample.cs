namespace OOAP2_Tasks.Task_2
{
    // Базовый класс всех персонажей для поединка. Определяет основные действия персонажа. От него наследуются все персонажи.
    public abstract class Character
    {
        protected Health Health { get; }
        protected Weapon CurrentWeapon { get; }
        
        public abstract void Attack(Character enemy);

        public abstract void TakeDamage(Weapon weapon);
    }
    
    public class Witcher : Character
    {
        public override void Attack(Character enemy) => enemy.TakeDamage(CurrentWeapon);
        
        // Расширение класса родителя - тип Ведьмак добавляет поведение использование зелья.
        public void TakePotion(Potion potion) => Health.Increment(potion.Value);
        public override void TakeDamage(Weapon weapon) => Health.Decrement(weapon.Damage * 0.1d);
    }
    
    public class Ghost : Character
    {
        public override void Attack(Character enemy) => enemy.TakeDamage(CurrentWeapon);

        // Специализация поведения родителя - получение урона только от серебряного оружия.
        public override void TakeDamage(Weapon weapon)
        {
            if (WeaponType.IsSilverWeapon(Weapon))
            {
                Health.Decrement(weapon.Damage);
            }
        }
    }
    
    public interface MagicAttack
    {
        void CastSpell(Character enemy);
    }
    
    // В C# комбинация нескольких родительских классов может быть реализована через интерфейсы или композицию.
    // В данном случае комбинируется базовое поведение персонажа и способность накладывать заклятия. (Хотя это и не совсем наследование )
    public class Wizard : Character, MagicAttack
    {
        public Spell Spell { get; }
        
        public override void Attack(Character enemy)
        {
            enemy.TakeDamage(CurrentWeapon);
        }

        public override void TakeDamage(Weapon weapon)
        {
            Health.Decrement(weapon.Damage * 1.5d);
        }

        public void CastSpell(Character enemy)
        {
            Spell.Apply(enemy);
        }
    }
}