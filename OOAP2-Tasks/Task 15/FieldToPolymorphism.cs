namespace OOAP2_Tasks.Task_15.FieldExample
{
    public enum CardType
    {
        Meelee,
        Range,
        Siege
    }
    
    public class Card
    {
        // Тип карты отряда.
        public CardType CardType { get; }
    }
    
    public class CardField
    {
        // Пример определения расположения карты отряда по полю.
        public void PlaceCard(Card card)
        {
            switch(card.CardType)
            {
                case CardType.Meelee:
                    // Расположить в разделе поля для карт ближнего боя. 
                    break;
                case CardType.Range:
                    // Расположить в разделе поля для карт дальнего боя.
                    break;
                case CardType.Siege:
                    // Расположить в разделе поля для карт осадных отрядов.
                    break;
                default:
                    // Выставить статус невалидного типа отряда.
                    break;
            }
        }
    }
}

namespace OOAP2_Tasks.Task_15.PolymorphismExample
{

    public abstract class Card
    {
        // Полиморфная версия расположения отряда
        public abstract void PlaceCard(CardField field);
    }

    public class MeeleeCard : Card
    {
        public override void PlaceCard(CardField field) => field.PlaceCard(this);
    }

    public class RangeCard : Card
    {
        public override void PlaceCard(CardField field) => field.PlaceCard(this);
    }

    public class SiegeCard : Card
    {
        public override void PlaceCard(CardField field) => field.PlaceCard(this);
    }

    public class CardField
    {
        public void PlaceCard(MeeleeCard card)
        {
            // Расположить в разделе поля для карт ближнего боя.
        }
    
        public void PlaceCard(RangeCard card)
        {
            // Расположить в разделе поля для карт дальнего боя.
        }
    
        public void PlaceCard(SiegeCard card)
        {
            // Расположить в разделе поля для карт осадных отрядов.
        }
    }
}


