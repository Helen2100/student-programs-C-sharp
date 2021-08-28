using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }//свойство=хранится сама программа действий
		public int InstructionPointer { get; set; }//свойство-указывает на текущий индекс элемента в Instruction
		public byte[] Memory { get; }//свойство-байты памяти
		public int MemoryPointer { get; set; }//свойство-указывает на элемент из Memory

		Dictionary<char, Action<IVirtualMachine>> CommandDictionary = new Dictionary<char, Action<IVirtualMachine>>();//словарь команд

		public VirtualMachine(string program, int memorySize)
		{//присваиваем значения свойствам
			Instructions = program;//саму программу
			Memory = new byte[memorySize];//задаем размер массива памяти
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			CommandDictionary.Add(symbol, execute);//заполняем словарь команд
		}

		public void Run()
		{
			var programLength = Instructions.Length;//длина программы действий
			while (InstructionPointer < programLength)//пока мы находимся внутри программы
            {
                var commandName = Instructions[InstructionPointer];//достаем команду из программы
                if (CommandDictionary.ContainsKey(commandName))//если в словаре есть такой ключ
                    CommandDictionary[commandName](this);//то выполняем данную команду
                InstructionPointer++;//перемещаем указатель
            }
        }
	}
}
