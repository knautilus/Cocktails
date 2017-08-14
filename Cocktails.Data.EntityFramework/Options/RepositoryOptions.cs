namespace Cocktails.Data.EntityFramework.Options
{
    public class RepositoryOptions : IRepositoryOptions
    {
        public bool AutoCommit { get; set; } = true;
    }
}
