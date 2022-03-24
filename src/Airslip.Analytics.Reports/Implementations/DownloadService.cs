using Airslip.Analytics.Core.Models;
using Airslip.Analytics.Reports.Common;
using Airslip.Analytics.Reports.Interfaces;
using Airslip.Analytics.Reports.Models;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Interfaces;
using Airslip.Common.Types.Responses;
using Airslip.Common.Utilities;
using System.Net.Mime;
using System.Text;

namespace Airslip.Analytics.Reports.Implementations;

public class DownloadService : IDownloadService
{
    public async Task<IResponse> Download<TResponseType>(IReport report, OwnedDataSearchModel query, string fileName)
    {
        query = query with
        {
            Page = 0, RecordsPerPage = 0
        };
        
        IResponse response = await report.Execute(query);

        if (response is EntitySearchResponse<TResponseType> search)
        {
            fileName = $"{fileName}-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.json";
            string responseContent = Json.Serialize(search.Results);
            byte[] bytes = Encoding.ASCII.GetBytes(responseContent);

            return new DownloadResponse(fileName, bytes, MediaTypeNames.Application.Json);
        }

        return response;
    }
}