using System;

// ====== Implementation ======
public abstract class Ingredient
{
    private string cut;
    private int cost;

    public Ingredient(string cut, int cost)
    {
        this.cut = cut;
        this.cost = cost;
    }
    public abstract string prepare();
    public abstract int getTotalCost();

    public string getCut()
    {
        return cut;
    }

    public int getCost()
    {
        return cost;
    }
}

// ====== Concrete Implementations ======
public class Pork : Ingredient
{
    private int marinatedHours;
    public Pork(string cut, int cost, int marinatedHours) : base(cut, cost)
    {
        this.marinatedHours = marinatedHours;
    }

    public override string prepare()
    {
        return $"เตรียม เนื้อหมู ส่วน({getCut()}),หมัก({marinatedHours})ชั่วโมง";
    }

    public override int getTotalCost()
    {
        return getCost() + (marinatedHours * 2);
    }
}

public class Chicken : Ingredient
{

    private bool isChickenSkin;
    public Chicken(string cut, int cost, bool isChickenSkin) : base(cut, cost)
    {
        this.isChickenSkin = isChickenSkin;
    }
    public override string prepare()
    {
        if (isChickenSkin)
        {
            return $"เตรียม เนื้อไก่ ส่วน({getCut()}), เอาหนังไก่";
        }
        return $"เตรียม เนื้อไก่ ส่วน({getCut()}), ไม่เอาหนัง";
    }
    public override int getTotalCost()
    {
        if (!isChickenSkin)
        {
            return getCost() + 1;
        }
        return getCost();
    }
}

// ====== Abstraction ====== 

public abstract class FoodMenu
{
    protected Ingredient ingredient;
    protected string menuName;
    protected int basePrice;

    public FoodMenu(Ingredient ingredient, string menuName, int basePrice)
    {
        this.ingredient = ingredient;
        this.menuName = menuName;
        this.basePrice = basePrice;
    }

    public virtual int getTotalPrice()
    {
        return basePrice + ingredient.getTotalCost();
    }

    public void getBill()
    {
        Console.WriteLine($"====== Bill ({menuName}) ======");
        Console.WriteLine($"ราคา {menuName} : {basePrice} บาท");
        Console.WriteLine($"ราคาวัตถุดิบ : {ingredient.getTotalCost()} บาท");
        getInfo();
        Console.WriteLine($"ราคารวมทั้งหมด : {getTotalPrice()} บาท");
        Console.WriteLine("====================================================\n");
    }

    public abstract void cook();
    public virtual void getInfo() { } // Hook
}

// ====== Refined Abstractions ======
public class FriedRice : FoodMenu
{
    private string toppingName;
    private int toppingPrice;
    public FriedRice(Ingredient ingredient, string toppingName, int toppingPrice) : base(ingredient, "ข้าวผัด", 40)
    {
        this.toppingName = toppingName;
        this.toppingPrice = toppingPrice;
    }

    public override int getTotalPrice()
    {
        return base.getTotalPrice() + toppingPrice;
    }
    public override void getInfo()
    {
        Console.WriteLine($"Topping ({toppingName}) : {toppingPrice} บาท");
    }

    public override void cook()
    {
        Console.WriteLine("--- กำลังทำอาหาร ...");
        Console.WriteLine($"ผัดข้าวผัด กับ {ingredient.prepare()} ");
        Console.WriteLine($"เทใส่จาน แล้วตกแต่งด้วย {toppingName} ");
        Console.WriteLine("--- ทำเสร็จแล้ว! ...\n");
    }
}

public class Omelet : FoodMenu
{
    private int numberEggs;
    public Omelet(Ingredient ingredient, int numberEggs) : base(ingredient, "ไข่เจียว", 30)
    {
        this.numberEggs = numberEggs;
    }

    public override int getTotalPrice()
    {
        return base.getTotalPrice() + (numberEggs * 3);
    }
    public override void getInfo()
    {
        Console.WriteLine($"เพิ่มไข่ ({numberEggs}) ฟอง : {numberEggs * 3}");
    }

    public override void cook()
    {
        Console.WriteLine("--- กำลังทำอาหาร ...");
        Console.WriteLine($"ตอกไข่ {numberEggs + 1} ฟอง แล้วตีไข่ให้เข้ากัน");
        Console.WriteLine($"ทอดไข่เจียว กับ {ingredient.prepare()}");
        Console.WriteLine($"เทใส่จาน");
        Console.WriteLine("--- ทำเสร็จแล้ว!...\n");
    }
}

class Program
{
    static void Main()
    {
        Pork pork = new Pork("สันคอ", 20, 5);
        Chicken chicken = new Chicken("สะโพก", 15, false);

        FoodMenu order1 = new FriedRice(pork, "มะนาว", 3);
        order1.cook();
        order1.getBill();

        FoodMenu order2 = new FriedRice(chicken, "ผักกาดหอม", 5);
        order2.cook();
        order2.getBill();

        FoodMenu order3 = new Omelet(pork, 2);
        order3.cook();
        order3.getBill();

        FoodMenu order4 = new Omelet(chicken, 1);
        order4.cook();
        order4.getBill();
    }
}

