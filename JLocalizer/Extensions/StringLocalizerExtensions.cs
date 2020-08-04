namespace Microsoft.Extensions.Localization
{
    using JLocalizer;

    public static class StringLocalizerExtensions
    {
        public static void Reset(this IStringLocalizer localizer)
        {
            ((JStringLocalizer)localizer).Resource.Execute();
        }

        public static void Reset<T>(this IStringLocalizer<T> localizer)
        {
            Reset(((JStringLocalizer<T>)localizer).Localizer);
        }
    }
}
