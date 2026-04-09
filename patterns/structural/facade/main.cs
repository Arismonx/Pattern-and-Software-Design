using System;
using System.Diagnostics;

// ====== Complex Subsystem ======
public class RouteCalculatorSystem
{
    private List<string> station = new List<string>
    {
        "ramkhamhaeng","huamak","banthapchang","latkrabang"
    };

    private string currentStation;
    public void setCurrentStation(string currentStation)
    {
        this.currentStation = currentStation;
    }

    public void getCurrentStation()
    {
        Console.WriteLine($">>ตำแหน่งปัจจุบัน: {currentStation}");
    }

    public int getDestinationStaion(string destination)
    {
        string des = destination.ToLower();

        foreach (var stn in station)
        {
            if (stn == des)
            {
                int start = station.IndexOf(currentStation);
                int end = station.IndexOf(des);

                if (end >= start)
                {
                    return end - start;
                }
                else
                {
                    return start - end;
                }
            }
        }

        return 0;
    }

    public int calculateFare(int countStation)
    {
        return 15 + (countStation * 5);
    }
}

public class PaymentSystem
{
    private Dictionary<int, int> cashStok = new Dictionary<int, int>
    {
        {1,10},
        {2,10},
        {5,10},
        {10,10},
        {20,10},
        {50,10},
        {100,10}
    };

    public bool validateCash(int[] cashs)
    {
        foreach (var c in cashs)
        {
            if (c is 500 or 1000)
            {
                Console.WriteLine(">>ไม่รับแบงค์ 500 กับ 1000");
                return false;
            }

            if (c is not (1 or 2 or 5 or 10 or 20 or 50 or 100))
            {
                Console.WriteLine($">>ยกเลิก: ชนิดเงิน {c} บาท ไม่ถูกต้อง");
                return false;
            }
        }

        return true;
    }

    public void addCashStock(int[] cashs) // [50,10,20]
    {
        foreach (var cs in cashStok) // Loop Dictionary cashStok
        {
            foreach (var c in cashs) // loop array cashs
            {
                if (c == cs.Key)
                {
                    cashStok[c] += 1;
                    Console.WriteLine($">>เพิ่มเงิน: {c} บาท เข้า stock แล้ว");
                }
            }
        }
    }

    public void pullCashStock(int cash)
    {
        foreach (var cs in cashStok)
        {
            if (cash == cs.Key)
            {
                cashStok[cash] -= 1;
            }
        }
    }

    public void checkCashStock()
    {
        foreach (var cs in cashStok)
        {
            Console.WriteLine($"เงิน: {cs.Key} บาท ,มีจำนวน: {cs.Value} ");
        }
    }

    public void change(int cash, int fare)
    {
        int change = cash - fare;
        int[] cashType = { 100, 50, 20, 10, 5, 2, 1 };

        if (change == 0)
        {
            Console.WriteLine(">>ไม่มีเงินทอน (จ่ายพอดี) ");
            return;
        }

        foreach (var c in cashType)
        {
            while (change >= c) //ดึงเงินออกมาทอน วนๆไป
            {
                pullCashStock(c);
                change -= c;
                Console.WriteLine($">>ทอนเงิน: {c} บาท (เหลือต้องทอนอีก: {change} บาท)");
            }
        }

        if (change == 0)
        {
            Console.WriteLine(">>ทอนเงินสำเร็จ!");
            return;
        }
    }
}

public class Token
{
    private string destination;
    private int id;
    private int fare;

    public Token(int id, string destination, int fare)
    {
        this.id = id;
        this.destination = destination;
        this.fare = fare;
    }

    public void setDestination(string des)
    {
        this.destination = des;
    }

    public void setFare(int fare)
    {
        this.fare = fare;
    }

    public int getId()
    {
        return this.id;
    }

    public void getInfo()
    {
        Console.WriteLine($"Token id: {id}, สถานีปลายทาง: {destination} , ค่าโดยสาร: {fare}");
    }
}

public class TokenSystem
{
    private List<Token> tokenStock;

    public TokenSystem()
    {
        this.tokenStock = new List<Token>();

        tokenStock.Add(new Token(1, "huamak", 30));
        tokenStock.Add(new Token(2, "None", 0));
        tokenStock.Add(new Token(3, "latkrabang", 25));
        tokenStock.Add(new Token(4, "huamak", 20));
    }

    public void checkTokenStock()
    {
        foreach (var t in tokenStock)
        {
            t.getInfo();
        }
    }

    public Token writeToken(string des, int fare)
    {
        Token pickToken = tokenStock[0];
        tokenStock.RemoveAt(0);
        Console.WriteLine($"\n>>ดึงเหรียญ [ID: {pickToken.getId()}] จาก stock");

        //ล้างข้อมูล token
        formatToken(pickToken);
        pickToken.getInfo();

        //เขียนข้อมูล token ใหม่
        Console.WriteLine($"\n>>เขียนข้อมูลปลายทางใหม่: {des} (ราคา {fare} บาท) \n");
        pickToken.setDestination(des);
        pickToken.setFare(fare);
        pickToken.getInfo();

        return pickToken;
    }

    public void formatToken(Token t)
    {
        Console.WriteLine($">>เครื่องกำลังล้างข้อมูลเก่าในเหรียญ [ID: {t.getId()}]");
        t.setDestination("None");
        t.setFare(0);
    }
}

// ======  Facade ======
public class ElectricTrainTicketMachineFacade
{
    private RouteCalculatorSystem routeCalSystem;
    private PaymentSystem paymentSystem;
    private TokenSystem tokenSystem;
    public ElectricTrainTicketMachineFacade(RouteCalculatorSystem r, PaymentSystem p, TokenSystem t)
    {
        this.routeCalSystem = r;
        this.paymentSystem = p;
        this.tokenSystem = t;
    }

    public void buyTicket(string destination, int[] cashs)
    {
        Console.WriteLine($"=== ซื้อตั๋วไป: {destination} ===");

        int totalCash = 0;
        foreach (var c in cashs)
        {
            totalCash += c;
        }

        Console.WriteLine($"\n>>[ระบบ] RouteCalculator ทำงาน <<");

        routeCalSystem.setCurrentStation("ramkhamhaeng"); // สมมติว่าอยุ่ รามคำแหง

        Console.WriteLine(">>ลูกค้าต้องการไปที่สถานี: " + destination);

        int countStation = routeCalSystem.getDestinationStaion(destination);
        int fare = routeCalSystem.calculateFare(countStation);

        routeCalSystem.getCurrentStation();
        Console.WriteLine($">>คุณต้องจ่าย : {fare} บาท");
        Console.WriteLine($">>ได้รับเงินจากลูกค้ามา: {totalCash} บาท");

        if (totalCash >= fare)
        {
            Console.WriteLine($"\n>>[ระบบ] Payment ทำงาน <<");
            if (paymentSystem.validateCash(cashs)) //เช็คว่าเป็น แบงค์ 500 กับ 1000 ไหม
            {
                paymentSystem.addCashStock(cashs);

                Console.WriteLine(">>เช็คจำนานเงินใน Stock");
                paymentSystem.checkCashStock();

                Console.WriteLine(">>ซื้อตั๋วสำเร็จ รับเงินทอน");
                paymentSystem.change(totalCash, fare);

                Console.WriteLine(">>เช็คจำนานเงินใน Stock");
                paymentSystem.checkCashStock();

                Console.WriteLine($"\n >>[ระบบ] Token ทำงาน<<");

                Console.WriteLine($">>เช็ค Token ใน stock <<");
                tokenSystem.checkTokenStock();

                Token token = tokenSystem.writeToken(destination, fare);

                Console.WriteLine($">>เช็ค Token ใน stock <<");
                tokenSystem.checkTokenStock();

                Console.WriteLine(">>ซื้อตั๋วสำเร็จ รับตั๋ว");
                token.getInfo();
            }
        }
        else
        {
            Console.WriteLine(">>เงินไม่พอจ่าย");
        }
    }
}

public class Program
{
    static void Main()
    {
        RouteCalculatorSystem r = new RouteCalculatorSystem();
        TokenSystem t = new TokenSystem();
        PaymentSystem p = new PaymentSystem();

        ElectricTrainTicketMachineFacade facade = new ElectricTrainTicketMachineFacade(r, p, t);
        facade.buyTicket("latkrabang", [20, 20]);

        Console.WriteLine();
        facade.buyTicket("latkrabang", [2, 1, 2, 5, 20]);

        Console.WriteLine();
        facade.buyTicket("huamak", [500]);
    }
}