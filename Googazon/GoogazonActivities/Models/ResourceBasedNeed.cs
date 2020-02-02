namespace GoogazonActivities.Models
{
    public class ResourceBasedNeed
    {
        private readonly string _resource;
        private readonly string _need;

        public ResourceBasedNeed(string resource, string need)
        {
            _resource = resource;
            _need = need;
        }

        public static implicit operator string(ResourceBasedNeed instance) => $"{instance._resource}{instance._need}";
    }
}