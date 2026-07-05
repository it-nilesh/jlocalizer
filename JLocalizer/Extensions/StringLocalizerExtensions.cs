namespace Microsoft.Extensions.Localization
{
    using JLocalizer;
    using System;

    public static class StringLocalizerExtensions
    {
        public static void Reset(this IStringLocalizer localizer)
        {
            if (localizer is JStringLocalizer jStringLocalizer)
            {
                jStringLocalizer.Resource.Reload();
                return;
            }

            throw new InvalidOperationException("Reset is only supported for JLocalizer localizer instances.");
        }

        public static void Reset<T>(this IStringLocalizer<T> localizer)
        {
            if (localizer is JStringLocalizer<T> jStringLocalizer)
            {
                Reset(jStringLocalizer.Localizer);
                return;
            }

            throw new InvalidOperationException("Reset is only supported for JLocalizer localizer instances.");
        }
    }
}
