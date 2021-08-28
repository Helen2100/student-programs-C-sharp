using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{//создаем массив констант(буквы(строчные и заглавные) и цифры)
			char[] arrayLettersNumbers = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
			vm.RegisterCommand('.', machine => { write((char)machine.Memory[machine.MemoryPointer]);});
			//ѕечатает значение из €чейки, на которую указывает MemoryPointer.
			vm.RegisterCommand('-', machine => { unchecked { machine.Memory[machine.MemoryPointer]--; } });
			//”меньшает на 1 значение из €чейки, на которую указывает MemoryPointer.
			vm.RegisterCommand(',', machine => { machine.Memory[machine.MemoryPointer] = (byte)read(); });
			//¬водит извне значение и сохран€ет в €чейку, на которую указывает MemoryPointer.
			vm.RegisterCommand('+', machine => { unchecked { machine.Memory[machine.MemoryPointer]++; } });
			//”величивает на 1 значение из €чейки, на которую указывает MemoryPointer.
			vm.RegisterCommand('<', machine =>//ѕереходит к предыдущей €чейке
			{//если курсор находитс€ не в начале строки, но сместить его на 1 влево
				if (machine.MemoryPointer != 0) machine.MemoryPointer--;
				else machine.MemoryPointer = machine.Memory.Length - 1;
			});//иначе присвоить €чейке предпоследнее значение в пам€ти
			vm.RegisterCommand('>', machine =>//ѕереходит к следующей €чейке
			{//если курсор находитс€ не в конце строки, то сместить его на 1 вправо
				if (machine.MemoryPointer != machine.Memory.Length - 1) machine.MemoryPointer++;
				else machine.MemoryPointer = 0;//инче присвоить €чейке значение 0
			});
            foreach (var constant in arrayLettersNumbers)//перебираем константы в массиве
			{//сохран€ет значение каждого символа с приведением его к типу byte
				vm.RegisterCommand(constant, machine => machine.Memory[machine.MemoryPointer] = (byte)constant);
            }
        }
	}
}