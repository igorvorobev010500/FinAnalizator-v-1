using FinAnalizator_v_1.src.Model;

namespace FinAnalizator_v_1.src.Service.Data
{
    public static class DataService
    {
        #region Хранение данных из Excel файла
        private static List<ExcelModel> _expensesData = new();

        public static List<ExcelModel> ExpensesData
        {
            get => _expensesData;
            set => _expensesData = value ?? new List<ExcelModel>();
        }
        #endregion
    }
}
