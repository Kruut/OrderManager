namespace OrderManager
{
    public class FileManager
    {
        public static void ConfigureDistrictsManager(string path)
        {
            if (File.Exists(path))
            {
                Logger.GetLogger().Debug("Файл со списком районов найден");
                try
                {
                    var districtsString = File.ReadAllText(path);
                    Logger.GetLogger().Debug("Файл со списком районов считан успешно");
                    var rawDistricts = districtsString.Split();

                    var districtList = rawDistricts
                        .Select(x => Convert.ToInt32(x))
                        .ToList();
                    if (districtList.Count == 0)
                    {
                        Logger.GetLogger().Warn("Был получен пустой список районов, Любой id района, соответствующий типу, будет считаться валидным.");
                        return;
                    }
                    DistrictsManager<int>.SetDistrictsList(districtList);
                }
                catch (Exception ex)
                {
                    Logger.GetLogger().Warn("Ошибка при обработке списка районов", ex);
                }
            }
            else
            {
                Logger.GetLogger().Debug("Файл со списком районов не найден. Любой id района, соответствующий типу, будет считаться валидным");
            }
        }

        public static void GenerateOrdersToFile(string path, int orderCount = 1000)
        {
            var orders = new List<Order>();
            for (int i = 0; i < orderCount; i++)
                orders.Add(Order.GenerateOrder());
            File.WriteAllText(path, string.Join("\n;\n", orders));

        }

        public static string ReadOrderFile(string path)
        {
            if (!File.Exists(path))
            {
                var message = $"Файл {path} не найден";
                Console.WriteLine(message);
                Logger.GetLogger().Error(message);
                return null;
            }

            try
            {
                //Чтение файла
                var result = File.ReadAllText(path);
                Logger.GetLogger().Debug("Файл успешно считан");
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Ошибка при работе с файлом {path}";
                Console.WriteLine(message);
                Logger.GetLogger().Error(message, ex);
                return null;
            }
        }
    }
}
