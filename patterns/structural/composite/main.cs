using System;

// ====== Component ======
public interface BagItem
{
    float getWeight();
    void getDetail();

}

// ======  Leaf ======
public abstract class Item : BagItem
{
    protected float weight;
    protected string name;

    public Item(float weight, string name)
    {
        this.weight = weight;
        this.name = name;
    }

    public float getWeight()
    {
        return weight;
    }

    public abstract void getDetail();

}

// ======  Concrete Leaf Book ======
public class Book : Item
{
    public Book(float weight, string name) : base(weight, name) { }

    public override void getDetail()
    {
        Console.WriteLine($"- Book: {name} ({weight} kg)");
    }
}

// ======  Concrete Leaf Pencil ======
public class Pencil : Item
{
    public Pencil(float weight, string name) : base(weight, name) { }

    public override void getDetail()
    {
        Console.WriteLine($"- Pencil: {name} ({weight} kg)");
    }
}

// ======  Concrete Leaf IPad ======
public class IPad : Item
{
    public IPad(float weight, string name) : base(weight, name) { }

    public override void getDetail()
    {
        Console.WriteLine($"- IPad: {name} ({weight} kg)");
    }
}

// ======  composite ======
public class Backpack : BagItem
{
    private BagItem[] items;
    private string name;
    private int count;

    public Backpack(string name, int capacity)
    {
        this.name = name;
        this.items = new BagItem[capacity];
        this.count = 0;
    }

    public void addItem(BagItem item)
    {
        if (count < items.Length)
        {
            items[count] = item;
            count++;
        }
        else
        {
            Console.WriteLine($"{name} is full!");
        }
    }

    public float getWeight()
    {
        float totalWeight = 0;

        for (int i = 0; i < count; i++)
        {
            totalWeight += items[i].getWeight();
        }
        return totalWeight;
    }
    public void getDetail()
    {
        Console.WriteLine($"\n[{name}] contains:");

        for (int i = 0; i < count; i++)
        {
            items[i].getDetail();
        }

        Console.WriteLine($"Total Weight of {name}: {getWeight()} kg\n");
    }
}

class Program
{
    static void Main()
    {
        // Leaf
        IPad ipad = new IPad(0.45f, "IPad Air 4");

        Book pyBook = new Book(0.54f, "PythonBook");
        Book csBook = new Book(0.65f, "C#Book");

        Pencil pencil2B = new Pencil(0.01f, "2B Pencil");

        // Composite
        Backpack pencilBag = new Backpack("PencilBag", 5);
        pencilBag.addItem(pencil2B);

        Backpack pocket = new Backpack("Pocket", 3);
        pocket.addItem(pyBook);
        pocket.addItem(csBook);
        pocket.addItem(pencilBag);

        Backpack backpack = new Backpack("Backpack", 10);
        backpack.addItem(ipad);
        backpack.addItem(pocket);

        backpack.getDetail();

        Console.WriteLine(backpack.getWeight());
    }

}