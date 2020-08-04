using System;

namespace JLocalizer
{
    public static class Helper
    {
        public static string GetBaseCultureName(string cultureName)
        {
            return cultureName.Contains("-")
                ? cultureName.Substring(0, cultureName.IndexOf("-", StringComparison.Ordinal))
                : cultureName;
        }

        public static string GetCultureNameFromFile(string fileName)
        {
            string fileNameWithoutExtension = fileName.Substring(0, fileName.LastIndexOf("."));
            int cultureLenght = fileNameWithoutExtension.LastIndexOf(".") + 1;
            string cultureName = fileNameWithoutExtension.Substring(cultureLenght == -1 ? 0 : cultureLenght);
            return cultureName;
        }
    }
}
