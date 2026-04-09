using System;

//--- abstract class
public abstract class WebScraperTemplate
{
    private string saveFormat;
    private string target;
    private int timeDelay;
    private string filename;
    public WebScraperTemplate(string saveFormat,
                              string target,
                              int timeDelay,
                              string filename)
    {
        this.saveFormat = saveFormat;
        this.target = target;
        this.timeDelay = timeDelay;
        this.filename = filename;
    }

    protected void connect(string url)
    {
        Console.WriteLine(">> Connect: " + url);
    }

    protected void delay()
    {
        Console.WriteLine($">>Time Delay: {getTimeDelay()} sec.");
    }

    protected abstract void extractData();
    protected abstract void cleanData();
    protected virtual void analyzeData() { }// hook
    protected abstract void saveData();

    protected void closeConnection()
    {
        Console.WriteLine(">> Close Connection!");
    }
    protected virtual void sendNotify() { } // Hook

    public void runScript(string url)
    {
        this.connect(url);
        this.delay();
        this.extractData();
        this.cleanData();
        this.analyzeData();
        this.saveData();
        this.closeConnection();
        this.sendNotify();
    }

    // -- set 
    public void setSaveformat(string saveFormat)
    {
        this.saveFormat = saveFormat;
    }

    public void setTarget(string target)
    {
        this.target = target;
    }

    public void setTimeDelay(int timeDelay)
    {
        this.timeDelay = timeDelay;
    }

    public void setFilename(string filename)
    {
        this.filename = filename;
    }

    // -- get 
    public string getSaveformat()
    {
        return saveFormat;
    }

    public string getTarget()
    {
        return target;
    }

    public int getTimeDelay()
    {
        return timeDelay;
    }

    public string getFilename()
    {
        return filename;
    }
}

// --- Subclass 
public class ShoppingScraper : WebScraperTemplate
{
    private int limitItems;
    private bool isDiscounted;

    public ShoppingScraper(string saveFormat,
                           string target,
                           int timeDelay,
                           int limitItems,
                           bool isDiscounted,
                           string filename) :
                           base(saveFormat, target, timeDelay, filename)
    {
        this.limitItems = limitItems;
        this.isDiscounted = isDiscounted;
    }

    protected override void extractData()
    {
        Console.WriteLine($">>สกัดเอาส่วน: {getTarget()} ({limitItems} ชิ้น)");
        if (isDiscounted)
        {
            Console.WriteLine(">>สกัดเอาข้อมูลที่ สินค้ากำลังลดราคา Flash Sale");
        }
    }

    protected override void cleanData()
    {
        Console.WriteLine(">>ทำความสะอาดข้อมูล: ลบช่องว่าง,ตัดส่วนที่ราคาที่มี '฿'");
    }

    protected override void analyzeData()
    {
        Console.WriteLine(">>วิเคราะห์ข้อมูล: ราคาที่มากที่สุด-น้อยสุด,หาค่าเฉลี่ยของสินค้า: " + limitItems + " ชิ้น");
    }

    protected override void saveData()
    {
        Console.WriteLine(">>จัดการข้อมูลสินค้า " + limitItems + " ชิ้นในรูปแบบตาราง");
        Console.WriteLine(">>บันทึกข้อมูล: " + getFilename() + "." + getSaveformat());
    }

    protected override void sendNotify()
    {
        Console.WriteLine(">>ส่งแจ้งเตือนผ่าน ไลน์ ว่าสกัดข้อมูลสำเร็จแล้ว");
    }

    //--- set
    public void setLimitItems(int limitItems)
    {
        this.limitItems = limitItems;
    }

    public void setIsDiscounted(bool isDiscounted)
    {
        this.isDiscounted = isDiscounted;
    }

    //--- get
    public int getLimitItems()
    {
        return limitItems;
    }

    public bool getIsDiscounted()
    {
        return isDiscounted;
    }
}

public class GameWikiScraper : WebScraperTemplate
{
    private string category;
    private string patchVersion;

    public GameWikiScraper(string saveFormat,
                           string target,
                           int timeDelay,
                           string category,
                           string patchVersion,
                           string filename) :
                           base(saveFormat, target, timeDelay, filename)
    {
        this.category = category;
        this.patchVersion = patchVersion;
    }

    protected override void extractData()
    {
        Console.WriteLine(">>สกัดเอาส่วน: " + getTarget());
        Console.WriteLine(">>หมวดหมู่: " + category);
        Console.WriteLine(">>Patch Version: " + patchVersion);
    }

    protected override void cleanData()
    {
        Console.WriteLine(">>ทำความสะอาดข้อมูล: ลบช่องว่าง,ลบส่วนที่ Tap ");
    }

    protected override void saveData()
    {
        Console.WriteLine($">>จัดโครงข้อมูลหมวดหมู่ {category} เป็นหัวข้อใหญ่ และส่วน {getTarget()} เป็นหัวข้อรองตามด้วยรายละเอียด");
        Console.WriteLine(">>บันทึกข้อมูล: " + getFilename() + "." + getSaveformat());
    }

    //--- set
    public void setCategory(string category)
    {
        this.category = category;
    }

    public void setPatchVersion(string patchVersion)
    {
        this.patchVersion = patchVersion;
    }

    //--- get
    public string getCategory()
    {
        return category;
    }

    public string getPatchVersion()
    {
        return patchVersion;
    }
}

public class Program
{
    static void Main()
    {
        ShoppingScraper shopping = new ShoppingScraper("csv", "ชื่อสินค้า,ราคา", 2, 100, true, "product");
        shopping.runScript("https://shopping.com");

        Console.WriteLine();

        shopping.setFilename("product_2");
        shopping.setLimitItems(1000);
        shopping.runScript("https://shopping.com");


        Console.WriteLine();


        GameWikiScraper gameWiki_1 = new GameWikiScraper("txt", "ชื่อ,พลังโจมตี", 1, "monster", "1.2", "gameWiki_1");
        gameWiki_1.runScript("https://gameWiki.com");
    }
}