using EasyNetQ;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace NotificationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(IBus bus) : ControllerBase
{
    [HttpPost]
    [Route("publish")]
    public void PublishMessage()
    {
        bus.PubSub.Publish<PingMessage>(new PingMessage { Message = "Hello" });
    }
    
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