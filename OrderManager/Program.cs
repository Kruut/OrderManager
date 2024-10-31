using OrderManager;

[assembly: log4net.Config.XmlConfigurator()]

//Создание объекта типа ArgParser с передачей аргументов от Main для дальнейшего обращения к обработанным данным
var argParser = new ArgParser(args);
//Переменная списка заказов
var orders = new List<Order>();

//Извлечение данных из файла с проверкой на ошибки и записи их в коллекцию
var readOrders = FileManager.ReadOrderFile(argParser.InputDeliveryOrderFilePath);

//Проверка на пустой файл
if (string.IsNullOrEmpty(readOrders))
{
    var message = $"Файл был считан успешно, но не содержал данных";
    Console.WriteLine(message);
    Logger.GetLogger().Error(message);
    return;
}

FileManager.ConfigureDistrictsManager(argParser.DistrictsListFilePath);

//Получение разделенных на отдельные заказы строк
var stringOrders = readOrders.Split(';');

//Форматирование входных данных и формирование коллекции заказов в виде объектов класса Order
foreach (var stringOrder in stringOrders)
{
    //Разделение строки на конкретные значения атрибутов
    var orderAttributesStrings = stringOrder.Split(["id ", "weight ", "district ", "time "], StringSplitOptions.RemoveEmptyEntries).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    try
    {
        if (orderAttributesStrings.Count == 4)
            //Добавление заказа в коллекцию
            orders.Add(new Order(orderAttributesStrings));
        else
        {
            var message = $"Некорректно введен заказ:\n{string.Join("\n", orderAttributesStrings)} \n";
            Console.WriteLine(message);
            Logger.GetLogger().Error(message);
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
        Logger.GetLogger().Error(ex.Message, ex);
    }
}

//Получение аргументов через argParser (либо из параметров командной строки, либо из консоли)
argParser.GetInput(out var cityDistrict, out var firstDeliveryDateTime);

string sortedOrdersString = "";

//Фильтрация списка и запись результата в файл для вывода (путь получается через argParser)
try
{
    Logger.GetLogger().Debug($"Вызов фильтрации списка заказов, элементов в коллекции до фильтрации: {orders.Count}");
    var filteredOrders = FilterOrderListByDistrictAndTime(orders, cityDistrict, firstDeliveryDateTime);
    sortedOrdersString = string.Join("\n\n", filteredOrders);
    Logger.GetLogger().Debug($"Фильтрация завершена успешно, элементов в результирующей коллекции: {filteredOrders.Count}");

    if (string.IsNullOrEmpty(sortedOrdersString))
        File.WriteAllText(argParser.DeliveryOrderFilePath, "Нет подходящих заказов");

}
catch (Exception ex)
{
    var message = $"Произошла ошибка при фильтрации заказов: {ex.Message}";
    Console.WriteLine(message);
    Logger.GetLogger().Error(ex.Message, ex);
}

//Вывод отсортированных заказов в файл
try
{
    File.WriteAllText(argParser.DeliveryOrderFilePath, sortedOrdersString);
    Logger.GetLogger().Debug($"Список записан в файл {argParser.DeliveryOrderFilePath}");

    Console.WriteLine($"\nЗаказы успешно отсортированы и записаны в файл {argParser.DeliveryOrderFilePath}");
}
catch (Exception ex)
{
    var message = $"Произошла ошибка при записи отфильтрованного списка в файл: {ex.Message}";
    Console.WriteLine(message);
    Logger.GetLogger().Error(ex.Message, ex);
}

//Фильтрация коллекции по времени и id района (лямбда выражение передается в метод Where библиотеки Linq)
static List<Order> FilterOrderListByDistrictAndTime(List<Order> orders, int cityDistrict, DateTime firstDeliveryDateTime) =>
    orders.Where((x) => (x.DistrictId == cityDistrict) && (firstDeliveryDateTime.AddMinutes(30) >= x.Time) && (firstDeliveryDateTime <= x.Time)).ToList();