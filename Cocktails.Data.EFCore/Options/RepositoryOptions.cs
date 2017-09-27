namespace Cocktails.Data.EFCore.Options
{
    public class RepositoryOptions : IRepositoryOptions
    {
        public bool AutoCommit { get; set; } = true;
    }
}
