using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Employers
{
    class Program
    {
        static void Main(string[] args)
        {           
            Console.Write($"Доступные команды: \n" +
                          $"add name address phone - для добавления работодателя, где name наименование, address - адрес, phone - телефон. В качестве разделителя используейте tab; \n" +
                          $"sort - сортировка в алфавитном порядке; \n" +
                          $"show - вывод всего списка работодателей; \n" +
                          $"delete i - удаление по индексу, где i - это индекс (число); \n" +
                          $"read path - считать данные из файла, path - путь к файлу, разделитель между значениями tab; \n" +
                          $"write path - записать данные в файл, path - путь к файлу, разделитель между значениями tab; \n" +
                          $"change path lineIdex colIndex value - записать данные в файл, path - путь к файлу, разделитель между значениями tab, lineIdex - индекс строки, colIndex - индекс столбца, value - значение; \n" +
                          $"clean - очистка всего списка; \n" +
                          $"fromjson path - создать пользователя из json;" +
                          $"tojson - записать информацию о пользователя в json.\n\n");

            List<Employer> employers = new List<Employer>();

            while (true)
            {
                var data = Console.ReadLine();
                if (String.IsNullOrEmpty(data))
                {
                    continue;
                }
                else
                {
                    var command = data.Split()[0].ToLower();
                    var dataWithoutCommand = data.Remove(0, data.IndexOf(' ') + 1);

                    switch (command)
                    {
                        case "read":
                            using (StreamReader reader = new StreamReader(dataWithoutCommand, System.Text.Encoding.Default))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    var employerInfo = line.Split('\t');
                                    var employer = new Employer(employerInfo[0]);
                                    employer.Addres = employerInfo.Length > 1
                                                        ? employerInfo[1]
                                                        : "";
                                    employer.Phone = employerInfo.Length > 2
                                                        ? employerInfo[2]
                                                        : "";
                                    employers.Add(employer);
                                }
                            }
                            Console.WriteLine("Считаны данные о работодателях из файла " + dataWithoutCommand + "\n");
                            Print(employers);
                            Console.WriteLine();
                            break;
                        case "write":
                            using (StreamWriter writer = new StreamWriter(dataWithoutCommand, false, System.Text.Encoding.Default))
                            {
                                foreach (var employer in employers)
                                {
                                    writer.WriteLine(employer.ToString());
                                }                             
                            }
                            Console.WriteLine("Данные о работодателях записаны в файл " + dataWithoutCommand + "\n");
                            break;
                        case "change":
                            var path = dataWithoutCommand.Split()[0];
                            var lineIndex = Int32.Parse(dataWithoutCommand.Split()[1]);
                            var colIndex = Int32.Parse(dataWithoutCommand.Split()[2]);
                            var value = dataWithoutCommand.Split()[3];
                            int i = 0;
                            List<string> fileData = new List<string>();
                            using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default))
                            {
                                string line;                                
                                while ((line = reader.ReadLine()) != null)
                                {
                                    if (i == lineIndex)
                                    {
                                        var changeableData = line.Split('\t');
                                        changeableData[colIndex] = value;
                                        line = string.Join("\t", changeableData);
                                    }
                                    fileData.Add(line);
                                    i++;
                                }
                            }
                            using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Default))
                            {
                                foreach (var str in fileData)
                                {
                                    writer.WriteLine(str);
                                }
                            }
                            Console.WriteLine("Изменены данные в файле " + path + "\n");
                            break;
                        case "add":
                            var info = dataWithoutCommand.Split('\t');
                            var name = info.Length > 0
                                        ? info[0]
                                        : "";
                            var address = info.Length > 1
                                        ? info[1]
                                        : "";
                            var phone = info.Length > 2
                                        ? info[2]
                                        : "";
                            employers.Add(new Employer(name, address, phone));
                            Console.WriteLine("Добавлен Работодатель " + dataWithoutCommand + "\n");
                            break;
                        case "delete":
                            var index = Int32.Parse(dataWithoutCommand);
                            if (index < employers.Count() && index >= 0)
                            {
                                employers.RemoveAt(index);
                                Console.WriteLine("Удален Работодатель под индексом " + dataWithoutCommand + "\n");
                                Print(employers);
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("Ошибка, в списке меньше элементов (" + employers.Count() + ")\n");
                            }
                            break;
                        case "show":
                            Console.WriteLine("Работодатели:");
                            Print(employers);
                            Console.WriteLine();
                            break;
                        case "sort":
                            employers.Sort(Employer.CompareByName);
                            Console.WriteLine("Список отсортирован в алфавитном порядке:");
                            Print(employers);
                            Console.WriteLine();
                            break;
                        case "clean":
                            employers.Clear();
                            Console.WriteLine("Список очищен" + "\n");
                            break;
                        case "fromjson":
                            using (StreamReader reader = new StreamReader(dataWithoutCommand, System.Text.Encoding.UTF8))
                            {
                                var jsonStr = reader.ReadToEnd();
                                var userFromJson = addUserFromString(jsonStr);
                            }
                            break;
                        case "tojson":
                            using (StreamWriter writer = new StreamWriter(dataWithoutCommand, false, System.Text.Encoding.UTF8))
                            {
                                var user = new User("ivanov", "s12qt4", "ivanov@gmail.com", "user");
                                var jsonFromUser = addUserToString(user);
                                writer.WriteLine(jsonFromUser);
                            }
                            break;
                        default:
                            Console.WriteLine("Команда не найдена" + "\n");
                            break;
                    }
                }
            }
        }

        static public void Print(List<Employer> data)
        {
            foreach (var item in data)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static public User addUserFromString(string jsonString)
        {
            return JsonSerializer.Deserialize<User>(jsonString);
        }

        static public string addUserToString(User user)
        {
           return JsonSerializer.Serialize(user);
        }


    }
}
