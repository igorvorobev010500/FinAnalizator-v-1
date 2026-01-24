using System.Text.RegularExpressions;

namespace FinAnalizator_v_1.src.Service.Parsing
{
    public static class DataParser
    {
        #region ОТделение суммы и описания
        /// Отделяет цифры (сумму) от текста (описание расхода)
        public static (string amount, string description) SeparateString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (string.Empty, string.Empty);

            var match = Regex.Match(input.Trim(), @"(\d+(?:[.,]\d+)?)");

            if (match.Success)
            {
                string amount = match.Groups[1].Value;
                string description = input.Replace(amount, "").Trim();

                description = NormalizeCurrency(description);

                return (amount, description);
            }

            return (string.Empty, input.Trim());
        }
        #endregion

        #region Нормализуем и удаляем обозначения валют
        /// Приводит обозначения валют к единому формату "рублей" и удаляет его для более корректного отображения в форме    
        public static string NormalizeCurrency(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string normalized = Regex.Replace(
                input,
                @"\b(?:рублей|р|руб\.?|р\.?)\b",
                "рублей",
                RegexOptions.IgnoreCase
            );

            string deleteString = "рублей";
            string result = normalized.Replace(deleteString, "");
            return result.Trim();
        }
        #endregion
    }
}
