namespace ChannelService.Service;

public class ChannelException : Exception
{
    public class ChannelNotFoundException(string message) : Exception(message);
    
    public class ChannelIdNotNullOrEmptyException(string message) : Exception(message);
}