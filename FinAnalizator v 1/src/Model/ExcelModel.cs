using System.ComponentModel;

namespace FinAnalizator_v_1.src.Model
{
    #region Модель для хранения данных из Excel файла

    // Day - день недели
    // Expense - сумма траты
    // Value - валюта (рубли)
    // ExpenseInfo - информация о трате

    public class ExcelModel
    {
        [DisplayName("День")]
        public string? Day { get;set; }

        [DisplayName("Сумма")]
        public string? Expense { get; set; }

        [DisplayName("Валюта")]
        public string? Value { get; set; }

        [DisplayName("Описание")]
        public string? ExpenseInfo { get; set; }

        [DisplayName("Категория")]
        public Categories? Category { get; set; }

    }
    #endregion

}
