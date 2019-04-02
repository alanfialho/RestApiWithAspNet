namespace DatabaseIntegration.ViewModels
{
    public class LinkViewModel
    {
        public string Href { get; }
        public string Rel { get; }
        public string Method { get; }

        public LinkViewModel(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
