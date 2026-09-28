using NotificationService.Models;

namespace NotificationService.Data;

public class NotificationRepository : INotificationRepository
{
    public Task<Notification> GetNotificationByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}