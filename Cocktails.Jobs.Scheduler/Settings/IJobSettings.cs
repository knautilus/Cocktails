namespace Cocktails.Jobs.Scheduler.Settings
{
    public interface IJobSettings
    {
        bool Enabled { get; set; }
        string CronUtcExpression { get; set; }
    }
}
