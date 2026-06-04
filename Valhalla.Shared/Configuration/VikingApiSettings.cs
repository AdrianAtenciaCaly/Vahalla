namespace Valhalla.Shared.Configuration
{
    public sealed class VikingApiSettings
    {
        public const string SectionName = "VikingApi";
        public string BaseUrl { get; init; } = string.Empty;
    }
}
