namespace Cocktails.Jobs.Scheduler.Recurring
{
    public class TestRecurringJob2 : IRecurringJob
    {
        public bool Enabled => true;

        public string CronUtcExpression => "*/5 * * * *";

        private readonly string _guid;

        public TestRecurringJob2()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 80; i++)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }
                catch (Exception)
                {
                    Console.WriteLine($"{_guid}:ThrowIfCancellationRequested");
                    break;
                }

                Thread.Sleep(1000);
                Console.WriteLine($"{_guid}:{i}");
            }

            Console.WriteLine($"{_guid}:finished");

            return Task.CompletedTask;
        }
    }
}
