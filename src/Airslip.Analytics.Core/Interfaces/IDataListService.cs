using Airslip.Analytics.Core.Models;
using Airslip.Common.Types.Interfaces;
using System.Threading.Tasks;

namespace Airslip.Analytics.Core.Interfaces;

public interface IDataListService
{
    Task<IResponse> GetCurrencies(DataSearchModel query);
}