namespace Airslip.Common.Services.Handoff.Interfaces;

public interface IMessageHandoffWorker
{
    Task Execute(string message);
}