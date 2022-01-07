using Airslip.Analytics.Core.Enums;

namespace Airslip.Analytics.Core.Interfaces
{
    public interface IFromDataSource
    {
        DataSources DataSource { get; set; }
        long TimeStamp { get; set; }
    }
}