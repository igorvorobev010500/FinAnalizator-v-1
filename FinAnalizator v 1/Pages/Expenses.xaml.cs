using FinAnalizator_v_1.src.Service.UI;
using FinAnalizator_v_1.src.Service.Data;
using System.Windows.Controls;

namespace FinAnalizator_v_1.Pages
{
    public partial class Expenses : UserControl
    {

        private static _IDialog _IDialog = new DialogService();

        public Expenses()
        {
            InitializeComponent();
            LoadExpenses();
        }

        private void LoadExpenses()
        {
            // Получаем данные расходов из DataService
            var expensesData = DataService.ExpensesData;
            // Проверяем, что данные не null и содержат хоть 1 строку
            if (expensesData != null && expensesData.Count > 0)
            {
                ExpensesDataGrid.ItemsSource = expensesData;
            }
            else
            {
                _IDialog.ShowInfo("Данные не были загруженны, возможны проблемы с отображением");
            }
        }
    }
}
