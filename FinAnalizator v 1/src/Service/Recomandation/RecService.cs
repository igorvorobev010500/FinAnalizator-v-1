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

        private async Task Calculation(List<string> rec)
        {
            decimal totalExspense = 0;
            decimal totalNeeds = 0;
            decimal totalWishes = 0;
            decimal totalSaving = 0;
            decimal otherTotal = 0;

            foreach (var Expense in _expense)
            {
                totalExspense += _wages;

                switch (Expense.Category)
                {
                    case Categories.нужды:
                        totalNeeds += _wages;
                        break;
                    case Categories.желания:
                        totalWishes += _wages;
                        break;
                    case Categories.сбережения:
                        totalSaving += _wages;
                        break;
                    default:
                        otherTotal += _wages;
                        break;
                }
            }
            decimal remaining = _wages - totalExspense;

            rec.Add($"Ваша недельная зп {_wages:C2}");
            rec.Add($"Ваши расходы за неделю {totalExspense:C2}");
            rec.Add($"Остаток {remaining:C2}");
            rec.Add("");


            //анализ бюджета
            if (remaining < 0)
            {
                rec.Add($"Вы привысили расходы на {Math.Abs(remaining):C2} , сократите ваши расходы");
            }
            else if (remaining < _wages * 0.1m)
            {
                rec.Add($"Внимание!!! Осталось мало денег до конца недели");
            }
            else
            {
                rec.Add($"Ваш бюджет в порядке");
            }

            //rec.Add("") отделяем в пустой строке 
            rec.Add("");
            rec.Add("Анализ по категориям");


        }
    }
}
