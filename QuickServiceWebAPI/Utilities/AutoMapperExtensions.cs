using AutoMapper;
using System.Reflection;

namespace QuickServiceWebAPI.Utilities
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }

    //public class CreateSlametricsToSlametricsListConverter :
    //         ITypeConverter<CreateSlametricDTO, IEnumerable<Slametric>>
    //{
    //    public IEnumerable<Slametric> Convert
    //    (CreateSlametricDTO source, IEnumerable<Slametric> destination, ResolutionContext context)
    //    {
    //        /*first mapp from People, then from Team*/
    //        foreach (var model in source.SlametricsDetails.Select
    //                (e => context.Mapper.Map<Slametric>(e)))
    //        {
    //            context.Mapper.Map(source, model);
    //            yield return model;
    //        }

    //        /*first mapp from Team, then from People*/
    //        //foreach (var member in source.Members)
    //        //{
    //        //    var model = context.Mapper.Map<TeamMember>(source);
    //        //    context.Mapper.Map(member, model);
    //        //    yield return model;
    //        //}
    //    }
    //}
}
