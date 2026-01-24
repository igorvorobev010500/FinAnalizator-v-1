using FinAnalizator_v_1.Forms;
using FinAnalizator_v_1.src.Service.UI;
using System.Windows.Controls;

namespace FinAnalizator_v_1.Pages
{
    public partial class Rec : UserControl
    {
        private static readonly _IDialog _IDialog = new DialogService();

        public Rec()
        {
            InitializeComponent();
            CheckWages();
        }

        public static async Task CheckWages()
        {
            var wagesForm = new WagesForm();
            wagesForm.ShowDialog();
           


        }
    }
}