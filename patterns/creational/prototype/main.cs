using System;

// ====== Prototype ======
public abstract class Cookie
{
    private int id;
    private string flour;
    private string sugar;
    private string butter;

    public Cookie(int id, string flour, string sugar, string butter)
    {
        this.id = id;
        this.flour = flour;
        this.sugar = sugar;
        this.butter = butter;
    }

    // Copy Constructor
    public Cookie(Cookie source)
    {
        this.id = source.id;
        this.flour = source.flour;
        this.sugar = source.sugar;
        this.butter = source.butter;
    }

    public abstract Cookie Clone();
    public int GetId()
    {
        return this.id;
    }

    public virtual void GetInfo()
    {
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"Flour: {flour}");
        Console.WriteLine($"Sugar: {sugar}");
        Console.WriteLine($"Butter: {butter}");
    }
}

// ====== Concrete Prototype ChocolateCookie ======
public class ChocolateCookie : Cookie
{
    private string chocolate;
    private string cocoaPowder;


    public ChocolateCookie(
            int id,
            string flour,
            string sugar,
            string butter,
            string chocolate,
            string cocoaPowder) : base(id, flour, sugar, butter)
    {
        this.chocolate = chocolate;
        this.cocoaPowder = cocoaPowder;
    }

    public ChocolateCookie(ChocolateCookie source) : base(source)
    {
        this.chocolate = source.chocolate;
        this.cocoaPowder = source.cocoaPowder;
    }

    public override Cookie Clone()
    {
        return new ChocolateCookie(this);
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Chocolate: {chocolate}");
        Console.WriteLine($"CocoaPowder: {cocoaPowder}");
    }
}

// ====== Concrete Prototype OatmealCookie ======
public class OatmealCookie : Cookie
{
    private string oatmeal;

    public OatmealCookie(
            int id,
            string flour,
            string sugar,
            string butter,
            string oatmeal) : base(id, flour, sugar, butter)
    {
        this.oatmeal = oatmeal;
    }

    public OatmealCookie(OatmealCookie source) : base(source)
    {
        this.oatmeal = source.oatmeal;
    }

    public override Cookie Clone()
    {
        return new OatmealCookie(this);
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Oatmeal: {oatmeal}");
    }
}

// ====== SubClass Prototype OatmealRaisinCookie ======
public class OatmealRaisinCookie : OatmealCookie
{
    private string raisin;

    public OatmealRaisinCookie(
            int id,
            string flour,
            string sugar,
            string butter,
            string oatmeal,
            string raisin) : base(id, flour, sugar, butter, oatmeal)
    {
        this.raisin = raisin;
    }

    public OatmealRaisinCookie(OatmealRaisinCookie source) : base(source)
    {
        this.raisin = source.raisin;
    }

    public override Cookie Clone()
    {
        return new OatmealRaisinCookie(this);
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Raisin: {raisin}");
    }
}

// ====== Cookie Registry ======
public class CookieRegistry
{
    private Cookie[] items;
    private int count;

    public CookieRegistry()
    {
        this.items = new Cookie[4];
        this.count = 0;
    }
    public void AddCookie(Cookie c)
    {
        items[count] = c;
        count++;
    }

    public Cookie GetCookieById(int id)
    {
        for (int i = 0; i < count; i++)
        {
            if (items[i].GetId() == id)
            {
                return items[i].Clone();
            }
        }
        return null;
    }

}

class Program
{
    static void Client(CookieRegistry registry)
    {
        ChocolateCookie chocolateCookie = new ChocolateCookie(1, "dough", "sugar", "butter", "chocolate", "cocoa powder");
        OatmealCookie oatmealCookie = new OatmealCookie(2, "dough", "brown sugar", "butter", "oatmeal");

        //subclass oatmealCookie
        OatmealRaisinCookie oatmealRaisinCookie = new OatmealRaisinCookie(3, "dough", "brown sugar", "butter", "oatmeal", "raisin");


        registry.AddCookie(chocolateCookie);
        registry.AddCookie(oatmealCookie);
        registry.AddCookie(oatmealRaisinCookie);

        // Get All ID
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"=== Create a copy {i} ===");
            Cookie cookie = registry.GetCookieById(i);
            cookie.GetInfo();
        }

        // Get ID 3
        Console.WriteLine($"=== Get ID (3) ===");
        Cookie cookie1 = registry.GetCookieById(3);
        cookie1.GetInfo();

    }
    static void Main()
    {
        CookieRegistry cookieRegistry = new CookieRegistry();
        // Client(cookieRegistry);

        OatmealCookie oatmealCookie = new OatmealCookie(1, "dough", "brown sugar", "butter", "oatmeal");

        // cookieRegistry.AddCookie(oatmealCookie);

        for (int i = 0; i < 10; i++)
        {
            // Cookie cookie = cookieRegistry.GetCookieById(1);
            Cookie cookie = oatmealCookie.Clone();
            Console.WriteLine($"========({i + 1})=========");
            cookie.GetInfo();

        }
    }
}