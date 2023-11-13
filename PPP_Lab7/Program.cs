
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using static PPP_Lab9.RatingSystemExtentions;

namespace PPP_Lab9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //User User1 = new(1, "Alice", "Cather", 25);
            //User User2 = new(2, "Bob", "Bracker", 30);

            var ratingSystem = SerializeOrDeserialize.JsonDeserialize("output.json");

            //SerializeOrDeserialize.JsonSerialize(ratingSystem, "output.json");

            //SerializeOrDeserialize.XmlSerialize(ratingSystem, "output.xml");

            //var ratingSystem1 = SerializeOrDeserialize.XmlDeserialize("output.xml");

            //SerializeOrDeserialize.BinarySerialize(ratingSystem1, "output.bin");

            //var ratingSystem2 = SerializeOrDeserialize.BinaryDeserialize("output.bin");



            //так как у меня Dictionary то отсортированные значения после добавления в Dictionary снова становяться неотсортированными. Такие особенности словаря
            //Поэтому я сделал возврат листа из значений словаря, который и будет средством вывода отсортированного словаря пользователей


            Console.WriteLine("До изменений:");
            Console.WriteLine(ratingSystem);

            var ratingList = RatingSystemExtentions.Sort(ratingSystem, User.OrderByFirstNameLeft);
            Console.WriteLine("После сортировки по имени в алфавитном порядке:");
            consolePrintListOfKeyValuePair(ratingList);

            ratingList = RatingSystemExtentions.Sort(ratingSystem, User.OrderByDescendingNameLeft);
            Console.WriteLine("После сортировки по имени в обратном алфавитному порядке:");
            consolePrintListOfKeyValuePair(ratingList);

            ratingList = RatingSystemExtentions.Sort(ratingSystem, User.OrderByIdLeft);
            Console.WriteLine("После сортировки по id на возростание:");
            consolePrintListOfKeyValuePair(ratingList);

            ratingList = RatingSystemExtentions.Sort(ratingSystem, User.OrderByIdDescendingIdLeft);
            Console.WriteLine("После сортировки по іd на убывание:");
            consolePrintListOfKeyValuePair(ratingList);

            //использование лямбда-выражений
            ratingList = RatingSystemExtentions.Sort(ratingSystem, (left, right) => left.Age > right.Age);
            Console.WriteLine("После сортировки по возрасту от меньшего к большему:");
            consolePrintListOfKeyValuePair(ratingList);

            //использование анонимного метода
            CompareDelegate orderByDescendingAgeLeft = delegate (User left, User right)
            {
                return left.Age < right.Age;
            };

            ratingList = RatingSystemExtentions.Sort(ratingSystem, orderByDescendingAgeLeft);
            Console.WriteLine("После сортировки по возрасту от большего к меньшему:");
            consolePrintListOfKeyValuePair(ratingList);

            ratingList = RatingSystemExtentions.Search(ratingSystem,"Al", User.SearchByFirstName);
            Console.WriteLine("Поиск по имени:");
            consolePrintListOfKeyValuePair(ratingList);
        }

        /// <summary>
        /// Выводит список пар ключ-значение на консоль.
        /// </summary>
        /// <param name="ratingList">Список пар ключ-значение для вывода.</param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void consolePrintListOfKeyValuePair(List<KeyValuePair<User, int>> ratingList)
        {
            if (ratingList != null)
            {
                foreach (var item in ratingList)
                {
                    Console.WriteLine($"User Info:  Id: {item.Key.Id,-3}\tFirst Name: {item.Key.FirstName,-10}\tLast Name: {item.Key.LastName,-10}\tAge: {item.Key.Age,-3}\tRating: {item.Value,-3}");
                }
            }
            else
            {
                throw new ArgumentNullException("ratingList должен быть не null");
            }
        }
    }
}