using ModernWpf.Controls;
using System.Windows;
using FinAnalizator_v_1.src.Service.Validations;


namespace FinAnalizator_v_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RecValid.SetContentFrame(ContentFrame);
            if (NavView.MenuItems.Count > 0)
            {
                NavView.SelectedItem = NavView.MenuItems[0];
            }
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not NavigationViewItem item) return;

            // обработка нажатия на вкладку меню
            switch (item.Tag.ToString())
            {
                case "upload":
                    ContentFrame.Content = new Pages.UploadFile();
                    RecValid.ResetRecPageFlag();
                    break;
                case "expenses":
                    ContentFrame.Content = new Pages.Expenses();
                    RecValid.ResetRecPageFlag();
                    break;
                case "rec":
                    var wagesForm = new Forms.WagesForm();
                    wagesForm.ShowDialog();
                    break;
                default:
                    break;
            }

        }
    }
}