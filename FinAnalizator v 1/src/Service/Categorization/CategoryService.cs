using FinAnalizator_v_1.src.Model;
using static FinAnalizator_v_1.src.Service.Categorization.CategoryKeywords;

namespace FinAnalizator_v_1.src.Service.Categorization
{
    public static class CategoryService
    {
        // метод по определению категории по ключевым словам
        public static Categories? CategoryDefinition(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            var lowerText = text.ToLower();

            foreach (var (category, (_, keywords)) in categoryData)
            {
                if (keywords.Any(keyword => lowerText.Contains(keyword)))
                    return category;
            }

            return Categories.другое;
        }

    }
}
