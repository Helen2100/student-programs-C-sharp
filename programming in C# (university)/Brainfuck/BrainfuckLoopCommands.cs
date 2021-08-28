using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {//объ€вл€ем переменные
            var squareBracket = new Dictionary<int, int>();//словарь дл€ скобок
            var positions = new Stack<int>();//стек дл€ индекса открывающей скобки
            int positionOpeningBracket;//переменна€ дл€ позиции откр.скобки
            int positionClosingBracket;//переменна€ дл€ индекс закрыв.скобки
            for (int index = 0; index < vm.Instructions.Length; index++)
            {//пробегаем по программе
                if (vm.Instructions[index] == '[') positions.Push(index);//если эл.=открыв.скобка,то кладем индекс откр.скобки в стек
                if (vm.Instructions[index] == ']')//если элемент = закрыв.скобка
                {
                    positionOpeningBracket = positions.Pop();//запоминаем позицию откр.скобки
                    positionClosingBracket = index;//запоминаем индекс закрыв.скобки
                    squareBracket.Add(positionOpeningBracket, positionClosingBracket);//добавл€ем в словарь позицию откр.скобки и индекс закр.скобки
                    squareBracket.Add(positionClosingBracket, positionOpeningBracket);//добавл€ем в словарь индекс закр.скобки и позицию откр.скобки
                }
            }
            vm.RegisterCommand('[', machine =>
            {//Ћ€мбда-выражение дл€ '['
                if (machine.Memory[machine.MemoryPointer] == 0)//если текущий байт пам€ти равен нулю
                {//ѕерескочить по программе вправо на соответствующий(с учетом вложенности) символ '['
                    machine.InstructionPointer = squareBracket[machine.InstructionPointer];
                }
            });
            vm.RegisterCommand(']', machine =>
            {//Ћ€мбда-выражение дл€ ']'
                if (machine.Memory[machine.MemoryPointer] != 0)//если текущий байт пам€ти не равен нулю
                {//ѕерескочить по списку инструкций влево на соответствующий (с учетом вложенности) символ ']'
                    machine.InstructionPointer = squareBracket[machine.InstructionPointer];
                }
            });
        }
    }
}
