namespace Cocktails.Mapper
{
    public interface IModelMapper
    {
        TModel Map<TModel>(object source);
    }
}
