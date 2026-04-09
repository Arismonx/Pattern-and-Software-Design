using System;

// ====== Command ======
public interface Command
{
    void execute();
}

// ===== Concrete Command ======
public class TimeSetCommand : Command
{
    private MinecraftWorld minecraftWorld;
    private int time;

    public TimeSetCommand(MinecraftWorld minecraftWorld, int time)
    {
        this.minecraftWorld = minecraftWorld;
        this.time = time;
    }

    public void execute()
    {
        minecraftWorld.changeTime(time);
    }
}

public class WeatherCommand : Command
{
    private MinecraftWorld minecraftWorld;
    private string weather;

    public WeatherCommand(MinecraftWorld minecraftWorld, string weather)
    {
        this.minecraftWorld = minecraftWorld;
        this.weather = weather;
    }

    public void execute()
    {
        minecraftWorld.changeWeather(weather);
    }
}


// ====== Receiver ======
public class MinecraftWorld
{
    private string name;
    private Dictionary<int, string> timeList = new Dictionary<int, string>
    {
        { 1000,"7 AM" },
        { 6000,"12 PM" },
        { 12000,"6 PM" },
        { 18000,"12 AM" },
        { 23000,"5 AM" }
    };

    private List<string> weatherList = new List<string>
    {
        "clear","rain"
    };

    public MinecraftWorld(string name)
    {
        this.name = name;
    }

    public void addTimeList(int key, string value)
    {
        timeList.Add(key, value);
    }

    public void deleteTimeList(int key)
    {
        foreach (var t in timeList)
        {
            if (t.Key == key)
            {
                timeList.Remove(key);
            }
        }
    }

    public void getInfoTimeList()
    {
        foreach (var t in timeList)
        {
            Console.WriteLine($"time code: {t.Key}, time:{t.Value}");
        }
    }

    public void addWeatherList(string weather)
    {
        weatherList.Add(weather);
    }

    public void deleteWeather(string weather)
    {
        weatherList.Remove(weather);
    }

    public void getInfoWeatherList()
    {
        foreach (var w in weatherList)
        {
            Console.WriteLine($"weather: {w}");
        }
    }

    public void changeTime(int time)
    {
        foreach (var t in timeList)
        {
            if (t.Key == time)
            {
                Console.WriteLine($">>เปลี่ยนเวลาในโลก {name} เป็นเวลา: {t.Value}");
            }
        }
    }

    public void changeWeather(string weather)
    {
        string weatherLow = weather.ToLower();

        foreach (var w in weatherList)
        {
            if (w == weatherLow)
            {
                Console.WriteLine($">>เปลี่ยนสภาพอากาศเป็น: {weatherLow}");
            }
        }
    }
}

// ====== Invoker ======

public class GameConsole
{
    private Command command;
    public void executeCommand()
    {
        command.execute();
    }
    public void setCommand(Command command)
    {
        this.command = command;
    }
}

public class Program
{
    static void Main()
    {
        GameConsole console = new GameConsole();
        MinecraftWorld serverWorld = new MinecraftWorld("Hello World");

        TimeSetCommand timeSet = new TimeSetCommand(serverWorld, 12000);
        console.setCommand(timeSet);
        console.executeCommand();

        WeatherCommand weatherSet = new WeatherCommand(serverWorld, "rain");
        console.setCommand(weatherSet);
        console.executeCommand();

        Console.WriteLine();

        // try addTime and add Weather
        Console.WriteLine("-- befor add new time --");
        serverWorld.getInfoTimeList();
        serverWorld.addTimeList(3000, "9 AM");
        Console.WriteLine("\n-- After add new time --");
        serverWorld.getInfoTimeList();

        Console.WriteLine("\n-- Befor add new weather --");
        serverWorld.getInfoWeatherList();
        serverWorld.addWeatherList("thunder");
        Console.WriteLine("\n-- After add new weather --\n");
        serverWorld.getInfoWeatherList();

        Console.WriteLine("-------------------");
        TimeSetCommand timeSet2 = new TimeSetCommand(serverWorld, 3000);
        console.setCommand(timeSet2);
        console.executeCommand();

        Console.WriteLine("-------------------");
        WeatherCommand weatherSet2 = new WeatherCommand(serverWorld, "thunder");
        console.setCommand(weatherSet2);
        console.executeCommand();

        GameConsole mobile = new GameConsole();
        WeatherCommand weatherClear = new WeatherCommand(serverWorld, "clear");
        mobile.setCommand(weatherClear);
        mobile.executeCommand();

    }
}