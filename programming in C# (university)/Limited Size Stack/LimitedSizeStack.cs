using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private LinkedList<T> list = new LinkedList<T>();//двусвязный список = стек
        private int sizeList;//макс размер стека(чтобы не запутаться в дальнейшем коде)
        public int Count { get; set; }//возращает текущее кол-во элементов в стеке(счетчик)

        public LimitedSizeStack(int limit)
        {
            sizeList = limit;//заносим значение в переменную
        }

        public void Push(T item)
        {
            if (sizeList > 0)//по условию
            {
                if (list.Count == sizeList)//по условию
                {
                    list.RemoveFirst();//удаляем первую переменную со смещеннием
                    Count--;//уменьшаем кол-во эл на 1
                }
                list.AddLast(item);//добавляем переменную в конец стека 
                Count++;//увеличиваем счетчик на 1
            }
        }

        public T Pop()
        {
            if (list.Count == 0) throw new NotImplementedException();//ошибка, т.к стек не заполнен
            var lastElementList = list.Last.Value;//"вынимает" последний элемент из стека
            list.RemoveLast();//удаляем его из стрека
            Count--;//вычитаем 1 из счетчика
            return lastElementList;//возвращаем послений элемент стека
        }
    }
}