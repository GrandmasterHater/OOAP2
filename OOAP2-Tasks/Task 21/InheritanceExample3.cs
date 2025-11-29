namespace OOAP2_Tasks.Task_21.InheritanceExample;

public class State
{
    // Набор методов для работы с состоянием некоторого конечного автомата.
}

// Пример наследования реализации: Наследование от State позволяет переиспользовать реализации работы со стейтами и
// при этом не передаёт никакого смысла с точки зрения абстракции (АТД). BaseViewState адаптирует реализацию состояния
// для работы с визуалом.
public class BaseViewState : State { }

// Пример льготного наследования - используем готовый функционал для конкретных целей в виде состояний кнопки "В бой" 
// в онлайн игре.
public class WaitForMatchingState : BaseViewState { }

public class StartingBattleState : BaseViewState { }