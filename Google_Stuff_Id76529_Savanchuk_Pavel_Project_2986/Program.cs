using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Пространство имён пользователя
namespace Google_Stuff_Id76529_Savanchuk_Pavel_Project_2986
{
    //Класс Item  используемый нами по умолчанию
    public class Item
    {
        /// <summary>
        /// Переменная имени объекта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Переменная приоритета объекта
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Переменная типа Item для следующего объекта
        /// </summary>
        public Item Next { get; set; }
        /// <summary>
        /// Переменная типа Item для предыдущего объекта
        /// </summary>
        public Item Back { get; set; }
    }

    /// <summary>
    /// Инициализируем класс MyList для работы с пользовательскими свойствами
    /// </summary>
    public class MyList
    {
        /// <summary>
        /// Используем методы доступа get и set для свойств
        /// </summary>
        public Item List { get; set; }

        /// <summary>
        /// Инициализируем MyList как переменную
        /// </summary>
        public MyList()
        {
            List = new Item();
        }

        //добавление нового элемента по его приоритету
        //если приоритет одинаковый то первым в списке
        //окажется тот кто раньше пришёл
        public void Add(string name, int priority)
        {
            // Переход к началу списка
            ReverseBegin();
            // Задаем максимальный приоритет
            bool maxPriority = true;
            // Начинаем добавлять свойства, пока не заполнятся оба
            while (!IsEnd())
            {
                //Итерация для сдвига по списку
                List = List.Next;
                // Определяем приоритет переменной
                if (priority < List.Priority)
                {
                    // Инициализируем новый элемент очереди
                    Item local = new Item()
                    {
                        Name = name,
                        Priority = priority,
                        // Текущий элемент становится на место следующего
                        // Прошлый на место предыдущего для сохранения приоритета
                        Next = List,
                        Back = List.Back
                    };
                    // При помощи сдвигов задаем приоритеты переменных
                    List.Back.Next = local;
                    List.Back = local;
                    List = local;
                    // Снимаем максимальный приоритет
                    maxPriority = false;

                    // Выходим из цикла
                    break;
                }

                // Осуществляем проверку на возможный приоритет
                if (priority == List.Priority)
                {
                    // Итерация для свдига по очереди
                    List = List.Next;
                    // Инициализируем новый элемент очереди
                    Item local = new Item()
                    {
                        Name = name,
                        Priority = priority,
                        // Текущий элемент становится на место следующего
                        // Прошлый на место предыдущего для сохранения приоритета
                        Next = List,
                        Back = List.Back
                    };
                    // При помощи сдвигов задаем приоритеты переменных
                    List.Back.Next = local;
                    List.Back = local;
                    List = local;
                    // Снимаем максимальный приоритет
                    maxPriority = false;

                    // Выходим из цикла
                    break;
                }
            }
            //Проверяем, если пуст ли список,
            //или его приоритет самый высокий
            if (IsEmpty() || maxPriority)
            {
                AddEnd(name, priority);
            }
        }

        /// <summary>
        /// Процедура добавляет элемент в конец очереди, не учитывая приоритет
        /// </summary>
        /// <param name="name"></param>
        /// <param name="priority"></param>
        public void AddEnd(string name, int priority)
        {
            // Функция перехода к концу списка
            ReverseEnd();
            // Инициализируем переменную
            Item local = new Item()
            {
                // Задаем свойства имени и приоритета
                // Ставим его на место последнего элемента очереди
                Name = name,
                Priority = priority,
                Back = List
            };
            // При помощи сдвигов задаем приоритеты переменных
            List.Next = local;
            List = List.Next;
        }

        /// <summary>
        /// Функция для получения приоритета элемента очереди
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetPriority(string name)
        {
            // Переходим в начало очереди
            ReverseBegin();
            // Начинаем перебор очереди
            while (!IsEnd())
            {
                List = List.Next;
                if (List.Name == name)
                {
                    // Возвращаем очередь, выводя при этом очередь приоритета
                    return List.Priority;
                }
            }
            return -1;
        }

        /// <summary>
        /// Функция для получения имени элемента очереди
        /// </summary>
        /// <returns></returns>
        public string GetElements()
        {
            // Инициализируем переменную вывода
            // Переходим в начало очереди
            string output = "";
            ReverseBegin();
            // Начинаем перебор очереди
            while (!IsEnd())
            {
                List = List.Next;
                output += "\n" + List.Name + " " + List.Priority + "\n";
            }
            // Возвращаем очередь, выводя при этом очередь имен
            return output;
        }

        /// <summary>
        /// Функция удаления последнего элемента очереди
        /// </summary>
        public void DeleteEnd()
        {
            // Переходим в начало очереди
            ReverseBegin();
            // При выполнении условия, что очередь не пуста, удаляем последнего в ней
            if (!IsEmpty())
            {
                List = List.Next;
                List.Back = null;
            }
        }

        /// <summary>
        /// Функция перехода к началу очереди
        /// </summary>
        public void ReverseBegin()
        {
            // Начинаем перебор очереди
            while (List.Back != null)
            {
                List = List.Back;
            }
            // Если очередь не пуста, переходим к следующему элементу очереди
            if (List.Back != null && List.Next != null)
                List = List.Next;
        }

        /// <summary>
        /// Функция для перехода к концу очереди
        /// </summary>
        public void ReverseEnd()
        {
            // Начинаем перебор очереди
            // Если очередь не пуста переходим к следующему, пока не дойдем до последнего
            while (List.Next != null)
            {
                List = List.Next;
            }
        }

        /// <summary>
        /// Проверка булевой переменной, находимся ли мы в начале списка
        /// </summary>
        /// <returns></returns>
        public bool IsBegin()
        {
            // Проверяем, что следующая за первой переменной не пуста, и что мы не находимся на последней переменной
            return List.Back == null && List.Next != null;
        }



        /// <summary>
        /// Проверка булевой переменной, находимся ли мы в конце списка
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            // Проверяем, что следующая за текущей переменной пуста
            // чтобы убедиться, что мы на последней переменной
            return List.Next == null;
        }

        /// <summary>
        /// Проверка, что мы элемент очереди возвращает нулевое значение
        /// То есть пустой
        /// </summary>
        /// <returns></returns>
        public bool IsEnda()
        {
            return List.Back == null;
        }


        /// <summary>
        /// Проверяем, остался ли пустым список очереди
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return List.Next == null && List.Back == null && List.Priority == 0 && List.Name == null;
        }
    }

    class Program
    {
        //Статический метод по умолчанию
        static void Main(string[] args)
        {
            //Объявляем пустые строки
            string command = "";
            string name = "";
            //Объявляем две целочисленных переменных и инициализируем их нулями
            int priority = 0, result = 0;
            //Динамическое объявление объекта list класса MyList
            MyList list = new MyList();
            //Выводим на консоль приглашение пользователя
            Console.WriteLine("1. Add - Добавление пользователя");
            Console.WriteLine("2. Delete - Удаление приоритетного");
            Console.WriteLine("3. Search - Поиск по имени");
            Console.WriteLine("4. Cout - Вывод");
            Console.WriteLine("Exit - Выход");
            //Объявляем бесконечный цикл
            while (true)
            {
                //Приглашение пользователя
                Console.Write("Введите команду:");
                //Считывание с консоли строки от пользователя и помещения её в переменную или объект command
                command = Console.ReadLine();
                //Вызов оператора множественного выбора посредством сравнения
                switch (command)
                {
                    //Если значением command является "2", то выполняется этот кейс
                    case "2":
                        //Вызов нестатического метода DeleteEnd() от имени экземпляра list
                        list.DeleteEnd();
                        //Вывод на экран текста "Команда удаления выполнена успешно."
                        Console.WriteLine("Команда удаления выполнена успешно.");
                        //Завершить выполнение switch
                        break;
                    //Если значением command является "1", то выполняется этот кейс
                    case "1":
                        //Вывод на консоль слова "name:" без переноса строки
                        Console.Write("Имя: ");
                        //Присваиваем введённую пользователем в консоле строку объекту name
                        name = Console.ReadLine();
                        //Вывод на консоль слова "priority:" без переноса строки
                        Console.Write("Приоритет: ");
                        //Присваиваем введённую пользователем в консоле строку объекту priority, предварительно превратив эту строку в тип Int32
                        priority = Convert.ToInt32(Console.ReadLine());
                        //Вызов нестатического метода Add() с аргументами name, priority от имени экземпляра list
                        list.Add(name, priority);
                        //Завершить выполнение switch
                        break;
                    //Если значением command является "4", то выполняется этот кейс
                    case "4":
                        Console.Write("Очередь: " + list.GetElements());
                        //Завершить выполнение switch
                        break;
                    //Если значением command является "3", то выполняется этот кейс
                    case "3":
                        //Вывод на консоль слова "name:" без переноса строки
                        Console.Write("Имя: ");
                        //Присваиваем введённую пользователем в консоле строку объекту name
                        name = Console.ReadLine();
                        //Вызов нестатического метода GetPriority() с аргументом name и присвоение возвращённого им значения объекту result
                        result = list.GetPriority(name);
                        //Объявление и инициализация экземпляра execute класса string значением
                        //"Ничего не найдено." если условие result == -1 истинно
                        //"Найден элемент: " + name если условие result == -1 ложно
                        //Конструкция условие ? истина : лож называется тернарным оператором полного ветвления
                        string execute = result == -1 ? "Ничего не найдено." : "Найден элемент: " + name;
                        //Вывод на экран значения объекта execute
                        Console.WriteLine(execute);
                        //Завершить выполнение switch
                        break;
                    //Если значением command является "exit", то выполняется этот кейс
                    case "exit":
                        return;
                    //Если значением command является строка, которая не предусмотрена кейсами выше
                    default:
                        Console.WriteLine("Неверная команда.");
                        //Завершить выполнение switch
                        break;
                }
            }
        }
    }
}