using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Services.Handoff.Interfaces;
using Airslip.Common.Types.Interfaces;

namespace Airslip.Analytics.Core.Interfaces;

public interface IRegisterDataService<TEntity, in TModel, in TRawModel> : IMessageHandoffWorker
    where TModel : class, IModel, IFromDataSource
    where TEntity : class, IEntity, IFromDataSource
{
        
}