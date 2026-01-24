using FinAnalizator_v_1.Forms;
using FinAnalizator_v_1.src.Service.Parsing;
using FinAnalizator_v_1.src.Service.Data;
using System.Windows;
using System.Windows.Controls;

namespace FinAnalizator_v_1.Pages
{
    public partial class UploadFile : UserControl
    {
        public UploadFile()
        {
            InitializeComponent();
        }

        #region Обработка нажатия кнопки
        private async void LoadExcelFile_Click(object sender, RoutedEventArgs e)
        {
            loadButton.IsEnabled = false;
            try
            {
                string filePath = SelectExcelFile();

                if (!string.IsNullOrEmpty(filePath))
                {
                    var expenses = await ExcelParser.ParseExcel(filePath);

                    DataService.ExpensesData = expenses;

                    MessageForm messageWindow = new MessageForm("Данные успешно загружены!", "Успех");
                    messageWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageForm messageWindow = new MessageForm(ex.Message, "Ошибка");
                messageWindow.ShowDialog();
            }
            finally
            {
                loadButton.IsEnabled = true;
            }
        }
        #endregion

        #region Выбор файла Excel
        private string SelectExcelFile()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Выберите файл с данными о тратах"
            };
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                return openFileDialog.FileName;
            }
            else
            {
                throw new OperationCanceledException("Выбор файла был отменен пользователем.");
            }
        }
        #endregion
    }
}
