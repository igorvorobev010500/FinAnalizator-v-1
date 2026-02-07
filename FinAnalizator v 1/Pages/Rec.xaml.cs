using FinAnalizator_v_1.Forms;
using FinAnalizator_v_1.src.Service.UI;
using OfficeOpenXml.LoadFunctions.Params;
using System.Windows.Controls;
using System.Text;

namespace FinAnalizator_v_1.Pages
{
    public partial class Rec : UserControl
    {
        private static readonly _IDialog _IDialog = new DialogService();
        public List<string> Recommendations { get; set; }

        //конструктор по умолчанию при нажатии на вкладку
        public Rec()
        {
            InitializeComponent();

        }

        //конструктор для передачи рекомендаций
        public Rec(List<string> recommendations)
        {

            InitializeComponent();

            Recommendations = recommendations;

            Dispatcher.BeginInvoke(() => LoadData());
        }

        public static async Task CheckWages()
        {
            var wagesForm = new WagesForm();
            wagesForm.ShowDialog();
        }

        private void LoadData()
        {
            if (recommendationsBlock == null)
            {
                return;
            }

            if (Recommendations != null && Recommendations.Count > 0)
            {
                var sb = new System.Text.StringBuilder();
                foreach (var rec in Recommendations)
                {
                    sb.AppendLine(rec);
                }

                var recommendationsText = sb.ToString();
                recommendationsBlock.Text = recommendationsText;
            }
            else
            {
                recommendationsBlock.Text = "Рекомендации отсутствуют";
            }
        }
    }
}