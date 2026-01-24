using ModernWpf.Controls;
using System.Windows;


namespace FinAnalizator_v_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
                    break;
                case "expenses":
                    ContentFrame.Content = new Pages.Expenses();
                    break;
                case "rec":
                    ContentFrame.Content = new Pages.Rec();
                    break;
                case "settings":
                    ContentFrame.Content = new Pages.Settings();
                    break;
                default:
                    break;
            }

        }
    }
}