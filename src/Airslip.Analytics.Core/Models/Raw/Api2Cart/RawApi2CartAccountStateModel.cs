using Airslip.Common.CustomerPortal.Enums;

namespace Airslip.Analytics.Core.Models.Raw.Api2Cart;

public record RawApi2CartAccountStateModel
{
    public AuthenticationState CurrentState { get; init; } = AuthenticationState.Authenticating;
    public int Offset { get; init; }
}