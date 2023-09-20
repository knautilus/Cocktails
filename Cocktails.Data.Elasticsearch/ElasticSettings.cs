namespace Cocktails.Data.Elasticsearch
{
    public class ElasticSettings
    {
        public string NodesList { get; set; } = ",";
        public string AuthHeaderKey { get; set; } = "";
        public string AuthHeaderValue { get; set; } = "";
    }
}
