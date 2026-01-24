using FinAnalizator_v_1.src.Model;
using FinAnalizator_v_1.src.Service.UI;
using static FinAnalizator_v_1.src.Service.Validations.RecValid;
using FinAnalizator_v_1.Pages;
using System.Windows;
using System.Threading.Tasks;

namespace FinAnalizator_v_1.Forms
{
    public partial class WagesForm : Window
    {
        private static readonly DialogService _dialogService = new DialogService();

        public WagesForm()
        {
            InitializeComponent();
           // Loaded += (s, e) => WagesText.Focus();
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