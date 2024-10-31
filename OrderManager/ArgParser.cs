namespace OrderManager
{
    internal class ArgParser
    {
        private const string _cityDistrictArgName = "_cityDistrict";

        private const string _firstDeliveryDateTimeArgName = "_firstDeliveryDateTime";

        private const string _deliveryOrderArgName = "_deliveryOrder";

        private const string _inputDeliveryOrderArgName = "_inputDeliveryOrder";

        private const string _districtsListArgName = "_districtsList";

        private readonly IDictionary<string, string> Arguments = new Dictionary<string, string>();

        /// <summary>
        /// Атрибут для получения входного списка заказов.
        /// Инкапсулирует алгоритмы получения самого аргумента, возвращая вычисляемое значение (либо переданное в параметрах командной строки, либо значение по умолчанию).
        /// </summary>
        /// Для получения значения из командной строки обращается к атрибуту Arguments, хранящему входные аргументы как ключ-значение
        public string InputDeliveryOrderFilePath => Arguments.ContainsKey(_inputDeliveryOrderArgName) 
            ? Arguments[_inputDeliveryOrderArgName] 
            : "orders.txt";

        /// <summary>
        /// Атрибут для получения результирующего списка заказов.
        /// Инкапсулирует алгоритмы получения самого аргумента, возвращая вычисляемое значение (либо переданное в параметрах командной строки, либо значение по умолчанию).
        /// </summary>
        /// Для получения значения из командной строки обращается к атрибуту Arguments, хранящему входные аргументы как ключ-значение
        public string DeliveryOrderFilePath => Arguments.ContainsKey(_deliveryOrderArgName) 
            ? Arguments[_deliveryOrderArgName] 
            : "sortedOrders.txt";

        /// <summary>
        /// Атрибут для получения списка существующих районов.
        /// Инкапсулирует алгоритмы получения самого аргумента, возвращая вычисляемое значение (либо переданное в параметрах командной строки, либо значение по умолчанию).
        /// </summary>
        /// Для получения значения из командной строки обращается к атрибуту Arguments, хранящему входные аргументы как ключ-значение
        public string DistrictsListFilePath => Arguments.ContainsKey(_districtsListArgName) ? Arguments[_districtsListArgName] : "districts.txt";
        
        /// <summary>
        /// Конструктор ArgParser, принимающий аргументы командной строки. Аргументы будут преобразованы в словарь, что позволит обращаться к ним в безопасном виде.
        /// </summary>
        /// <param name="args">Аргументы командной строки в формате "key"="value", пара должна быть одним элементом массива для корректного распознания</param>
        public ArgParser(string[] args)
        {

            foreach (string arg in args)
            {
                var argTokens = arg.Split("=");
                if (argTokens.Length == 2)
                {
                    Arguments[argTokens[0]] = argTokens[1];
                }
            }
        }
        /// <summary>
        /// Метод для получения входных параметров для фильтрации заказов.
        /// Возвращает значания, переданные через командную строку (если они есть), либо запрашивает их через консоль (если их нет)
        /// Для получения нескольких значений в результате работы метода используются выходные параметры.
        /// </summary>
        /// <param name="cityDistrict">Выходной параметр, содержащий id района</param>
        /// <param name="firstDeliveryDateTime">Выходной параметр, содержащий время первого заказа</param>
        public void GetInput(out int cityDistrict, out DateTime firstDeliveryDateTime)
        {
            if ((!Arguments.ContainsKey(_cityDistrictArgName))
                || (!int.TryParse(Arguments[_cityDistrictArgName], out cityDistrict)))
                cityDistrict = GetDistrictIdFromConsole();

            if ((!Arguments.ContainsKey(_firstDeliveryDateTimeArgName))
                || (!DateTime.TryParse(Arguments[_firstDeliveryDateTimeArgName], out firstDeliveryDateTime)))
                firstDeliveryDateTime = GetDateTimeFromConsole();
        }

        private static DateTime GetDateTimeFromConsole()
        {
            //Проверка на правильность введенного времени первого заказа
            do
            {
                try
                {
                    Console.WriteLine("Введите время первого заказа: ");
                    return DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Время первого заказа введено неверно\n");
                }
            }
            while (true);
        }

        private static int GetDistrictIdFromConsole()
        {
            do
            {
                try
                {
                    Console.WriteLine("Введите id района: ");
                    var districtId = int.Parse(Console.ReadLine());

                    if ((districtId < 2560491) || (districtId > 2560495))
                        Console.WriteLine("Такого района нет в базе данных");
                    else
                        return districtId;
                }
                catch
                {
                    Console.WriteLine("Id района введен неверно");
                }

            }
            while (true);
        }
    }

}
