namespace Cocktails.Api.Scheduler.Configuration
{
    public class HangfireSettings
    {
        public string DashboardUrl { get; set; }
        //public string DashboardAccessKey { get; set; }
        public string StorageConnectionString { get; set; }
        public string StorageSchemaName { get; set; }
    }
}
