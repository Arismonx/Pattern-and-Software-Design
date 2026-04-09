using System;

//--- visitor
public interface Visitor
{
    void visitorOre(Ore o);
    void visitorWood(Wood w);
}

//--- concrete visitor
public class ArmorCrafter : Visitor
{
    private string name;
    private int levelCrafter;

    public ArmorCrafter(string name, int levelCrafter)
    {
        this.name = name;
        this.levelCrafter = levelCrafter;
    }

    public void visitorOre(Ore o)
    {
        this.showInfoCrafter();
        Console.WriteLine($"วัตถุดิบที่ใช้สร้างชุดเกราะ: {o.getName()} (ความบริสุทธิ์ {o.getPurity()}%) (พลังแฝง {o.getMagicElement()})");

        Console.WriteLine($">>[ช่าง {name}]: กำลังคราฟชุดเกราะ...");
        if (levelCrafter >= 50 && o.getPurity() >= 80)
        {
            Console.WriteLine($">>ได้รับ ชุดเกราะ {o.getName()} (พลังแฝง {o.getMagicElement()}) ระดับตำนาน!!!");
        }
        else
        {
            Console.WriteLine($">>ได้รับ ชุดเกราะ {o.getName()} (พลังแฝง {o.getMagicElement()}) ระดับปกติ");
        }
    }

    public void visitorWood(Wood w)
    {
        this.showInfoCrafter();
        Console.WriteLine($"วัตถุดิบที่ใช้สร้างชุดเกราะ: {w.getName()} (จากป่า {w.getZone()})");

        Console.WriteLine($">>[ช่าง {name}]: กำลังคราฟชุดเกราะ...");
        if (levelCrafter >= 80)
        {
            Console.WriteLine($">>ได้รับ ชุดเกราะ {w.getName()} ระดับตำนาน!!!");
        }
        else
        {
            Console.WriteLine($">>ได้รับ ชุดเกราะ {w.getName()} ระดับปกติ");
        }
    }

    public void showInfoCrafter()
    {
        Console.WriteLine(">>---Info---<<");
        Console.WriteLine("- ช่างชื่อ: " + name);
        Console.WriteLine("- เลเวลของช่าง: " + levelCrafter);
    }

    //--set
    public void setName(string name)
    {
        this.name = name;
    }

    public void setLevelCrafter(int levelCrafter)
    {
        this.levelCrafter = levelCrafter;
    }

    //--get
    public string getName()
    {
        return name;
    }

    public int getLevelCrafter()
    {
        return levelCrafter;
    }
}

public class WeaponCrafter : Visitor
{
    private string name;
    private int levelCrafter;

    public WeaponCrafter(string name, int levelCrafter)
    {
        this.name = name;
        this.levelCrafter = levelCrafter;
    }

    public void visitorOre(Ore o)
    {
        this.showInfoCrafter();
        Console.WriteLine($"วัตถุดิบที่ใช้สร้างอาวุธ: {o.getName()} (ความบริสุทธิ์ {o.getPurity()}%) (พลังแฝง {o.getMagicElement()})");
        Console.WriteLine($">>[ช่าง {name}]: กำลังคราฟอาวุธ...");
        if (levelCrafter >= 70 && o.getPurity() >= 90)
        {
            Console.WriteLine($">>ได้รับ อาวุธ {o.getName()} (พลังแฝง {o.getMagicElement()}) ได้ระดับตำนาน!!!");
        }
        else
        {
            Console.WriteLine($">>ได้รับ อาวุธ {o.getName()} (พลังแฝง {o.getMagicElement()}) ระดับปกติ");
        }
    }
    public void visitorWood(Wood w)
    {
        this.showInfoCrafter();
        Console.WriteLine($"วัตถุดิบที่ใช้สร้างอาวุธ: {w.getName()} (จากป่า {w.getZone()})");
        Console.WriteLine($">>[ช่าง {name}]: กำลังคราฟอาวุธ...");
        if (levelCrafter >= 80)
        {
            Console.WriteLine($">>ได้รับ อาวุธ {w.getName()} ระดับตำนาน!!!");
        }
        else
        {
            Console.WriteLine($">>ได้รับ อาวุธ {w.getName()} ระดับปกติ");
        }
    }

    public void showInfoCrafter()
    {
        Console.WriteLine(">>---Info---<<");
        Console.WriteLine("- ช่างชื่อ: " + name);
        Console.WriteLine("- เลเวลของช่าง: " + levelCrafter);
    }

    //--set
    public void setName(string name)
    {
        this.name = name;
    }

    public void setLevelCrafter(int levelCrafter)
    {
        this.levelCrafter = levelCrafter;
    }

    //--get
    public string getName()
    {
        return name;
    }

    public int getLevelCrafter()
    {
        return levelCrafter;
    }
}


//--- Element
public interface Material
{
    void accept(Visitor v);
}

//--- concrete class
public class Ore : Material
{
    private string name;
    private string magicElement;
    private int purity;

    public Ore(string name, string MagicElement, int purity)
    {
        this.name = name;
        this.magicElement = MagicElement;
        this.purity = purity;
    }

    public void accept(Visitor v)
    {
        v.visitorOre(this);
    }

    //--set
    public void setName(string name)
    {
        this.name = name;
    }

    public void setMagicElement(string magicElement)
    {
        this.magicElement = magicElement;
    }

    public void setPurity(int purity)
    {
        this.purity = purity;
    }

    //--get
    public string getName()
    {
        return name;
    }

    public string getMagicElement()
    {
        return magicElement;
    }

    public int getPurity()
    {
        return purity;
    }
}

public class Wood : Material
{
    private string name;
    private string zone;

    public Wood(string name, string zone)
    {
        this.name = name;
        this.zone = zone;
    }

    public void accept(Visitor v)
    {
        v.visitorWood(this);
    }

    //--set
    public void setName(string name)
    {
        this.name = name;
    }

    public void setZone(string zone)
    {
        this.zone = zone;
    }

    //--get
    public string getName()
    {
        return name;
    }

    public string getZone()
    {
        return zone;
    }
}


public class Program
{
    static void Client(List<Material> materials, Visitor visitor)
    {
        foreach (Material m in materials)
        {
            m.accept(visitor);
            Console.WriteLine();
        }
    }
    static void Main()
    {
        List<Material> materials = new List<Material> { };
        materials.Add(new Ore("gold", "fire", 98));
        materials.Add(new Wood("Pine Wood", "snow"));
        materials.Add(new Ore("sliver", "thunder", 70));

        Visitor weaponCrafter = new WeaponCrafter("Jame", 80);
        Client(materials, weaponCrafter);

        Visitor armorCrafter = new ArmorCrafter("Johny", 40);
        Client(materials, armorCrafter);
    }
}
