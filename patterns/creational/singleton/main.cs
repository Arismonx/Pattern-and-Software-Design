using System;

public class MailBox
{
    private static MailBox instance;
    private string[] letter;
    private int count;
    private MailBox()
    {
        letter = new string[3];
        count = 0;
        Console.WriteLine("- Create MailBox!!");
    }

    public void AddLetter(string message)
    {
        letter[count] = message;
        count++;
    }

    public void CheckMailBox()
    {
        Console.WriteLine("===== [@] Letter =====");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"ID: {i} , Body: {letter[i]}");
        }
    }

    public static MailBox GetInstance()
    {
        if (instance == null)
        {
            instance = new MailBox();
        }

        return instance;
    }
}

class Program
{
    static void Main()
    {
        MailBox mailBox = MailBox.GetInstance();
        mailBox.AddLetter("Hello Tuschy!");
        mailBox.AddLetter("Electricity Bill");

        mailBox.CheckMailBox();

        // test 
        MailBox mailBox2 = MailBox.GetInstance();

        mailBox2.AddLetter("Water Bill");

        mailBox.CheckMailBox();

        mailBox2.CheckMailBox();
    }
}