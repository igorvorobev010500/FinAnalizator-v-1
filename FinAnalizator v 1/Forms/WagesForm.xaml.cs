using FinAnalizator_v_1.src.Service.UI;
using System.Windows;
using static FinAnalizator_v_1.src.Service.Validations.RecValid;

namespace FinAnalizator_v_1.Forms
{
    public partial class WagesForm : Window
    {
        private static readonly DialogService _dialogService = new DialogService();

        public WagesForm()
        {
            InitializeComponent();
        }

        private void CloseForm_Click(object sender, RoutedEventArgs e)
        {
            string input = WagesText.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                _dialogService.ShowError("Поле не может быть пустым.");
                return;
            }

            if (!decimal.TryParse(input, out decimal amount))
            {
                _dialogService.ShowError("Введите корректное число.");
                return;
            }

            ValidWages(amount);
            DialogResult = true;
            Close();
        }
    }
}