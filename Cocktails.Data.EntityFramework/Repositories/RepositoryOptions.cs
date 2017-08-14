namespace Cocktails.Data.EntityFramework.Repositories
{
    public class RepositoryOptions : IRepositoryOptions
    {
        public bool AutoCommit { get; set; } = true;
    }
}
