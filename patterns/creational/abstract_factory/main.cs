using System;
using System.Security.Cryptography.X509Certificates;

// ====== Abstract Factory ======
public interface ClothingFactory
{
    Shirt CreateShirt();
    Pants CreatePants();
    Shoe CreateShoe();
}

// ====== Concrete Factory Summer ======
public class SummerClothingFactory : ClothingFactory
{
    public Shirt CreateShirt()
    {
        return new SummerShirt();
    }

    public Pants CreatePants()
    {
        return new SummerPants();
    }

    public Shoe CreateShoe()
    {
        return new SummerShoe();
    }
}

// ====== Concrete Factory Winter ======
public class WinterClothingFactory : ClothingFactory
{
    public Shirt CreateShirt()
    {
        return new WinterShirt();
    }

    public Pants CreatePants()
    {
        return new WinterPants();
    }

    public Shoe CreateShoe()
    {
        return new WinterShoe();
    }
}

// ====== Concrete Factory Rains ======
public class RainsClothingFactory : ClothingFactory
{
    public Shirt CreateShirt()
    {
        return new RainsShirt();
    }

    public Pants CreatePants()
    {
        return new RainsPants();
    }

    public Shoe CreateShoe()
    {
        return new RainsShoe();
    }
}

// ====== Abstract Product Shirt ======
public interface Shirt
{
    void FabricType();
    void SleeveStyle();
}
// ====== Concrete Product Shrit ======
public class SummerShirt : Shirt
{
    public void FabricType()
    {
        Console.WriteLine("- Thin fabric ผ้าบาง");
    }
    public void SleeveStyle()
    {
        Console.WriteLine("- Short sleeve");
    }
}

public class WinterShirt : Shirt
{
    public void FabricType()
    {
        Console.WriteLine("- Thick fabric ผ้าหนา");
    }
    public void SleeveStyle()
    {
        Console.WriteLine("- Long sleeve");
    }
}

public class RainsShirt : Shirt
{
    public void FabricType()
    {
        Console.WriteLine("- Waterproof fabric");
    }
    public void SleeveStyle()
    {
        Console.WriteLine("- Long sleeve");
    }
}

// ====== Abstract Product Pants ======
public interface Pants
{
    void FabricType();
    void LengthStyle();
}

// ====== Concrete Product Pants ======
public class SummerPants : Pants
{
    public void FabricType()
    {
        Console.WriteLine("- Thin fabric");
    }
    public void LengthStyle()
    {
        Console.WriteLine("- Short sleeve");
    }
}

public class WinterPants : Pants
{
    public void FabricType()
    {
        Console.WriteLine("- Thick fabric");
    }
    public void LengthStyle()
    {
        Console.WriteLine("- Long sleeve");
    }
}

public class RainsPants : Pants
{
    public void FabricType()
    {
        Console.WriteLine("- Waterproof fabric");
    }
    public void LengthStyle()
    {
        Console.WriteLine("- Long sleeve");
    }
}

// ====== Abstract Product Shoe ======
public interface Shoe
{
    void ShoeType();
    void WaterResistant();
}

// ====== Concrete Product Shoe ======
public class SummerShoe : Shoe
{
    public void ShoeType()
    {
        Console.WriteLine("- sandals");
    }
    public void WaterResistant()
    {
        Console.WriteLine("- water resistant");
    }
}

public class WinterShoe : Shoe
{
    public void ShoeType()
    {
        Console.WriteLine("- snow boots");
    }
    public void WaterResistant()
    {
        Console.WriteLine("- water resistant");
    }
}

public class RainsShoe : Shoe
{
    public void ShoeType()
    {
        Console.WriteLine("- boots");
    }
    public void WaterResistant()
    {
        Console.WriteLine("- water resistant");
    }
}

public class Client
{
    private ClothingFactory factory;
    private Shirt shirt;
    private Pants pants;
    private Shoe shoe;

    public Client(ClothingFactory f)
    {
        factory = f;
    }
    public void ShowResult()
    {

        shirt = factory.CreateShirt();
        pants = factory.CreatePants();
        shoe = factory.CreateShoe();

        Console.WriteLine("== Shirt ==");

        shirt.FabricType();
        shirt.SleeveStyle();

        Console.WriteLine("== Pants ==");

        pants.FabricType();
        pants.LengthStyle();

        Console.WriteLine("== Shoe ==");

        shoe.ShoeType();
        shoe.WaterResistant();
    }
}

class Program
{
    static void Main(string[] args)
    {
        SummerClothingFactory summer = new SummerClothingFactory();
        WinterClothingFactory winter = new WinterClothingFactory();
        RainsClothingFactory rains = new RainsClothingFactory();

        Console.WriteLine("Summer Factory");
        Console.WriteLine("--------------");
        Client client1 = new Client(summer);
        client1.ShowResult();

        Console.WriteLine();

        Console.WriteLine("Winter Factory");
        Console.WriteLine("--------------");
        Client client2 = new Client(winter);
        client2.ShowResult();

        Console.WriteLine();

        Console.WriteLine("Rains Factory");
        Console.WriteLine("--------------");
        Client client3 = new Client(rains);
        client3.ShowResult();

        Console.WriteLine();

        Console.WriteLine("--- Call In Main ---");

        Console.WriteLine("== Case 1");
        SummerClothingFactory summer1 = new SummerClothingFactory();
        Shirt shirt = summer1.CreateShirt();
        shirt.FabricType();
        shirt.SleeveStyle();

        Console.WriteLine("== Case 2");
        SummerShirt summerShirt = new SummerShirt();
        summerShirt.FabricType();
        summerShirt.SleeveStyle();

        Console.WriteLine("== Case 3");
        Shirt shirt2 = summerShirt; // shirt2 = summerShirt ทั้งสองคือตัวเดียวกัน shirt2 เลยสามารถเรียใช้ method ได้
        shirt2.FabricType();
        shirt2.SleeveStyle();
    }
}
