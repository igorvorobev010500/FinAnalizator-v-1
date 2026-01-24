using FinAnalizator_v_1.src.Model;

namespace FinAnalizator_v_1.src.Service.UI
{
    public interface _IDialog
    {
        void ShowInfo(string message, string title = "Информация");

        void ShowError(string message, string title = "Ошибка");

    }
}
