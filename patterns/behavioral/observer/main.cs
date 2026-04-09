using System;
using System.Collections.Generic;

//--- Observer
public interface Observer
{
    void update(string message);
}

//--- Concrete Observer
public class LineNotifier : Observer
{
    private string channelID;
    private string accountName;

    public LineNotifier(string channelID, string accountName)
    {
        this.channelID = channelID;
        this.accountName = accountName;
    }

    public void update(string message)
    {
        Console.WriteLine($">>[LINE] ได้รับการอัพเดท");
        Console.WriteLine($">>Account Name: {accountName}");
        Console.WriteLine($">>ChannelID: {channelID}");
        Console.WriteLine($">>ได้รับการแจ้งเตือนว่า: {message}\n");
    }
}

public class PhoneNotifier : Observer
{
    private string phoneNumber;

    public PhoneNotifier(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;
    }

    public void update(string message)
    {
        Console.WriteLine($">>[PHONE] ได้รับการอัพเดท");
        Console.WriteLine($">>เบอร์โทร: {phoneNumber}");
        Console.WriteLine($">>SMS แจ้งเตือนว่า: {message}\n");
    }
}

public class DiscordNotifier : Observer
{
    private string channelID;
    private string serverName;

    public DiscordNotifier(string channelID, string serverName)
    {
        this.channelID = channelID;
        this.serverName = serverName;
    }

    public void update(string message)
    {
        Console.WriteLine($">>[DISCORD] ได้รับการอัพเดทไปที่");
        Console.WriteLine($">>Server Name: {serverName}");
        Console.WriteLine($">>ChannelID: {channelID}");
        Console.WriteLine($">>ได้รับการแจ้งเตือนว่า: {message} \n");
    }
}

//---- Publisher
public interface Publisher
{
    void subscriber(Observer o);
    void unsubscriber(Observer o);
    void notify(string message);
}

//---- Concrete Publisher
public class AiCCTV
{
    private EventPublisher eventPublisher;
    private string location;
    private string name;
    private string message;
    public AiCCTV(string location, string name, EventPublisher eventPublisher)
    {
        this.eventPublisher = eventPublisher;
        this.location = location;
        this.name = name;
    }

    public void detect(string detect)
    {
        Console.WriteLine($">>ชื่อ AiCCTV : {name}");
        Console.WriteLine($">>ติดตั้งที่ตำแหน่ง: {location}");

        this.message = "ตรวจจับเจอ: " + detect;
        this.eventPublisher.notify(message);
    }

    // -- get
    public string getLocattion()
    {
        return this.location;
    }

    public string getName()
    {
        return this.name;
    }

    public string getMessage()
    {
        return this.message;
    }

    //-- set 
    public void setLocation(string location)
    {
        this.location = location;
    }

    public void setMessage(string message)
    {
        this.message = message;
    }

    public void setName(string name)
    {
        this.name = name;
    }
}

public class EventPublisher : Publisher
{
    private List<Observer> observers = new List<Observer> { };

    public void subscriber(Observer o)
    {
        this.observers.Add(o);
    }

    public void unsubscriber(Observer o)
    {
        this.observers.Remove(o);
    }

    public void notify(string message)
    {
        foreach (var o in observers)
        {
            o.update(message);
        }
    }

    public void getInfoObserver()
    {
        foreach (var o in observers)
        {
            Console.WriteLine($"Subscriber: {o}");
        }
    }
}

public class Program
{
    static void Main()
    {

        LineNotifier line = new LineNotifier("L-13573", "NotifyMyHome");
        PhoneNotifier phone = new PhoneNotifier("0123456789");
        DiscordNotifier discord = new DiscordNotifier("D-1234", "NotifyMyHome");

        EventPublisher eventPublisher = new EventPublisher();

        AiCCTV aiCCTV = new AiCCTV("หน้าบ้าน", "AiCCTV-1", eventPublisher);

        eventPublisher.subscriber(line);
        eventPublisher.subscriber(phone);
        eventPublisher.subscriber(discord);

        eventPublisher.getInfoObserver();
        Console.WriteLine();

        aiCCTV.detect("คนใส่โม่ง");

        EventPublisher eventPublisher2 = new EventPublisher();
        AiCCTV aiCCTV2 = new AiCCTV("หลังบ้าน", "AiCCTV-2", eventPublisher2);
        eventPublisher2.subscriber(line);
        eventPublisher2.subscriber(phone);

        aiCCTV2.detect("แมว");

        Console.WriteLine();
        eventPublisher2.getInfoObserver();



        LineNotifier line2 = new LineNotifier("L-12345", "NotifyMyHome_1");
        eventPublisher.subscriber(line2);

        aiCCTV.detect("หมา");
        // aiCCTV2.detect("งู");
    }
}
