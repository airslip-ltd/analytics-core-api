using Airslip.Common.Types.Interfaces;
using System.Collections.Generic;

namespace Airslip.Analytics.Core.Models;

public record BankTransactionSummaryResponse(List<BankTransactionSummaryModel> Transactions) : ISuccess;