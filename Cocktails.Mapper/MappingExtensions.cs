using System.ComponentModel;

using AutoMapper;

namespace Cocktails.Mapper
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreReadOnly<TSource, TDestination>(
                   this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);

            foreach (var property in sourceType.GetProperties())
            {
                var descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                var attribute = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                if (attribute.IsReadOnly == true)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
