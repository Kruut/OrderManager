using System.Numerics;

namespace OrderManager
{
    internal class DistrictsManager<T>
    {
        private readonly List<T> _specifiedDistricts;

        private readonly bool _isDistrictsSpecified;

        private static DistrictsManager<T> _instance;

        private DistrictsManager(List<T> specifiedDistricts)
        {
            _specifiedDistricts = specifiedDistricts;
            _isDistrictsSpecified = true;
        }

        public static void SetDistrictsList(List<T> specifiedDistricts)
        {
            _instance = new DistrictsManager<T>(specifiedDistricts);
        }

        public static bool IsDistrictValid(T district)
        {
            return _instance is null || _instance._specifiedDistricts.Contains(district);
        }

        public static List<T> GetDistrictList() => _instance?._specifiedDistricts;

        public static bool IsDistrictsSpecified => _instance is not null && _instance._isDistrictsSpecified;
    }
}
