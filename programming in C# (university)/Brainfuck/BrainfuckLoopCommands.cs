using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {//��������� ����������
            var squareBracket = new Dictionary<int, int>();//������� ��� ������
            var positions = new Stack<int>();//���� ��� ������� ����������� ������
            int positionOpeningBracket;//���������� ��� ������� ����.������
            int positionClosingBracket;//���������� ��� ������ ������.������
            for (int index = 0; index < vm.Instructions.Length; index++)
            {//��������� �� ���������
                if (vm.Instructions[index] == '[') positions.Push(index);//���� ��.=������.������,�� ������ ������ ����.������ � ����
                if (vm.Instructions[index] == ']')//���� ������� = ������.������
                {
                    positionOpeningBracket = positions.Pop();//���������� ������� ����.������
                    positionClosingBracket = index;//���������� ������ ������.������
                    squareBracket.Add(positionOpeningBracket, positionClosingBracket);//��������� � ������� ������� ����.������ � ������ ����.������
                    squareBracket.Add(positionClosingBracket, positionOpeningBracket);//��������� � ������� ������ ����.������ � ������� ����.������
                }
            }
            vm.RegisterCommand('[', machine =>
            {//������-��������� ��� '['
                if (machine.Memory[machine.MemoryPointer] == 0)//���� ������� ���� ������ ����� ����
                {//����������� �� ��������� ������ �� ���������������(� ������ �����������) ������ '['
                    machine.InstructionPointer = squareBracket[machine.InstructionPointer];
                }
            });
            vm.RegisterCommand(']', machine =>
            {//������-��������� ��� ']'
                if (machine.Memory[machine.MemoryPointer] != 0)//���� ������� ���� ������ �� ����� ����
                {//����������� �� ������ ���������� ����� �� ��������������� (� ������ �����������) ������ ']'
                    machine.InstructionPointer = squareBracket[machine.InstructionPointer];
                }
            });
        }
    }
}
