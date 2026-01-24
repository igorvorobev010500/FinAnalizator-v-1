using FinAnalizator_v_1.src.Model;
using FinAnalizator_v_1.src.Service.Recomandation;
using FinAnalizator_v_1.src.Service.UI;

namespace FinAnalizator_v_1.src.Service.Validations
{
    public class RecValid
    {

        private static readonly _IDialog _IDialog = new DialogService();

        public static async Task ValidWages(decimal wages)
        {
            try
            {
                if (wages <= 0)
                {
                    _IDialog.ShowError("Введенная зп должна быть больше нуля.");
                    return;
                }

                var recService = new RecService(new List<ExcelModel>(), wages);
                var recommendations = await recService.Recomandations();

            }
            catch (FormatException)
            {
                _IDialog.ShowError("Пожалуйста, введите корректное значение для зп.");
            }
            catch (OverflowException)
            {
                _IDialog.ShowError("Вай, да вы миллиардер с таким заработком? (Армянским акцентом)");
            }
        }
    }
}
