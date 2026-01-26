using FinAnalizator_v_1.Forms;
using FinAnalizator_v_1.src.Service.UI;
using OfficeOpenXml.LoadFunctions.Params;
using System.Windows.Controls;

namespace FinAnalizator_v_1.Pages
{
    public partial class Rec : UserControl
    {
        private static readonly _IDialog _IDialog = new DialogService();
        public List<string> Recommendations { get; }

        //конструктор по умолчанию при нажатии на вкладку
        public Rec()
        {
            InitializeComponent();
            CheckWages();
        }

        //конструктор для передачи рекомендаций
        public Rec(List<string> recommendations)
        {
            Recommendations = recommendations;
            LoadData();
        }

        public static async Task CheckWages()
        {
            //открытие окна для заполнения зп
            var wagesForm = new WagesForm();
            wagesForm.ShowDialog();
        }

        private void LoadData()
        {
            if (Recommendations != null && Recommendations.Count > 0)
            {
                //RecommendationsListBox.ItemsSource = Recommendations;
            }
            else
            {
                _IDialog.ShowInfo("Рекомендации не были загруженны, возможны проблемы с отображением");
            }
        }
    }
}