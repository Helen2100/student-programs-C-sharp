using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, IMagic
    {//создаем глобальные переменные и присваиваем им значения
        Segment segment;//для сегменты
        Document document;//для документа
        Cat cat = new Cat("Анфиса", "Сибирская", DateTime.Now);//для кошки(имя, порода, время рождения)
        Vector vectorFirst = new Vector(100, -8456);//для вектора и сектора
        Vector vectorEnd = new Vector(100, -8456);//для сектора
        Robot robot = new Robot("Е-980-СН");//для робота(id)
        byte[] array = { 8, 2, 11, 22, 77 };//для текста документа
        public GhostsTask()
        {//присваиваем значения переменым (документ и сегмент)
            document = new Document("Homework", Encoding.UTF8, array);//название, кодирование, текст
            segment = new Segment(vectorFirst, vectorEnd);//два вектора, которые задают границы сегмента
        }

        public void DoMagic()
        {//меняем значения переменных
            Robot.BatteryCapacity--;//меняем значение аккомулятора для робота
            vectorFirst.Add(new Vector(2, 28));//меняем значение первого вектора
            cat.Rename("Лёлик");//меняем имя кошки
            array[4] = 111;//меняем значение текста
        }
        //даольше описаны методы, которые возвращают выше перечисленные переменные
        Segment IFactory<Segment>.Create()
        {
            return segment;
        }
        Vector IFactory<Vector>.Create()
		{
			return vectorFirst;
		}		
        Document IFactory<Document>.Create()
        {
            return document;
        }
        Robot IFactory<Robot>.Create()
        {
            return robot;
        }
        Cat IFactory<Cat>.Create()
        {
			return cat;
        }  
    }
}