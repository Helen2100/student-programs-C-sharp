using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }//создаем стек
        public int Limit;//создаем переменную для указания лимита стека
        private LimitedSizeStack<Tuple<string, int, TItem>> historyOperations;//создаем дженерик для записи вызовов(история)

        /// <summary>
        /// метод присваивает глобальным переменным значения
        /// </summary>
        /// <param name="limit"> лимит исходного стека </param>
        public ListModel(int limit)
        {
            Items = new List<TItem>();//основной стек
            Limit = limit;//лимит
            historyOperations = new LimitedSizeStack<Tuple<string, int, TItem>>(limit);//дженерик для записи истории операций
        }

        /// <summary>
        /// метод вставляет переменную в конец стека и запоминает эту операцию
        /// (метод добавляет в "history" название операции, место, где произошла операция,
        /// и значение переменной, с которой произошла операция, )
        /// </summary>
        /// <param name="item"> переменная, которую нужно вставить в стек </param>
        public void AddItem(TItem item)
        {
            historyOperations.Push(new Tuple<string, int, TItem>("insert", 0, item));//название операции, индекс элемента, к которому применена операция, и значение элемента
            Items.Add(item);
        }

        /// <summary>
        /// метод заменяет переменную в определенном месте в стеке и запоминает эту операцию
        /// (метод добавляет в "history" название операции, место, где произошла операция,
        /// и значение переменной, с которой произошла операция)
        /// </summary>
        /// <param name="index"> место в стеке, в котором нужно удалить переменную </param>
        public void RemoveItem(int index)
        {
            historyOperations.Push(new Tuple<string, int, TItem>("detele", index, Items[index]));//название операции, индекс элемента, к которому применена операция, и значение элемента
            Items.RemoveAt(index);
        }

        /// <summary>
        /// Метод CanUndo() является проверкой коррекности метода Undo
        /// </summary>
        /// <returns> Возвращает true, если на данный момент история действий не пуста. Иначе метод должен вернуть false </returns>
        public bool CanUndo()
        {
            return historyOperations.Count != 0;
        }

        /// <summary>
        /// Метод Undo отменяет последнее действие из истории.
        /// </summary>
        public void Undo()
        {
            if (CanUndo())//если история не пустая
            {
                var recentOperation = historyOperations.Pop();//запоминаем последнию операцию
                if (recentOperation.Item1 == "insert")//если это вставка
                    Items.RemoveAt(Items.Count - 1);//удаляем вставленный элемент
                else
                    Items.Insert(recentOperation.Item2, recentOperation.Item3);//вставляем удаленный элемент
            }
            else throw new InvalidOperationException();//то возвратить сообщение об ошибке
        }
    }
}