namespace NotificationService.Infrastructure;

public interface IMessageHandler
{
    void Register(CancellationToken cancellationToken);

}