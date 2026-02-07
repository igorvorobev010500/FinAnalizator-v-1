using OfficeOpenXml;
using System.IO;
using FinAnalizator_v_1.src.Model;
using FinAnalizator_v_1.src.Service.UI;
using static FinAnalizator_v_1.src.Service.Categorization.CategoryService;

namespace FinAnalizator_v_1.src.Service.Parsing
{
    public class ExcelParser
    {

        private static readonly _IDialog _IDialog = new DialogService();

        public static async Task<List<ExcelModel>> ParseExcel(string filePath)
        {
            ExcelPackage.License.SetNonCommercialPersonal("FinAnalizator User");

            using var package = new ExcelPackage(new FileInfo(filePath));
            var sheet = package.Workbook.Worksheets[0];

            var expenses = new List<ExcelModel>();

            for (int col = 1; col <= (sheet.Dimension?.End.Column ?? 0); col++)
            {
                string? day = sheet.Cells[1, col].Text?.Trim();

                if (string.IsNullOrEmpty(day))
                    break;

                for (int row = 2; row <= (sheet.Dimension?.End.Row ?? 0); row++)
                {
                    var cell = sheet.Cells[row, col];
                    var expenseText = cell.Text?.Trim();

                    if (!string.IsNullOrEmpty(expenseText))
                    {
                        var (amount, description) = DataParser.SeparateString(expenseText);

                        //добавляем обработку ошибок ес convert.todecimal выкинет исключение
                        try
                        {
                            expenses.Add(new ExcelModel
                            {
                                Day = day,
                                Expense = Convert.ToDecimal(DataParser.NormalizeCurrency(amount)),
                                ExpenseInfo = description,
                                Value = "рублей",
                                Category = CategoryDefinition(description)
                            });
                        }
                        catch (FormatException)
                        {
                            _IDialog.ShowError(amount + " - неверный формат числа. Проверьте файл и попробуйте снова.");
                        }

                    }
                }
            }
            return expenses;
        }
    }

}
