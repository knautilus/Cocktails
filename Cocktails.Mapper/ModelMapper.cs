using AutoMapper;

namespace Cocktails.Mapper
{
    public class ModelMapper : IModelMapper
    {
        private readonly IMapper _mapper;

        public ModelMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TModel Map<TModel>(object source)
        {
            return _mapper.Map<TModel>(source);
        }
    }
}
