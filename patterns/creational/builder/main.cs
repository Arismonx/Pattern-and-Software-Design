using System;


// ====== Builder ======
public interface Builder
{
    void reset();
    void drawCircle();
    void drawSquare();
    void drawTriangle();
    void drawRectangle();
    void drawStar();
    void drawHeart();
}

// ====== Product ======
public class Artwork
{
    private string circle;
    private string square;
    private string triangle;
    private string rectangle;
    private string star;
    private string heart;

    public void setCircle(string circle)
    {
        this.circle = circle;
    }

    public void setSquare(string square)
    {
        this.square = square;
    }

    public void setTriangle(string triangle)
    {
        this.triangle = triangle;
    }

    public void setRectangle(string rectangle)
    {
        this.rectangle = rectangle;
    }

    public void setStar(string star)
    {
        this.star = star;
    }

    public void setHeart(string heart)
    {
        this.heart = heart;
    }

    // get

    public string getCircle()
    {
        return circle;
    }

    public string getSquare()
    {
        return square;
    }

    public string getTriangle()
    {
        return triangle;
    }

    public string getRectangle()
    {
        return rectangle;
    }

    public string getStar()
    {
        return star;
    }

    public string getHeart()
    {
        return heart;
    }

    public void showResult()
    {
        Console.WriteLine("---[Create Artwork with]---");
        if (!string.IsNullOrEmpty(circle)) Console.WriteLine($"- Circle: {circle}");
        if (!string.IsNullOrEmpty(square)) Console.WriteLine($"- square: {square}");
        if (!string.IsNullOrEmpty(triangle)) Console.WriteLine($"- triangle: {triangle}");
        if (!string.IsNullOrEmpty(rectangle)) Console.WriteLine($"- rectangle: {rectangle}");
        if (!string.IsNullOrEmpty(star)) Console.WriteLine($"- star: {star}");
        if (!string.IsNullOrEmpty(heart)) Console.WriteLine($"- heart: {heart}");
        Console.WriteLine("\n");
    }
}


// ====== Concrete Builder PaperBuilder ======
public class PaperBuilder : Builder
{
    private Artwork artwork;

    public PaperBuilder()
    {
        this.reset();
    }
    public void reset()
    {
        this.artwork = new Artwork();
    }
    public void drawCircle()
    {
        artwork.setCircle("Draw a Circle with a compass");
    }
    public void drawSquare()
    {
        artwork.setSquare("Draw a Square with a ruler, making sure all sides are equal");
    }
    public void drawTriangle()
    {
        artwork.setTriangle("Draw a Triangle with a ruler, Let there be only 3 corners");
    }
    public void drawRectangle()
    {
        artwork.setRectangle("Draw a Rectangle with a ruler");
    }
    public void drawStar()
    {
        artwork.setStar("Draw a Star or use a stencil");
    }
    public void drawHeart()
    {
        artwork.setHeart("Draw a Heart or use a stencil");
    }

    public Artwork getResult()
    {
        Artwork result = this.artwork;
        this.reset();

        return result;
    }
}


// ====== Concrete Builder DigitalBuilder ======
public class DigitalBuilder : Builder
{
    private Artwork artwork;

    public DigitalBuilder()
    {
        this.reset();
    }
    public void reset()
    {
        this.artwork = new Artwork();
    }
    public void drawCircle()
    {
        artwork.setCircle("Add a Circle to canvas");
    }
    public void drawSquare()
    {
        artwork.setSquare("Add a Square to canvas");
    }
    public void drawTriangle()
    {
        artwork.setTriangle("Add a Triangle to canvas");
    }
    public void drawRectangle()
    {
        artwork.setRectangle("Add a Rectangle to canvas");
    }
    public void drawStar()
    {
        artwork.setStar("Add a Star to canvas");
    }
    public void drawHeart()
    {
        artwork.setHeart("Add a Heart to canvas");
    }

    public Artwork getResult()
    {
        Artwork result = this.artwork;
        this.reset();

        return result;
    }
}

// ====== Director ======
public class Director
{
    public void drawHome(Builder builder)
    {
        builder.reset();
        builder.drawSquare();
        builder.drawTriangle();
        builder.drawRectangle();
        builder.drawCircle();
    }

    public void drawLetter(Builder builder)
    {
        builder.reset();
        builder.drawRectangle();
        builder.drawTriangle();
        builder.drawHeart();
    }

    public void drawChristmasDay(Builder builder)
    {
        builder.reset();
        builder.drawTriangle();
        builder.drawStar();
        builder.drawSquare();
        builder.drawRectangle();
        builder.drawCircle();
        builder.drawHeart();
    }
}

class Program
{   // ====== Client ======
    static void Client(Director director)
    {
        PaperBuilder paperBuilder = new PaperBuilder();
        DigitalBuilder digitalBuilder = new DigitalBuilder();

        Console.WriteLine("=====[PAPER]=====");

        Console.WriteLine("--- Draw Home in paper ---");
        director.drawHome(paperBuilder);
        Artwork artwork = paperBuilder.getResult();
        artwork.showResult();

        Console.WriteLine("--- Draw Letter in paper ---");
        director.drawLetter(paperBuilder);
        artwork = paperBuilder.getResult();
        artwork.showResult();

        Console.WriteLine("--- Draw Christmas Day in paper ---");
        director.drawChristmasDay(paperBuilder);
        artwork = paperBuilder.getResult();
        artwork.showResult();

        Console.WriteLine("=====[DIGITAL]=====");

        Console.WriteLine("--- Draw Home in digital ---");
        director.drawHome(digitalBuilder);
        artwork = digitalBuilder.getResult();
        artwork.showResult();

        Console.WriteLine("--- Draw Letter in digital ---");
        director.drawLetter(digitalBuilder);
        artwork = digitalBuilder.getResult();
        artwork.showResult();

        Console.WriteLine("--- Draw Christmas Day in digital ---");
        director.drawChristmasDay(digitalBuilder);
        artwork = digitalBuilder.getResult();
        artwork.showResult();

    }
    static void Main()
    {
        // Director director = new Director();
        // Client(director);

        // Console.WriteLine("--- Draw it yourself ---");
        // Artwork artwork = new Artwork();
        // artwork.setCircle("Draw circle");
        // artwork.setSquare("Draw Square");
        // artwork.setTriangle("Draw Triangle");
        // artwork.setRectangle("Draw Rectangle");
        // artwork.setStar("Draw Star");
        // artwork.setHeart("Draw Heart");

        // Console.WriteLine(artwork.getCircle());
        // Console.WriteLine(artwork.getSquare());
        // Console.WriteLine(artwork.getTriangle());
        // Console.WriteLine(artwork.getRectangle());
        // Console.WriteLine(artwork.getStar());
        // Console.WriteLine(artwork.getHeart());

        // artwork.showResult();

        Console.WriteLine("==== PaperBuilder in main =====");
        PaperBuilder paper = new PaperBuilder();
        paper.reset();
        paper.drawRectangle();
        paper.drawTriangle();
        paper.drawHeart();
        paper.drawCircle();
        paper.drawSquare();
        paper.drawStar();

        Artwork artwork = paper.getResult();
        artwork.showResult();

        Console.WriteLine("==== DigitalBuilder in main =====");
        DigitalBuilder digital = new DigitalBuilder();
        digital.reset();
        digital.drawRectangle();
        digital.drawTriangle();
        digital.drawHeart();
        digital.drawCircle();
        digital.drawSquare();
        digital.drawStar();

        artwork = digital.getResult();
        artwork.showResult();
    }
}