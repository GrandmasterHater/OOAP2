namespace OOAP2_Tasks.Task_13
{
    public abstract class BaseClassA
    {
        // 1. метод публичен в родительском классе А и публичен в его потомке B;
        public void PublicHidingExample() { }
        
        // 2. метод публичен в родительском классе А и скрыт в его потомке B;
        public void PublicToPrivateHidingExample() { }
        
        // 3. метод скрыт в родительском классе А и публичен в его потомке B;
        private void PrivateToPublicHidingExample() { }
        
        // 4. метод скрыт в родительском классе А и скрыт в его потомке B.
        private void PrivateToPrivateHidingExample() { }
    }
    
    public class DerivedClassB : BaseClassA
    {
        // 1. метод публичен в родительском классе А и публичен в его потомке B;
        // Используем тот же модификатор доступа и ключевое слово new.
        public new void PublicHidingExample() { }
        
        // 2. метод публичен в родительском классе А и скрыт в его потомке B;
        // Используем более узкий модификатор доступа и ключевое слово new.
        private new void PublicToPrivateHidingExample() { }
        
        // 3. метод скрыт в родительском классе А и публичен в его потомке B;
        // Используем более широкий модификатор доступа. С точки зрения языка это нее совсем сокрытие, приватные методы не наследуются,
        // просто определение нового метода в наследнике с тем же именем.
        public void PrivateToPublicHidingExample() { }
        
        // 4. метод скрыт в родительском классе А и скрыт в его потомке B.
        // Используем тот же модификатор доступа и имя метода. С точки зрения языка это нее совсем сокрытие, приватные методы не наследуются,
        // просто определение нового метода в наследнике с тем же именем.
        private void PrivateToPrivateHidingExample() { }
    }
}