using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{//������� ������ ��������(�����(�������� � ���������) � �����)
			char[] arrayLettersNumbers = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
			vm.RegisterCommand('.', machine => { write((char)machine.Memory[machine.MemoryPointer]);});
			//�������� �������� �� ������, �� ������� ��������� MemoryPointer.
			vm.RegisterCommand('-', machine => { unchecked { machine.Memory[machine.MemoryPointer]--; } });
			//��������� �� 1 �������� �� ������, �� ������� ��������� MemoryPointer.
			vm.RegisterCommand(',', machine => { machine.Memory[machine.MemoryPointer] = (byte)read(); });
			//������ ����� �������� � ��������� � ������, �� ������� ��������� MemoryPointer.
			vm.RegisterCommand('+', machine => { unchecked { machine.Memory[machine.MemoryPointer]++; } });
			//����������� �� 1 �������� �� ������, �� ������� ��������� MemoryPointer.
			vm.RegisterCommand('<', machine =>//��������� � ���������� ������
			{//���� ������ ��������� �� � ������ ������, �� �������� ��� �� 1 �����
				if (machine.MemoryPointer != 0) machine.MemoryPointer--;
				else machine.MemoryPointer = machine.Memory.Length - 1;
			});//����� ��������� ������ ������������� �������� � ������
			vm.RegisterCommand('>', machine =>//��������� � ��������� ������
			{//���� ������ ��������� �� � ����� ������, �� �������� ��� �� 1 ������
				if (machine.MemoryPointer != machine.Memory.Length - 1) machine.MemoryPointer++;
				else machine.MemoryPointer = 0;//���� ��������� ������ �������� 0
			});
            foreach (var constant in arrayLettersNumbers)//���������� ��������� � �������
			{//��������� �������� ������� ������� � ����������� ��� � ���� byte
				vm.RegisterCommand(constant, machine => machine.Memory[machine.MemoryPointer] = (byte)constant);
            }
        }
	}
}