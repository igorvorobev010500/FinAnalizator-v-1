using FinAnalizator_v_1.Forms;
using FinAnalizator_v_1.src.Model;
using System.Windows;

namespace FinAnalizator_v_1.src.Service.UI
{
    public class DialogService : _IDialog
    {
        public void ShowInfo(string message, string title = "Информация")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var messageForm = new MessageForm(message, title);
                messageForm.ShowDialog();
            });
        }

        public void ShowError(string message, string title = "Ошибка")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var messageForm = new MessageForm(message, title);
                messageForm.ShowDialog();
            });
        }

   
    }
}
