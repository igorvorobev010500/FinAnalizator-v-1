using FinAnalizator_v_1.src.Model;

namespace FinAnalizator_v_1.src.Service.Recomandation
{
    public class RecService
    {
        private readonly List<ExcelModel> _expense;
        private readonly decimal _wages;


        private const decimal needs = 50m;    // Нужды
        private const decimal wishes = 30m;    // Желания
        private const decimal savings = 20m;  // Сбережения

        public RecService(List<ExcelModel> Expense, decimal Amount)
        {
            _expense = Expense;
            _wages = Amount;
        }

        public async Task<List<string>> Recomandations()
        {
            var rec = new List<string>();

            if (_expense.Count == 0)
            {
                rec.Add("Нет данных для анализа");
                return rec;
            }

            await Calculation(rec);

            return rec;
        }

        //<summary>

        //decimal? totalExspense = 0;
        //decimal? totalNeeds = 0;
        //decimal? totalWishes = 0;
        //decimal? totalSaving = 0;
        //decimal? otherTotal = 0;

        //помечаем их как nullable для расчетов с данными которые могут иметь null
        //возможно выпадение исключения при попытке сложения null с decimal

        //</summary>


        private async Task Calculation(List<string> rec)
        {
            //подсчет общих расходов и по категориям
            decimal? totalExspense = 0;
            decimal? totalNeeds = 0;
            decimal? totalWishes = 0;
            decimal? totalSaving = 0;
            decimal? otherTotal = 0;

            foreach (var expens in _expense)
            {
                totalExspense += expens.Expense;

                switch (expens.Category)
                {
                    case Categories.нужды:
                        totalNeeds += expens.Expense;
                        break;
                    case Categories.желания:
                        totalWishes += expens.Expense;
                        break;
                    case Categories.сбережения:
                        totalSaving += expens.Expense;
                        break;
                    default:
                        otherTotal += expens.Expense;
                        break;
                }
            }
            decimal? remaining = _wages - totalExspense;

            rec.Add($"Ваша недельная зп {_wages:C2}");
            rec.Add($"Ваши расходы за неделю {totalExspense:C2}");
            rec.Add($"Остаток {remaining:C2}");
            rec.Add("");


            //анализ бюджета
            if (remaining < 0)
            {
                rec.Add($"Вы привысили расходы на {Math.Abs((decimal)remaining):C2} , сократите ваши расходы");
            }
            else if (remaining < _wages * 0.1m)
            {
                rec.Add($"Внимание!!! Осталось мало денег до конца недели");
            }
            else
            {
                rec.Add($"Ваш бюджет в порядке");
            }

            
            //rec.Add("") - отделяем в пустой строке 
            rec.Add("");
            rec.Add("Анализ по категориям");

            // Анализ расходов по категориям
            switch (totalExspense)
            {
                case > 0:
                    CategoryAnalysis(rec, (decimal)totalExspense, (decimal)totalNeeds, (decimal)totalWishes, (decimal)totalSaving, (decimal)otherTotal);
                    break;
                default:
                    rec.Add("Нет данных о расходах по категориям");
                    break;
            }

            // Рекомендации по бюджету
            rec.Add("Рекомендации по распределению бюджета:");
            rec.Add("");

            decimal recommendedNeeds = _wages * (needs / 100m);
            decimal recommendedWishes = _wages * (wishes / 100m);
            decimal recommendedSavings = _wages * (savings / 100m);

            // Рекомендации по нуждам
            rec.Add($"1. Нужды (рекомендуется {needs}% = {recommendedNeeds:C2}):");
            BudgetRec(rec, (decimal)totalNeeds, recommendedNeeds, _wages, "нуждам");
            rec.Add("");

            // Рекомендации по желаниям
            rec.Add($"2. Желания (рекомендуется {wishes}% = {recommendedWishes:C2}):");
            BudgetRec(rec, (decimal)totalWishes, recommendedWishes, _wages, "желаниям");
            rec.Add("");

            // Рекомендации по сбережениям
            rec.Add($"3. Сбережения (рекомендуется {savings}% = {recommendedSavings:C2}):");
            SaveRec(rec, (decimal)totalSaving, recommendedSavings, _wages);
            rec.Add("");

            // Итоговые рекомендации
            rec.Add("Итоговые рекомендации:");
            rec.Add("");

            switch (remaining)
            {
                case < 0:
                    rec.Add($"• Срочно сократите расходы на {Math.Abs((decimal)remaining):C2}");
                    rec.Add($"• Попробуйте распределить по правилу 50/30/20");
                    break;
                case var rem when rem > _wages * 0.3m:
                    rec.Add($"• Отлично! У вас хороший остаток: {remaining:C2}");
                    rec.Add($"• Рекомендуем часть остатка добавить в сбережения");
                    break;
            }

            // Общая статистика
            rec.Add("");
            rec.Add($"Общая статистика:");
            rec.Add($"• Всего записей расходов: {_expense.Count}");
            rec.Add($"• Средний расход в день: {(totalExspense / 7):C2}");
            rec.Add($"• Дней до следующей зарплаты: {(_wages / (totalExspense > 0 ? totalExspense / 7 : 1)):F1}");
        }

        // Метод для подсчета процентов
        private static decimal GetPercentage(decimal amount, decimal total) =>
            total == 0 ? 0 : (amount / total) * 100;

        private void CategoryAnalysis(List<string> rec, decimal totalExpense, decimal totalNeeds, decimal totalWishes, decimal totalSaving, decimal otherTotal)
        {
            rec.Add($"1. Нужды:");
            rec.Add($"   • Потрачено: {totalNeeds:C2}");
            rec.Add($"   • Процент от всех расходов: {GetPercentage(totalNeeds, totalExpense):F1}%");
            rec.Add($"   • Процент от зарплаты: {GetPercentage(totalNeeds, _wages):F1}%");
            rec.Add("");

            rec.Add($"2. Желания:");
            rec.Add($"   • Потрачено: {totalWishes:C2}");
            rec.Add($"   • Процент от всех расходов: {GetPercentage(totalWishes, totalExpense):F1}%");
            rec.Add($"   • Процент от зарплаты: {GetPercentage(totalWishes, _wages):F1}%");
            rec.Add("");

            rec.Add($"3. Сбережения:");
            rec.Add($"   • Потрачено: {totalSaving:C2}");
            rec.Add($"   • Процент от всех расходов: {GetPercentage(totalSaving, totalExpense):F1}%");
            rec.Add($"   • Процент от зарплаты: {GetPercentage(totalSaving, _wages):F1}%");
            rec.Add("");

            switch (otherTotal)
            {
                case > 0:
                    rec.Add($"4. Другие категории:");
                    rec.Add($"   • Потрачено: {otherTotal:C2}");
                    rec.Add($"   • Процент от всех расходов: {GetPercentage(otherTotal, totalExpense):F1}%");
                    rec.Add($"   • Процент от зарплаты: {GetPercentage(otherTotal, _wages):F1}%");
                    rec.Add("");
                    break;
            }
        }

        private void BudgetRec(List<string> rec, decimal spent, decimal recommended, decimal wages, string categoryName)
        {
            switch (spent)
            {
                case var s when s > recommended * 1.1m:
                    rec.Add($"   • Слишком много! Сократите {categoryName} на {(spent - recommended):C2}");
                    rec.Add($"   • Сейчас: {spent:C2} ({GetPercentage(spent, wages):F1}%)");
                    break;
                case var s when s < recommended * 0.9m:
                    rec.Add($" Можно позволить себе больше по {categoryName}");
                    rec.Add($" Сейчас: {spent:C2} ({GetPercentage(spent, wages):F1}%)");
                    break;
                default:
                    rec.Add($" В норме!");
                    rec.Add($" Сейчас: {spent:C2} ({GetPercentage(spent, wages):F1}%)");
                    break;
            }
        }

        private void SaveRec(List<string> rec, decimal saved, decimal recommended, decimal wages)
        {
            switch (saved)
            {
                case var s when s < recommended * 0.9m:
                    rec.Add($" Мало! Увеличьте сбережения на {(recommended - saved):C2}");
                    rec.Add($" Сейчас: {saved:C2} ({GetPercentage(saved, wages):F1}%)");
                    break;
                case var s when s > recommended * 1.1m:
                    rec.Add($" Отлично! Вы много откладываете");
                    rec.Add($" Сейчас: {saved:C2} ({GetPercentage(saved, wages):F1}%)");
                    break;
                default:
                    rec.Add($" В норме!");
                    rec.Add($" Сейчас: {saved:C2} ({GetPercentage(saved, wages):F1}%)");
                    break;
            }
        }


    }
}
