using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }//��������=�������� ���� ��������� ��������
		public int InstructionPointer { get; set; }//��������-��������� �� ������� ������ �������� � Instruction
		public byte[] Memory { get; }//��������-����� ������
		public int MemoryPointer { get; set; }//��������-��������� �� ������� �� Memory

		Dictionary<char, Action<IVirtualMachine>> CommandDictionary = new Dictionary<char, Action<IVirtualMachine>>();//������� ������

		public VirtualMachine(string program, int memorySize)
		{//����������� �������� ���������
			Instructions = program;//���� ���������
			Memory = new byte[memorySize];//������ ������ ������� ������
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			CommandDictionary.Add(symbol, execute);//��������� ������� ������
		}

		public void Run()
		{
			var programLength = Instructions.Length;//����� ��������� ��������
			while (InstructionPointer < programLength)//���� �� ��������� ������ ���������
            {
                var commandName = Instructions[InstructionPointer];//������� ������� �� ���������
                if (CommandDictionary.ContainsKey(commandName))//���� � ������� ���� ����� ����
                    CommandDictionary[commandName](this);//�� ��������� ������ �������
                InstructionPointer++;//���������� ���������
            }
        }
	}
}
