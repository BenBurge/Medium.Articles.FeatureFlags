namespace RestApi.Entities
{
    public class FeatureFlag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
    }
}
