namespace JLocalizer
{
    public class JLocalizationOptions
    {
        public JLocalizationOptions()
        {
            Resources = new JLocalizationResourceStore();
        }

        public JLocalizationResourceStore Resources { get; }
    }
}
