using System;

public class Message
{
    public DateTime DateTime { get; }
    public string Type { get; }
    public string Content { get; }

    public Message(DateTime dateTime, string type, string content)
    {
        DateTime = dateTime;
        Type = type;
        Content = content;
    }
}
