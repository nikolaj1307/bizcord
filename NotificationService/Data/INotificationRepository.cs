using NotificationService.Models;

namespace NotificationService.Data;

public interface INotificationRepository
{
    Task<Notification> GetNotificationByIdAsync(string id);
}