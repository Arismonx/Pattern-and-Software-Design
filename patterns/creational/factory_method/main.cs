using System;

// ====== Product Coffee ======
public interface Coffee
{
    void BoilWater();
    void BrewEspresso();
    void Pour();
    void AddIngredients();
}

// ====== Concrete Product Latte ======
public class Latte : Coffee
{
    public void BoilWater()
    {
        Console.WriteLine("- Boil Water");
    }
    public void BrewEspresso()
    {
        Console.WriteLine("- Espresso is made by pouring water over coffee grounds, distilled from ground beans. ");
    }
    public void Pour()
    {
        Console.WriteLine("- Pour into coffee");
    }
    public void AddIngredients()
    {
        Console.WriteLine("- 1. Add Espresso");
        Console.WriteLine("- 2. Add Hot Milk ");
        Console.WriteLine("- 3. Add milk foam");
    }
}

// ====== Concrete Product Americano ======
public class Americano : Coffee
{
    public void BoilWater()
    {
        Console.WriteLine("- Boil Water");
    }
    public void BrewEspresso()
    {
        Console.WriteLine("- Espresso is made by pouring water over coffee grounds, distilled from ground beans. ");
    }
    public void Pour()
    {
        Console.WriteLine("- Pour into coffee");
    }
    public void AddIngredients()
    {
        Console.WriteLine("- 1. Add Espresso");
        Console.WriteLine("- 2. Add Hot Water");
    }
}

// ====== Concrete Product Cappuccino ======
public class Cappuccino : Coffee
{
    public void BoilWater()
    {
        Console.WriteLine("- Boil Water");
    }
    public void BrewEspresso()
    {
        Console.WriteLine("- Espresso is made by pouring water over coffee grounds, distilled from ground beans. ");
    }
    public void Pour()
    {
        Console.WriteLine("- Pour into coffee");
    }
    public void AddIngredients()
    {
        Console.WriteLine("- 1. Add Espresso");
        Console.WriteLine("- 2. Add Hot Milk");
        Console.WriteLine("- 3. Add milk foam");
        Console.WriteLine("- 4. Add Cinnamon Powder");
    }
}

// ====== Creator MenuCoffee ======
public abstract class MenuCoffee
{
    public abstract Coffee GetCoffee();
    public void MakeCoffee()
    {
        Coffee coffee = GetCoffee();
        coffee.BoilWater();
        coffee.BrewEspresso();
        coffee.Pour();
        coffee.AddIngredients();
    }
}

// ====== Concrete Creator MakeLatte ======
public class MakeLatte : MenuCoffee
{
    public override Coffee GetCoffee()
    {
        return new Latte();
    }
}

// ====== Concrete Creator MakeAmericano ======
public class MakeAmericano : MenuCoffee
{
    public override Coffee GetCoffee()
    {
        return new Americano();
    }
}

// ====== Concrete Creator MakeCappuccino ======
public class MakeCappuccino : MenuCoffee
{
    public override Coffee GetCoffee()
    {
        return new Cappuccino();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MakeLatte latte = new MakeLatte();
        latte.MakeCoffee();

        Console.WriteLine("-----------------------------");

        MakeAmericano americano = new MakeAmericano();
        americano.MakeCoffee();

        Console.WriteLine("-----------------------------");

        MakeCappuccino cappuccino = new MakeCappuccino();
        cappuccino.MakeCoffee();


        Console.WriteLine("---- Call in main ----");
        MakeLatte latte1 = new MakeLatte();
        Coffee coffee = latte1.GetCoffee();

        coffee.BoilWater();
        coffee.BrewEspresso();
        coffee.Pour();
        coffee.AddIngredients();

    }
}