namespace OrderManager
{
    /// <summary>
    /// Класс для заказов
    /// </summary>
    class Order
    {
        public const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        /// <summary>
        /// id заказа
        /// </summary>
        public Guid Id = Guid.Empty;
        /// <summary>
        /// вес заказа
        /// </summary>
        public float Weight;
        /// <summary>
        /// идентификатор района
        /// </summary>
        public int DistrictId = 0;
        /// <summary>
        /// время заказа
        /// </summary>
        public DateTime Time;
        public Order(string id, string weight, string districtId, string time)
        {
            var exMessage = $"\n Заказ:\nid: {id}\nweight: {weight}\ndistrict: {districtId}\ntime: {time} \nВведен некорректно, он будет пропущен";
            if (!Guid.TryParse(id, out Id))
                throw new ArgumentException(exMessage); 

            try
            {
                if (((float)Convert.ToDouble(weight) > 0.0))
                    Weight = (float)Convert.ToDouble(weight);
                else
                    throw new ArgumentException(exMessage);
            }
            catch
            {
                throw new ArgumentException(exMessage);
            }

            try
            {
                DistrictId = Convert.ToInt32(districtId);
                if (!DistrictsManager<int>.IsDistrictValid(DistrictId))
                    throw new ArgumentException($"\n Заказ:\nid: {id}\nweight: {weight}\ndistrict: {districtId}\ntime: {time} \nВведен некорректно, он будет пропущен");
            }
            catch
            {
                throw new ArgumentException($"\n Заказ:\nid: {id}\nweight: {weight}\ndistrict: {districtId}\ntime: {time} \nВведен некорректно, он будет пропущен"); ;
            }

            if (!DateTime.TryParse(time, out Time))
                throw new ArgumentException(exMessage);
        }

        public Order(Guid id, float weight, int districtId, DateTime time)
        {
            Id = id;
            Weight = weight;
            DistrictId = districtId;
            Time = time;
        }

        public Order(List<string> orderAttributesStrings) : this(orderAttributesStrings[0], orderAttributesStrings[1], orderAttributesStrings[2], orderAttributesStrings[3])
        { }

        public override string ToString() => $"id {Id} \nweight {Weight} \ndistrict {DistrictId} \ntime {Time:dd-MM-yyyy HH:mm:ss}";

        public static Order GenerateOrder()
        {
            Random rand = new Random();
            Guid id = Guid.NewGuid();
            float weight = rand.Next(1, 15) + (((float)rand.Next(1, 10)) / 10);
            var districtList = DistrictsManager<int>.GetDistrictList();
            int districtId = (districtList is not null) ?
                districtList[rand.Next(districtList.Count - 1)] :
                2560490 + rand.Next(1, 5);
            DateTime.TryParse($"2024-10-29 {rand.Next(0, 24)}:{rand.Next(0, 60)}:{rand.Next(0, 60)}", out var time);
            Order order = new Order(id, weight, districtId, time);
            return order;
        }

    }
}
