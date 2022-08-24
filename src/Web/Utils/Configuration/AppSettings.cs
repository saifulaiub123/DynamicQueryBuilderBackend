namespace Involys.Poc.Api
{
    public class AppSettings
    {
        public string Secret { get; set; }

        /* limit for the length of each multipart body (in bytes) */
        public long MaximumFormBodyLength { get; set; }

        public string Database { get; set; }
    }
}
