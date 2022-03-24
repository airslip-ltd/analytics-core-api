using Airslip.Common.Utilities;
using AutoMapper;
using JetBrains.Annotations;

namespace Airslip.Analytics.Processor.Mappers.Resolvers;

[UsedImplicitly]
public class UniqueIdResolver<TSource, TDest> : IValueResolver<TSource, TDest, string?>
{
    public string Resolve(TSource source, TDest destination, string? member, 
        ResolutionContext context)
    {
        return CommonFunctions.GetId();
    }
}