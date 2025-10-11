namespace OOAP2_Tasks.Task1
{
    public abstract class Weapon
    {
        public double Damage { get; }
    }
    
    public class Health
    {
        public double MaxValue { get; }
        
        public double Value { get; private set; }
        
        public bool IsEmpty { get; }

        public Health(double maxValue)
        {
            MaxValue = maxValue;
            Value = MaxValue;
        }
        
        public void Increment(double value) => Value += value;
        
        public void Decrement(double value) => Value -= value;
        
    }
    
    // Базовый класс всех персонажей для поединка. Определяет основные действия персонажа. От него наследуются все персонажи.
    public abstract class Character
    {
        // Для получения нужного базового состояния используется композиция. Класс персонажа содержит (has a) здоровье юнита и оружие.
        protected Health Health { get; }
        protected Weapon CurrentWeapon { get; }

        public bool IsAlive => !Health.IsEmpty;
        
        public Character(Health health, Weapon weapon)
        {
            Health = health;
            CurrentWeapon = weapon;
        }

        public abstract void Attack(Character enemy);

        public abstract void TakeDamage(Weapon weapon);
    }

    // Наследники класса персонаж - представляют связь (is-a).
    
    public class Witcher : Character
    {
        public Witcher(Health health, Weapon weapon) : base(health, weapon) { }

        public override void Attack(Character enemy) => enemy.TakeDamage(CurrentWeapon);

        public override void TakeDamage(Weapon weapon) => Health.Decrement(weapon.Damage * 0.1d);
    }
    
    
    public class Assassin : Character
    {
        public Assassin(Health health, Weapon weapon) : base(health, weapon) { }

        public override void Attack(Character enemy) => enemy.TakeDamage(CurrentWeapon);

        public override void TakeDamage(Weapon weapon) => Health.Decrement(weapon.Damage);
    }
    
    public class Military : Character
    {
        public Military(Health health, Weapon weapon) : base(health, weapon) { }

        public override void Attack(Character enemy) => enemy.TakeDamage(CurrentWeapon);
        
        public override void TakeDamage(Weapon weapon) => Health.Decrement(weapon.Damage * 0.7d);
    }
    
    // Бой - дуэль в которой сражаются персонажи.
    public class DuelBattle
    {
        public Character _firstParticipant;
        public Character _secondParticipant;

        public bool IsFinished => !_firstParticipant.IsAlive || !_secondParticipant.IsAlive;
        
        public DuelBattle(Character firstParticipant, Character secondParticipant)
        {
            _firstParticipant = firstParticipant;
            _secondParticipant = secondParticipant;
        }

        public void ExecuteBattleCycle()
        {
            if (!IsFinished)
            {
                // Каждый персонаж реализует атаку и применение урона по своему, используя при этом единый интерфейс персонажа - это полиморфизм.
                _firstParticipant.Attack(_secondParticipant);
                _secondParticipant.Attack(_firstParticipant);
            }
        }
    }
}