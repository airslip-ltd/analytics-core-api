using Airslip.Common.Repository.Types.Interfaces;
using Airslip.Common.Services.Handoff.Interfaces;
using Airslip.Common.Types.Enums;
using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces
{
    public interface IRegisterDataService<TEntity, in TModel, in TRawModel> : IMessageHandoffWorker
        where TModel : class, IModel, IFromDataSource
        where TEntity : class, IEntity, IFromDataSource
    {
        Task RegisterData(TRawModel rawModel, DataSources dataSource);
        Task RegisterData(string message, DataSources dataSource);
    }
}