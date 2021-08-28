using System;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private List<Clone> cloneList = new List<Clone>();//лист для клона
        /// <summary>
        /// принимает на вход описание клона (операцию, номер клона и значение клона)
        /// </summary>
        /// <param name="query"> описание клона </param>
        /// <returns> Вернуть программу, которую клон владеет и при этом усвоил последней </returns>
        public string Execute(string query)
        {
            if (cloneList.Count == 0) cloneList.Add(new Clone());//присваиваем начальное значение листу
            var commandLine = query.Split(' ');//разбивает строчку описания клона на массив
            var numberClone = int.Parse(commandLine[1]) - 1;//номер клона

            switch (commandLine[0])//команда для клона
            {
                case "check"://проверка последней усвоинной программы
                    return cloneList[numberClone].Check();
                case "learn"://присваиваем значение клону
                    cloneList[numberClone].Learn(commandLine[2]);
                    break;
                case "rollback"://Откатить последнюю программу у клона с определенным номером
                    cloneList[numberClone].Rollback();
                    break;
                case "relearn":// Переусвоить последний откат у клона с определенным номером
                    cloneList[numberClone].Relearn();
                    break;
                case "clone"://Клонировать клона с определенным номером
                    cloneList.Add(new Clone(cloneList[numberClone]));
                    break;
            }
            return null;//иначе вернуть пустую строку
        }
    }
    public class Program<T>
    {
        public T Value { get; set; }
        public Program<T> Next { get; set; }
        public Program<T> Previous { get; set; }
    }

    public class Stack<T>
    {
        public Program<T> Head;//начало стека
        public Program<T> Tail;//конец стека

        /// <summary>
        /// складывает в конец стека переданную переменную
        /// </summary>
        /// <param name="value"> переменная, которую нужно положить в конец стека </param>
        public void Push(T value)
        {
            if (Head == null)
                Tail = Head = new Program<T> { Value = value, Next = null, Previous = null };
            
            else
            {
                var program = new Program<T> { Value = value, Next = null, Previous = Tail };
                Tail.Next = program;
                Tail = program;
            }
        }
        /// <summary>
        /// находит значение последней переменной в стеке
        /// </summary>
        /// <returns> возвращает значение последней переменной в стеке </returns>
        public T Pop()
        {
            if (Tail == null) throw new InvalidOperationException();
            var program = Tail.Value;
            Tail = Tail.Previous;
            if (Tail == null)
                Head = null;
            else
                Tail.Next = null;
            return program;
        }
    }
    public class Clone
    {
        private Stack<string> LearnedPrograms;//выученные программы
        private Stack<string> ForgottenPrograms;//забытые программы

        public Clone()
        {
            LearnedPrograms = new Stack<string>();//присваиваем начальное значение переменной
            ForgottenPrograms = new Stack<string>();//присваиваем начальное значение переменной
        }
        /// <summary>
        ///  Клонирует клона
        /// </summary>
        /// <param name="clone"> клон, который нужно клонировать </param>
        public Clone(Clone clone)
        {
            LearnedPrograms = new Stack<string>()
                { Head = clone.LearnedPrograms.Head, Tail = clone.LearnedPrograms.Tail };
            ForgottenPrograms = new Stack<string>() 
                { Head = clone.ForgottenPrograms.Head, Tail = clone.ForgottenPrograms.Tail };
        }
        /// <summary>
        /// Обучает клона по программе
        /// </summary>
        /// <param name="command"> программа, которую клон должен выучить </param>
        public void Learn (string command)
        {
            LearnedPrograms.Push(command);
            ForgottenPrograms = new Stack<string>();
        }
        /// <summary>
        /// Возращает последнюю выученную программу
        /// </summary>
        /// <returns> Вернуть программу, которой клон владеет и при этом усвоил последней.
        /// Если клон владеет только базовыми знаниями, верните "basic" </returns>
        public string Check ()
        {
            if (LearnedPrograms.Head != null)
            {
                var temp = LearnedPrograms.Pop();
                LearnedPrograms.Push(temp);
                return temp;
            }
            return "basic";
        }
        /// <summary>
        /// Откатывает последнюю программу у клона
        /// </summary>
        public void Rollback ()
        {
            var lastLearnedProgram = LearnedPrograms.Pop();
            ForgottenPrograms.Push(lastLearnedProgram);
        }
        /// <summary>
        /// Переусваивает последний откат у клона
        /// </summary>
        public void Relearn ()
        {
            var lastForgottenPrograms = ForgottenPrograms.Pop();
            LearnedPrograms.Push(lastForgottenPrograms);
        }
    }
}

