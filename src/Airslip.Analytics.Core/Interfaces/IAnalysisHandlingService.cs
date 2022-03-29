using Airslip.Common.Services.Handoff.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IAnalysisHandlingService<in TModel> : IMessageHandoffWorker
{
    Task Execute(TModel model);
}