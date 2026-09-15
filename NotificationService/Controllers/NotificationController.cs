using Microsoft.AspNetCore.Mvc;
using Models;

namespace NotificationService.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    [HttpPost(Name = "CreateNotification")]
    public IActionResult CreateNotification(Notification notification)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet(Name = "GetNotification")]
    public IActionResult GetNotification()
    {
        throw new NotImplementedException();    
    }

}