using Airslip.Analytics.Core.Models;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IAnalysisMessagingService<TModel>
{
    Task RequestAnalysis(TModel model);
}