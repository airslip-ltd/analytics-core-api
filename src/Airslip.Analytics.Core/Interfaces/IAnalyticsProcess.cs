using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IAnalyticsProcess<in TModel> 
{
    Task Execute(TModel model);
}