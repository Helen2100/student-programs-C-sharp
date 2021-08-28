namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            count %= 100;
            if (count <= 9 || count >= 21 && count <= 99)
            {
                int mod = count % 10;
                if (mod >= 2 && mod <= 4)
                    return "рубля";
                else if (mod >= 5 || mod == 0)
                    return "рублей";
                else
                    return "рубль";
            }
            else
                return "рублей";
        }
    }
}