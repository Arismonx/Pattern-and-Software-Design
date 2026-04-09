using System;

// ====== Component ======
public interface ChristmasTree
{
    string getDetail();
    int getCost();

}

// ====== Concrete Component ======
public class PineTree : ChristmasTree
{
    private string size;
    private int costPinetree;

    public PineTree(string size, int costPinetree)
    {
        this.size = size;
        this.costPinetree = costPinetree;
    }

    public string getDetail()
    {
        return "Pine Tree" + $"({size})";
    }

    public int getCost()
    {
        return costPinetree;
    }

}

// ====== Base Decorator ======

public abstract class TreeDecorator : ChristmasTree
{
    protected ChristmasTree christmasTree;
    private int costDecorator;

    public TreeDecorator(ChristmasTree christmasTree, int costDecorator)
    {
        this.christmasTree = christmasTree;
        this.costDecorator = costDecorator;
    }

    public virtual string getDetail()
    {
        return christmasTree.getDetail();
    }

    public virtual int getCost()
    {
        return christmasTree.getCost();
    }

    public int getCostDecorator()
    {
        return costDecorator;
    }
}

// ====== Concrete Decorators ======

public class BallOrnament : TreeDecorator
{
    private int numberBalls;
    public BallOrnament(ChristmasTree c, int cost, int numberBalls) : base(c, cost)
    {
        this.numberBalls = numberBalls;
    }

    public override string getDetail()
    {
        return base.getDetail() + $" Ball Ornament({numberBalls}) ";
    }

    public override int getCost()
    {
        return base.getCost() + (numberBalls * getCostDecorator());
    }
}

public class FireLight : TreeDecorator
{
    private int numberStrands;
    public FireLight(ChristmasTree c, int cost, int numberStrands) : base(c, cost)
    {
        this.numberStrands = numberStrands;
    }

    public override string getDetail()
    {
        return base.getDetail() + $" Fire Light({numberStrands}) ";
    }

    public override int getCost()
    {
        return base.getCost() + (getCostDecorator() * numberStrands);
    }
}

public class StarTop : TreeDecorator
{
    private string color;
    public StarTop(ChristmasTree c, int cost, string color) : base(c, cost)
    {
        this.color = color;
    }

    public override string getDetail()
    {
        return base.getDetail() + $" Star Top({color}) ";
    }

    public override int getCost()
    {
        return base.getCost() + getCostDecorator();
    }
}

class Program
{
    static void Client(ChristmasTree c)
    {
        Console.WriteLine(c.getDetail());
    }
    static void Main()
    {
        ChristmasTree pineTree = new PineTree("60CM", 400);
        ChristmasTree pineTreeWithBallOrnament = new BallOrnament(pineTree, 10, 5);
        ChristmasTree pineTreeWithBallOrnamentWithFireLight = new FireLight(pineTreeWithBallOrnament, 30, 2);
        ChristmasTree pineTreeWithBallOrnamentWithFireLightAndStarTop = new StarTop(pineTreeWithBallOrnamentWithFireLight, 70, "RED");

        Console.WriteLine("Detail: " + pineTreeWithBallOrnamentWithFireLightAndStarTop.getDetail());
        Console.WriteLine("Total Cost: " + pineTreeWithBallOrnamentWithFireLightAndStarTop.getCost());

        Console.WriteLine();
        Client(pineTree);
        Client(pineTreeWithBallOrnament);
        Client(pineTreeWithBallOrnamentWithFireLight);
        Client(pineTreeWithBallOrnamentWithFireLightAndStarTop);

        Console.WriteLine();
        // ลองสลับตำแหน่ง
        ChristmasTree pineTree2 = new PineTree("120CM", 600);
        ChristmasTree treeWithFire = new FireLight(pineTree2, 30, 2);
        ChristmasTree treeWithFireWithStar = new StarTop(treeWithFire, 120, "Yellow");
        ChristmasTree treeWithFireWithStarAndBall = new BallOrnament(treeWithFireWithStar, 10, 10);
        ChristmasTree treeWithFireWithStarAndBall2 = new BallOrnament(treeWithFireWithStarAndBall, 10, 1);

        Console.WriteLine("Detail : " + treeWithFireWithStarAndBall2.getDetail());
        Console.WriteLine("Total Cost: " + treeWithFireWithStarAndBall2.getCost());

    }
}