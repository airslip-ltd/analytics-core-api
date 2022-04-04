using Airslip.Common.Services.Handoff.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IAnalysisHandlingService<in TModel> : IMessageHandoffWorker
    where TModel : class, ITraceable
{
    Task Execute(TModel model);
}