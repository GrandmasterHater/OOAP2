using System;
using System.Collections.Generic;

namespace OOAP2_Tasks.Task_8;

public class InOutExample
{
    #region Ковариантность
    
    // Тип всех персонажей
    public abstract class Character { }

    // Конкретный тип персонажа - ведьмак.
    public class Witcher : Character { }
    
    public class Human : Character { }
    
    public class Vampire : Character { }

    // Указываем, что фабрика использует ковариантность через параметр out в определении дженерик типа.
    public interface CharacterFactory<out TCharacter> where TCharacter : Character
    {
        TCharacter Create();
    }
    
    public class WitcherFactory : CharacterFactory<Witcher>
    {
        public Witcher Create() => new Witcher();
    }
    
    public class HumanFactory : CharacterFactory<Human>
    {
        public Human Create() => new Human();
    }

    public class Battle
    {
        public CharacterFactory<Character>[] GetFactoriesForWitcherAndHuman()
        {
            // Ковариантность - тип фабрики задаётся более общим типом, а присваивается контейнер с более конкретным типом.
            CharacterFactory<Character>[] factories = new CharacterFactory<Character>[2];
            factories[0] = new WitcherFactory();
            factories[1] = new HumanFactory();

            return factories;
        }
        
        public void SpawnCharactersForBattle()
        {
            CharacterFactory<Character>[] characterFactories = GetFactoriesForWitcherAndHuman();
        
            // Благодаря ковариантности мы можем работать с фабриками через общий интерфейс
            foreach (var characterFactory in characterFactories)
            {
                // Здесь character будут Witcher и Human.
                Character character = characterFactory.Create();
            }
        }
    }
    
    #endregion

    #region Контравариантность
    
    // Универсальный отправитель событий - работает с любым персонажем
    public class CharacterStatsEventSender<TCharacter>
    {
        public virtual void SendEvent(TCharacter character) { }
    }
    
    // Специализированный исполнитель для ведьмаков
    public class WitcherStatsEventSender : CharacterStatsEventSender<Witcher>
    {
        public override void SendEvent(Witcher character)
        {
            // Дополнительные параметры для типа ведьмака
        }
    }

    public class EventLogger
    {
        public void LogWitcher(Witcher witcher)
        {
            Action<Witcher> eventSender = new WitcherStatsEventSender().SendEvent;

            // Типы совпадают, работает как обычно.
            eventSender(witcher);
            
            // Дженерик тип делегата более конкретный чем тип в отправителе событий. Тут работает контравариантность.
            eventSender = new CharacterStatsEventSender<Character>().SendEvent;
        }
    }

    #endregion
}