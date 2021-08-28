using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable
	{//создаем глобальные переменные
		private byte[] byteArray;//для заданных чисел
		private int hash = -1;//для хэш кода

		public ReadonlyBytes(params byte[] givenArray)
		{
			if (givenArray == null) throw new ArgumentNullException();//проверяем на корректность вводимых данных
			byteArray = new byte[givenArray.Length];//задаем размер массива
			for (int i = 0; i < givenArray.Length; i++)//перебераем заданные переменные
				byteArray[i] = givenArray[i];//добавляем их в созданный массив 
			hash = GetHashCode();//вызываем метод, который вычисляем хэш код
		}

		public byte this[int index]
		{//метод который возращает элемент по индексу
			get
			{//проверка на корректность работы программы
				if (index >= byteArray.Length || index < 0) throw new IndexOutOfRangeException();
				return byteArray[index];//возращяем iый элемент
			}
		}

		public int Length => byteArray.Length;//метод, который возращает длину массива
		//реализуем интерфейс IEnumerable
		public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)byteArray).GetEnumerator();
		//метод, который возвращает перечислитель, осуществляющий итерацию по коллекции
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object array)
        {
			if (array == null) return false;//ложь, если массив байтов ничего в себе не содержит
            if (array == this) return true;//правда, если элементы массивов равны между собой
			if (array.GetType() != GetType()) return false;//ложь, если у массивов не совпадают типы переменных
			ReadonlyBytes newTypeArray = (ReadonlyBytes)array;//меняем тип массива для дальнейших действий
			if (!(array is ReadonlyBytes) || byteArray.Length != newTypeArray.Length) 
				return false;//ложь, если не тот тип переменной или если длина массивов не совпадают
			return !byteArray//проверка на совпадение массивов (по элементам)
				.Where((t, i) => t != newTypeArray[i])//фильтруем по не совпадению эл.
				.Any();//проверка на содержание эл в массиве
        }
        public override int GetHashCode()
		{//метод возвращает хэш-код(=ХК) массива
			unchecked
			{//позволяет предотвратить проверку переполнения при выполнении арифметических операций
				if (hash == -1)//ХК = нач.знач.
				{
					hash = 1;//меняем значения (для умножения)
					foreach (var number in byteArray)//перебираем числа в массиве
					{
						hash *= 658;//умножаем ХК на рандомное большое число
						hash += number;//прибавляем к ХК число
					}
					return hash;//возвращяем ХК
				}//если мы уже работали с ХК, то возращаем изм.знач.ХК
				return hash;
			}
		}
		public override string ToString()
		{
			string result = "[";//добавляем элемент, которым строка начинается
			if (byteArray.Length != 0)//если есть числа
			{
				for (int i = 0; i < byteArray.Length; i++)//перебираем числа
				{
					if (i < byteArray.Length - 1)//если это не последний элемент
						result += byteArray[i] + ", ";// добавляем числа в переменную
					else result += byteArray[i];// добавляем числа в переменную
				}
            }
			return result += "]";//добавляем элемент, которым строка заканчивается
        }
    }
}