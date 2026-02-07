using FinAnalizator_v_1.src.Model;

namespace FinAnalizator_v_1.src.Service.Categorization
{
    public class CategoryKeywords
    {
        //создаем словарь категорий с ключевыми словами
        public static readonly Dictionary<Categories, (string DisplayName, string[] Keywords)> categoryData = new()
        {
            { Categories.нужды, ("Нужды", new string[] { "продукты", "аренда", "коммуналка", "транспорт", "медицина", "такси", "ремонт авто", "обед", "завтрак", "ужин", "учёба"}) },
            { Categories.желания, ("Желания", new string[] { "ресторан", "развлечения", "путешествия", "хобби", "одежда", "кино", "кофе", "техника", "покупка игр"}) },
            { Categories.сбережения, ("Сбережения", new string[] { "депозит", "инвестиции", "накопления", "пенсия", "страховка" }) }
        };
    }
}
