using System;
using System.Collections.Generic;

// ====== Client Interface ======
public interface GameBoyController
{
    string getCommand();
}

// ====== PCKeyboard Adaptee ======
public class PCKeyboard
{
    private string key;

    public void setKey(string key)
    {
        this.key = key;
    }
    public string getInputKey()
    {
        return key;
    }
}

// ====== PCKeyboard Adapter ======
public class PCKeyboardAdapter : GameBoyController
{
    private PCKeyboard adaptee;

    private Dictionary<string, string> keyCommand = new Dictionary<string, string>
    {
        { "J", "ATTACK" },
        { "K", "DODGE" },
        { "W", "JUMP" },
        { "S", "SQUAT" },
        { "A", "LEFT" },
        { "D", "RIGHT" },
        { "Z", "START" },
        { "X", "SELECT" }
    };

    public PCKeyboardAdapter(PCKeyboard adaptee)
    {
        this.adaptee = adaptee;
    }

    public string getCommand()
    {

        foreach (var key in keyCommand)
        {
            if (key.Key == adaptee.getInputKey())
            {
                return key.Value;
            }
        }
        return "IDEL";
    }

}

// ====== XboxController Adaptee ======
public class XboxController
{
    private string button;
    public void setButton(string button)
    {
        this.button = button;
    }
    public string getInputButton()
    {
        return button;
    }
}

// ====== XboxController Adapter ======
public class XboxControllerAdapter : GameBoyController
{
    private XboxController adaptee;
    private Dictionary<string, string> buttonCommand = new Dictionary<string, string>
    {
        { "A BUTTON", "ATTACK" },
        { "B BUTTON", "DODGE" },
        { "D-PAD UP", "JUMP" },
        { "D-PAD DOWN", "SQUAT" },
        { "D-PAD LEFT", "LEFT" },
        { "D-PAD RIGHT", "RIGHT" },
        { "X BUTTON", "START" },
        { "Y BUTTON", "SELECT" }
    };

    public XboxControllerAdapter(XboxController adaptee)
    {
        this.adaptee = adaptee;
    }

    public string getCommand()
    {
        foreach (var button in buttonCommand)
        {
            if (button.Key == adaptee.getInputButton())
            {
                return button.Value;
            }
        }
        return "IDLE";
    }

}

// ====== FightingGames ======
public class FightingGames
{
    private string gameName;
    private bool isRunning;
    private GameBoyController connectedController;

    public FightingGames(string gameName)
    {
        this.gameName = gameName;
        this.isRunning = false;
    }

    public void connectController(GameBoyController connectController)
    {
        this.connectedController = connectController;
        Console.WriteLine("Connected controller Complete!");
    }
    public void startGame()
    {
        if (connectedController == null)
        {
            Console.WriteLine("Please connect a controller!");
            return;
        }
        this.isRunning = true;
        Console.WriteLine($"====Start Game ({gameName})====");
    }

    public void update()
    {
        if (!isRunning)
        {
            return;
        }

        string action = connectedController.getCommand();

        switch (action)
        {
            case "START": Console.WriteLine(">> Player press START to play game!"); break;
            case "SELECT": Console.WriteLine(">> Player press SELECT for select Character!"); break;
            case "ATTACK": Console.WriteLine(">> Player ATTACK the enemy!"); break;
            case "DODGE": Console.WriteLine(">> Player DODGE the enemy!"); break;
            case "JUMP": Console.WriteLine(">> Player JUMP!"); break;
            case "SQUAT": Console.WriteLine(">> Player SQUAT!"); break;
            case "LEFT": Console.WriteLine(">> Player move LEFT!"); break;
            case "RIGHT": Console.WriteLine(">> Player move RIGHT!"); break;
            case "IDLE": Console.WriteLine(">> Player stay still, Nothing!"); break;
            default: Console.WriteLine(">> Unknown action!"); break;
        }
    }
}

class Program
{
    static void Main()
    {
        FightingGames fightingGames = new FightingGames("Street Fighter");

        Console.WriteLine("=== Use KeyBoard Play FightingGame ===");
        PCKeyboard pcKeyboard = new PCKeyboard();
        PCKeyboardAdapter pcKeyboardAdapter = new PCKeyboardAdapter(pcKeyboard);

        fightingGames.connectController(pcKeyboardAdapter);
        fightingGames.startGame();


        while (true)
        {
            string inputKey = Console.ReadLine();
            pcKeyboard.setKey(inputKey.ToUpper());

            if (inputKey.ToLower() == "exit") break;

            fightingGames.update();
        }


        Console.WriteLine("\n");

        Console.WriteLine("=== Use XBox Controller Play FightingGame ===");
        XboxController xboxController = new XboxController();
        XboxControllerAdapter xboxControllerAdapter = new XboxControllerAdapter(xboxController);

        fightingGames.connectController(xboxControllerAdapter);

        string[] inputButton = { "X BUTTON", "Y BUTTON", "A BUTTON", "B BUTTON", "D-PAD UP", "D-PAD DOWN", "D-PAD LEFT", "D-PAD RIGHT" };

        fightingGames.startGame();

        for (int i = 0; i < inputButton.Length; i++)
        {
            xboxController.setButton(inputButton[i]);
            fightingGames.update();
        }

    }
}