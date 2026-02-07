using FinAnalizator_v_1.Pages;
using FinAnalizator_v_1.src.Service.Data;
using FinAnalizator_v_1.src.Service.Recomandation;
using FinAnalizator_v_1.src.Service.UI;
using System.Windows.Controls;

namespace FinAnalizator_v_1.src.Service.Validations
{
    public class RecValid
    {

        private static readonly _IDialog _IDialog = new DialogService();
        private static ContentControl _contentFrame;
        private static bool _recPageShown = false;
        private static Rec _recPageInstance;

        public static void SetContentFrame(ContentControl contentFrame)
        {
            _contentFrame = contentFrame;
        }

        public static void ResetRecPageFlag()
        {
            _recPageShown = false;
            _recPageInstance = null;
        }

        public static bool IsRecPageShown => _recPageShown;

        public static Rec GetRecPage()
        {
            return _recPageInstance;
        }

        public static async Task ValidWages(decimal wages)
        {
            try
            {

                if (wages <= 0)
                {
                    _IDialog.ShowError("Введенная зп должна быть больше нуля.");
                    return;
                }

                var expens = DataService.ExpensesData;

                var recService = new RecService(expens, wages);
                var recommendations = await recService.Recomandations();



                if (_contentFrame != null)
                {
                    _recPageInstance = new Rec(recommendations);
                    

                    _contentFrame.Content = _recPageInstance;
                    _recPageShown = true;

                }
                else
                {
                    _IDialog.ShowError("Ошибка навигации: контент-фрейм не инициализирован.");
                }

                System.Diagnostics.Debug.WriteLine($"[ValidWages] END");
            }
            catch (FormatException)
            {
                _IDialog.ShowError("Пожалуйста, введите корректное значение для зп.");
            }
            catch (OverflowException)
            {
                _IDialog.ShowError("Вай, да вы миллиардер с таким заработком? (Армянским акцентом)");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ValidWages] Exception: {ex.Message}\n{ex.StackTrace}");
                _IDialog.ShowError($"Ошибка при обработке рекомендаций: {ex.Message}");
            }
        }
    }
}
