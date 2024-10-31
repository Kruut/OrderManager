using OrderManager;
using System;

try
{
    FileManager.ConfigureDistrictsManager("districts.txt");
    FileManager.GenerateOrdersToFile("orders.txt",10000);
    Console.WriteLine("Файл заказов сгенерирован");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка при генерации файла: {ex.Message}");
}