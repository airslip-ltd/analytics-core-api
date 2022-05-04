using Airslip.Analytics.Core.Interfaces;
using Airslip.Common.Repository.Types.Models;
using Airslip.Common.Types.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Airslip.Analytics.Core.Data;

namespace Airslip.Analytics.Core.Models;

public record QueryModel(int Page, int RecordsPerPage,  
        EntitySearchSortModel Sort, List<SearchFilterModel> Search);