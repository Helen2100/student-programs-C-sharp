using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        List<T> Commands = new List<T>();
        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        public T this[int index]
        {//свойство
            get//возвращаем значение в зависимости от условия
            {//прописываем оператор lock для стабильности программы
                lock (Commands)//блокируем переменную пока работает данный кусок кода
                {
                    if (Commands.Count > index && index >= 0)//если индекс принадлежит нашему листу 
                        return Commands[index];//то возвращаем элемент по индексу
                    return null;//иначе возвращаем null
                }
            }
            set//устанавливаем значение
            {//прописываем оператор lock для стабильности программы
                lock (Commands)//блокируем переменную пока работает данный кусок кода
                {
                    if (Commands.Count > index && index >= 0)//если индекс принадлежит нашему листу 
                    {
                        Commands[index] = value;//запоминаем значение переменной
                        Commands.RemoveRange(index + 1, Commands.Count - index - 1);//удаляем все элементы после
                    }
                    else if (index == Commands.Count)//Если индекс в точности равен размеру коллекции
                        Commands.Add(value);//добавляем элемент
                }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {//прописываем оператор lock для стабильности программы
            lock (Commands)//блокируем переменную пока работает данный кусок кода
            {
                if (Commands.Count != 0)//если список не пустой
                    return Commands.Last();//то возвращаем последний элемент
                return null;//иначе возвращаем null
            }
        }


        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {//прописываем оператор lock для стабильности программы
            lock (Commands)//блокируем переменную пока работает данный кусок кода
            {
                if (LastItem() == knownLastItem)//если данная переменная является последней
                    Commands.Add(item);//то добавляем переменную в конец
            }
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count
        {//свойство
            get//возвращаем значение 
            {//прописываем оператор lock для стабильности программы
                lock (Commands)//блокируем переменную пока работает данный кусок кода
                {
                    return Commands.Count;//Возвращаем количество элементов в коллекции
                }
            }
        }
    }
}